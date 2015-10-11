namespace Cqrs.Demo.Contracts
{
    public struct Adresse
    {
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public int Postleitzahl { get; set; }
        public string Ort { get; set; }
    }
}