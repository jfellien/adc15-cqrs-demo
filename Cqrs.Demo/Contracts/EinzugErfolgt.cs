using System;
using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    public class EinzugErfolgt : IAmAnEventMessage
    {
        public string AggregateId { get; set; }
        public DateTime EinzugErfolgtAm { get; set; }
    }
}