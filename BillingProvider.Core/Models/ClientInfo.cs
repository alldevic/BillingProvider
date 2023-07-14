using System.Collections.Generic;

namespace BillingProvider.Core.Models
{
    public class ClientInfo
    {
        public ClientInfo()
        {
            Positions = new List<Position>();
        }

        public string Source { get; set; }

        public string SourcePath { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Sum { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public SignMethodCalculation SignMethodCalculation { get; set; }
        public List<Position> Positions { get; set; }

        public object[] AsArrayAtol() => new object[]
        {
            Name, Address, Sum, string.Join(";", Positions)
        };

        public object[] AsArrayKkm() => new object[]
        {
            Name, Address, Sum, string.Join(";", Positions), PaymentMethod, SignMethodCalculation
        };
    }

    public class Position
    {
        public string Name { get; set; }
        public string Sum { get; set; }

        public override string ToString() => $"{Name}+{Sum}";
    }
}