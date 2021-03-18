using System.Threading;
using System.Threading.Tasks;
using BillingProvider.Core.Comm.Tasks.Response;

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

        Task<ResponseTaskBase> RegisterCheck(string clientInfo, string name, string sum, string filePath, CancellationToken ct);
        void RegisterTestCheck();

        void TestConnection();
    }
}