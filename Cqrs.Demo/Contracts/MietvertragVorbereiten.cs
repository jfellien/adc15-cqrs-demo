using System;
using Fluent_CQRS;

namespace Cqrs.Demo.Contracts
{
    public class MietvertragVorbereiten : IAmACommandMessage
    {
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public int Postleitzahl { get; set; }
        public string Ort { get; set; }
        public string Einheit { get; set; }
        public string MieterVorname { get; set; }
        public string MieterNachname { get; set; }
        public DateTime EinzugGeplantAm { get; set; }
        public string Id { get; set; }
    }
}