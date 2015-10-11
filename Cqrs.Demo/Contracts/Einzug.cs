using System;
using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    public class Einzug : IAmACommandMessage
    {
        public string Id { get; set; }
        public DateTime EinzugErfolgtAm { get; set; }
    }
}