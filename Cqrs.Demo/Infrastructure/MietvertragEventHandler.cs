using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Demo.Contracts;
using Fluent_CQRS;
using Fluent_CQRS.Extensions;

namespace Cqrs.Demo.Infrastructure
{
    public class MietvertragEventHandler : IHandleEvents
    {
        private MietvertragTable _mietvertragTable;
        private ReservierungTable _reservierungTable;

        public MietvertragEventHandler()
        {
            _mietvertragTable = new MietvertragTable();
            _reservierungTable = new ReservierungTable();
        }

        public void Receive(IEnumerable<IAmAnEventMessage> events)
        {
            var i = 1;
            events.ToList().ForEach(message =>
            {

                Console.Write(i + ".) ");
                message.HandleMeWith(this);

                i++;
            });
        }

        void Handle(InstanzWurdeErzeugt message)
        {
            
            _mietvertragTable.Add(new MietvertragRow
            {
                ID = message.AggregateId
            });

            ConsoleLogger.Log("Instanz eines Mietvertrages {" + message.AggregateId + "} wurde in der DB gespeichert").AsHappyMessage();
        }

        void Handle(WohneinheitWurdeReserviert message)
        {
            _reservierungTable.Add(new ReservierungRow
            {
                Adresse = message.Adresse,
                Einheit = message.Einheit,
                ReserviertBis = message.ReserviertBis
            });

            ConsoleLogger.Log("Reservierung für Einheit " + message.Einheit + " wurde in der DB gespeichert").AsHappyMessage();
        }

        void Handle(MietvertragWurdeVorbereitet message)
        {
            var mietvertrag = _mietvertragTable.Single(row => row.ID == message.AggrgateId);

            mietvertrag.Mieter = message.MieterVorname + ", " + message.MieterNachname;
            mietvertrag.EinzugGeplantAm = message.EinzugGeplantAm;

            ConsoleLogger.Log("Vorbereitung eines Mietvertrags für " + message.MieterNachname + " wurde in der DB gespeichert.").AsHappyMessage();
        }

        void Handle(EinzugErfolgt message)
        {
            _mietvertragTable
                .Single(row => row.ID == message.AggregateId)
                .EinzugErfolgtAm = message.EinzugErfolgtAm;

            ConsoleLogger.Log("Einzug des Mieters am " + message.EinzugErfolgtAm.ToShortDateString() + " wurde in der DB gespeichert").AsHappyMessage();
        }
    }
}