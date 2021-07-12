using System.Threading;
using System.Threading.Tasks;
using BillingProvider.Core.Comm.Tasks.Response;
using BillingProvider.Core.Models;

namespace BillingProvider.Core.KKMDrivers
{
    public interface IKkmDriver
    {
        int Port { get; }
        string Address { get; }
        string Login { get; }
        string Password { get; }
        string CashierName { get; }
        string CashierVatin { get; }

        string CompanyEmail { get; }

        Tax Tax { get; }

        Task<ResponseTaskBase> RegisterCheck(string clientInfo, string name, string sum, string filePath, string source, CancellationToken ct, int signMethodCalculation, int signCalculationObject);
        void RegisterTestCheck();

        void TestConnection();

        void GetKktInfo();
    }
}