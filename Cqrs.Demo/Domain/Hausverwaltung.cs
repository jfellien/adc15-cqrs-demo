using System;
using Cqrs.Demo.Contracts;
using Cqrs.Demo.Infrastructure;
using Fluent_CQRS;
using Fluent_CQRS.Extensions;

namespace Cqrs.Demo.Domain
{
    public class Hausverwaltung
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
                .Try(mietvertrag =>
                {
                    var adresse = new Adresse
                    {
                        Straﬂe = command.Straﬂe,
                        Hausnummer = command.Hausnummer,
                        Postleitzahl = command.Postleitzahl,
                        Ort = command.Ort
                    };

                    mietvertrag.WohneinheitReservieren(
                        adresse, 
                        command.Einheit,
                        command.EinzugGeplantAm);

                    mietvertrag.Vorbereiten(
                        command.MieterVorname,
                        command.MieterNachname,
                        command.EinzugGeplantAm);
                })
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

        private void HandleFault(Fault fault)
        {
            ConsoleLogger.Log(fault.Message).AsError();
        }

        private void HandleException(Exception obj)
        {
            throw new NotImplementedException();
        }

    }
}