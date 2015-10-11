using System;
using Cqrs.Demo.Contracts;

namespace Cqrs.Demo.Infrastructure
{
    internal class ReservierungRow
    {
        public Adresse Adresse { get; set; }
        public String Einheit { get; set; }
        public DateTime ReserviertBis { get; set; }
    }
}