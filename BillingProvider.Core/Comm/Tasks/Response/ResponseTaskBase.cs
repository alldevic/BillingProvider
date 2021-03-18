namespace BillingProvider.Core.Comm.Tasks.Response
{
    public class ResponseTaskBase
    {
        public string ErrorMessage { get; set; }
        public ResponseTaskStatus ResponseTaskStatus { get; set; }
    }
}