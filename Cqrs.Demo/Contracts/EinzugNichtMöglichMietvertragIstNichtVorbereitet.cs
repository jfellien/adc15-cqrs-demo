using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    internal class EinzugNichtMöglichMietvertragIstNichtVorbereitet : Fault
    {
        public EinzugNichtMöglichMietvertragIstNichtVorbereitet() 
            : base("Mietvertrag noch nicht vorbereitet. Bitte erst vorbereiten, dann kann der Einzug statt finden")
        {
        }
    }
}