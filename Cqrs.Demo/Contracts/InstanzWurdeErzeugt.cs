using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    public class InstanzWurdeErzeugt : IAmAnEventMessage
    {
        public string AggregateId { get; set; }
    }
}