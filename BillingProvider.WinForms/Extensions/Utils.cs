using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using NLog;

namespace BillingProvider.WinForms.Extensions
{
    public static class Utils
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static void ChangeBackground(DataGridViewRow row, Color color)
        {
            Log.Debug($"Changing row background to: {color}");

            for (var i = 0; i < row.Cells.Count; i++)
            {
                row.Cells[i].Style.BackColor = color;
            }
        }

        public static bool PingServer(string server)
        {
            var ping = new Ping();
            try
            {
                var reply = ping.Send(server, 3000);
                if (reply.Status != IPStatus.Success)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}