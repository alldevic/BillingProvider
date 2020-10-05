using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;
using NLog;

namespace BillingProvider.KkmServer
{
    public static class Utils
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();


        public static void ServerAvailable(string server, int port)
        {
            var tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(server, port);
                Log.Info($"Сервер {server}:{port} доступен!");
            }
            catch
            {
                Log.Warn($"Сервер {server}:{port} не доступен!");
            }
        }

        public static void ChangeBackground(DataGridViewRow row, Color color)
        {
            Log.Debug($"Changing row background to: {color}");

            for (var i = 0; i < row.Cells.Count; i++)
            {
                row.Cells[i].Style.BackColor = color;
            }
        }
    }
}