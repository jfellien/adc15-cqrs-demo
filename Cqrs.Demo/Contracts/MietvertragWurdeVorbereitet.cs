using System;
using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    public class MietvertragWurdeVorbereitet : IAmAnEventMessage
    {
        public string AggrgateId { get; set; }
        public string MieterVorname { get; set; }
        public string MieterNachname { get; set; }
        public DateTime EinzugGeplantAm { get; set; }
    }
}