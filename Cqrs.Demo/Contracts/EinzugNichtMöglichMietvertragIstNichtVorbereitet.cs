using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    internal class EinzugNichtM�glichMietvertragIstNichtVorbereitet : Fault
    {
        public EinzugNichtM�glichMietvertragIstNichtVorbereitet() 
            : base("Mietvertrag noch nicht vorbereitet. Bitte erst vorbereiten, dann kann der Einzug statt finden")
        {
        }
    }
}