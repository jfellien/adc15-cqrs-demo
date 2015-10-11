using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Demo.Contracts;
using Cqrs.Demo.Infrastructure;
using Fluent_CQRS;

namespace Cqrs.Demo.Domain
{
    class Mietvertrag : Aggregate
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

        public void WohneinheitReservieren(Adresse adresse, string einheit, DateTime reserviertBis)
        {
            ConsoleLogger.Log("Reservierung der Wohneinheit wird durchgeführt").AsHappyMessage();

            Changes.Add(new WohneinheitWurdeReserviert
            {
                Adresse = adresse,
                Einheit = einheit,
                ReserviertBis = reserviertBis
            });
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

            if (MessagesOfType<MietvertragWurdeVorbereitet>().Any())
            {
                Changes.Add(new EinzugErfolgt
                {
                    AggregateId = Id,
                    EinzugErfolgtAm = einzugErfolgtAm
                });

                ConsoleLogger.Log("Einzug erfolgt").AsHappyMessage();
            }
            else
            {
                throw new EinzugNichtMöglichMietvertragIstNichtVorbereitet();
            }
        }
    }
}