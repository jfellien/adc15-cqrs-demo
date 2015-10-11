using System;
using Cqrs.Demo.Contracts;
using Cqrs.Demo.Domain;
using Cqrs.Demo.Infrastructure;
using Fluent_CQRS;

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
                Straße = "Raumer Str.",
                Hausnummer = "31", 
                Einheit = "0402",
                Postleitzahl = 10439,
                Ort = "Berlin",
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

            /* Logischen Fehler erwartet, da der Mietvertrag noch nicht vorbereitet wurde*/
            var einzug2 = new Einzug
            {
                Id = Guid.NewGuid().ToString(),
                EinzugErfolgtAm = new DateTime(1996, 09, 10)
            };

            hausverwaltung.Handle(einzug2);

            ConsoleLogger.Log("Hit [ENTER] ... ").AsWarning();
            Console.ReadLine();
        }
    }
}
