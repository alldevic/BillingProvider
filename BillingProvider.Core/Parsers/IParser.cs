using System.Collections.Generic;
using BillingProvider.Core.Models;

namespace BillingProvider.Core.Parsers
{
    public interface IParser
    {
        List<ClientInfo> Data { get; }
        List<string> Captions { get; }
        string Path { get; }

        PaymentMethod DefaultPaymentMethod { get; }
        SignMethodCalculation DefaultSignMethodCalculation { get; }

        void Load();
    }
}