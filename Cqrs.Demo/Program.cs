using System;
using System.Collections.Generic;
using System.Linq;
using Fluent_CQRS;
using Fluent_CQRS.Extensions;

namespace Cqrs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger.Log("Lets start to do CQRS").AsHappyMessage();

            var eventStore = new InMemoryEventStore();
            var aggregates = Aggregates.CreateWith(eventStore);

            aggregates.PublishNewStateTo(new MietvertragEventHandler());

            var hausverwaltung = new Hausverwaltung(aggregates);

            var mietvertragVorbereiten = new MietvertragVorbereiten
            {
                Id = Guid.NewGuid().ToString(),
                MieterVorname = "Jan",
                MieterNachname = "Fellien",
                EinzugGeplantAm = new DateTime(1996,09,01)
            };

            hausverwaltung.Handle(mietvertragVorbereiten);

            var einzug = new Einzug
            {
                Id = mietvertragVorbereiten.Id,
                EinzugErfolgtAm = new DateTime(1996, 09, 10)
            };

            hausverwaltung.Handle(einzug);

            ConsoleLogger.Log("Hit [ENTER] ... ").AsWarning();
            Console.ReadLine();
        }
    }

    internal class Einzug :IAmACommandMessage
    {
        public string Id { get; set; }
        public DateTime EinzugErfolgtAm { get; set; }
    }

    internal class MietvertragVorbereiten : IAmACommandMessage
    {
        public string MieterVorname { get; set; }
        public string MieterNachname { get; set; }
        public DateTime EinzugGeplantAm { get; set; }
        public string Id { get; set; }
    }

    internal class Hausverwaltung
    {
        private readonly Aggregates _aggregates;

        public Hausverwaltung(Aggregates aggregates)
        {
            _aggregates = aggregates;
        }

        public void Handle(MietvertragVorbereiten command)
        {
            _aggregates
                .Provide<Mietvertrag>()
                .With(command)
                .Try(mietvertrag => mietvertrag.Vorbereiten(
                    command.MieterVorname,
                    command.MieterNachname,
                    command.EinzugGeplantAm))
                .CatchFault(HandleFault)
                .CatchException(HandleException);
        }

        public void Handle(Einzug command)
        {
            _aggregates.Provide<Mietvertrag>()
                .With(command)
                .Try(mietvertrag=>mietvertrag.Einzug(command.EinzugErfolgtAm))
                .CatchFault(HandleFault)
                .CatchException(HandleException);
        }

        private void HandleFault(Fault obj)
        {
            throw new NotImplementedException();
        }

        private void HandleException(Exception obj)
        {
            throw new NotImplementedException();
        }

    }

    internal class Mietvertrag : Aggregate
    {
        public Mietvertrag(string id, IEnumerable<IAmAnEventMessage> history) : base(id, history)
        {
            if (MessagesOfType<InstanzWurdeErzeugt>().Any())
            {
                ConsoleLogger.Log("Instanz mit " + id + " schon vorhanden").AsInfo();
                return;
            }

            Changes.Add(new InstanzWurdeErzeugt
            {
                AggregateId = Id
            });

            ConsoleLogger.Log("Instanz mit " + id + " erzeugt").AsInfo();
        }

        public void Vorbereiten(string mieterVorname, string mieterNachname, DateTime einzugGeplantAm)
        {
            ConsoleLogger.Log("Mietvertrag wird vorbereitet").AsHappyMessage();

            Changes.Add(new MietvertragWurdeVorbereitet
            {
                AggrgateId = Id,
                MieterVorname = mieterVorname,
                MieterNachname = mieterNachname,
                EinzugGeplantAm = einzugGeplantAm
            });
        }

        public void Einzug(DateTime einzugErfolgtAm)
        {
            Changes.Add(new EinzugErfolgt
            {
                AggregateId = Id,
                EinzugErfolgtAm = einzugErfolgtAm
            });
        }
    }

    internal class EinzugErfolgt : IAmAnEventMessage
    {
        public string AggregateId { get; set; }
        public DateTime EinzugErfolgtAm { get; set; }
    }

    internal class MietvertragWurdeVorbereitet : IAmAnEventMessage
    {
        public string AggrgateId { get; set; }
        public string MieterVorname { get; set; }
        public string MieterNachname { get; set; }
        public DateTime EinzugGeplantAm { get; set; }
    }

    internal class InstanzWurdeErzeugt : IAmAnEventMessage
    {
        public string AggregateId { get; set; }
    }

    internal class MietvertragEventHandler : IHandleEvents
    {
        private MietvertragTable _mietvertragTable;

        public MietvertragEventHandler()
        {
            _mietvertragTable = new MietvertragTable();
        }

        public void Receive(IEnumerable<IAmAnEventMessage> events)
        {
            ConsoleLogger.Log("Handle Events").AsHappyMessage();
            events.ToList().ForEach(message => message.HandleMeWith(this));
        }

        void Handle(InstanzWurdeErzeugt message)
        {
            
            _mietvertragTable.Add(new MietvertragRow
            {
                ID = message.AggregateId
            });

            ConsoleLogger.Log("Instanz eines Mietvertrages angelegt {" + message.AggregateId + "}").AsHappyMessage();
        }

        void Handle(MietvertragWurdeVorbereitet message)
        {
            var mietvertrag = _mietvertragTable.Single(row => row.ID == message.AggrgateId);

            mietvertrag.Mieter = message.MieterVorname + ", " + message.MieterNachname;
            mietvertrag.EinzugGeplantAm = message.EinzugGeplantAm;

            ConsoleLogger.Log("Mietvertrag wurde vorbereitet in der DB gespeichert.").AsHappyMessage();
        }

        void Handle(EinzugErfolgt message)
        {
            _mietvertragTable
                .Single(row => row.ID == message.AggregateId)
                .EinzugErfolgtAm = message.EinzugErfolgtAm;

            ConsoleLogger.Log("Meiter ist erfolgreich eingezogen am " + message.EinzugErfolgtAm.ToShortDateString());
        }
    }

    public class MietvertragTable : List<MietvertragRow>
    { }

    public class MietvertragRow
    {
        public string ID { get; set; }
        public string Mieter { get; set; }
        public DateTime EinzugGeplantAm { get; set; }
        public DateTime EinzugErfolgtAm { get; set; }
    }
}
