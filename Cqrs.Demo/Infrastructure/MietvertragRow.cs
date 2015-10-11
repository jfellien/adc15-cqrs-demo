using System;

namespace Cqrs.Demo.Infrastructure
{
    public class MietvertragRow
    {
        public string ID { get; set; }
        public string Mieter { get; set; }
        public DateTime EinzugGeplantAm { get; set; }
        public DateTime EinzugErfolgtAm { get; set; }
    }
}