using System.Threading;
using System.Threading.Tasks;
using BillingProvider.Core.Comm.Tasks.Response;

namespace BillingProvider.Core.KKMDrivers
{
    public class KkmServerDriver : IKkmDriver
    {
        public KkmServerDriver(string cashierName, string cashierVatin, string password, string login, string address,
            int port, string companyEmail)
        {
            CashierName = cashierName;
            CashierVatin = cashierVatin;
            Password = password;
            Login = login;
            Address = address;
            Port = port;
            CompanyEmail = companyEmail;
        }

        public int Port { get; }
        public string Address { get; }
        public string Login { get; }
        public string Password { get; }
        public string CashierName { get; }
        public string CashierVatin { get; }
        public string CompanyEmail { get; }


        public Task<ResponseTaskBase> RegisterCheck(string clientInfo, string name, string sum, string filePath,CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterTestCheck()
        {
            throw new System.NotImplementedException();
        }

        public void TestConnection()
        {
            throw new System.NotImplementedException();
        }
    }
}