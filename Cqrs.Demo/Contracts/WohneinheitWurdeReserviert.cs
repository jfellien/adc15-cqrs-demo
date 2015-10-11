using System;
using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    public class WohneinheitWurdeReserviert : IAmAnEventMessage
    {
        public Adresse Adresse { get; set; }
        public string Einheit { get; set; }
        public DateTime ReserviertBis { get; set; }
    }
}