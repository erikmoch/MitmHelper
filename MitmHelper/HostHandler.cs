using System;
using System.IO;
using System.Net;
using System.Linq;

namespace MitmHelper
{
    public static class HostHandler
    {
        private static readonly string HostsFilePath = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");

        static HostHandler()
        {
            TryRemoveRedirects();
        }

        public static bool TryAddRedirect(string original, string redirect)
        {
            try
            {
                string match = $"{original} {redirect}";
                if (!File.ReadAllLines(HostsFilePath).Any(l => l.Contains(match)))
                    File.AppendAllText(HostsFilePath, $"\r\n{original} {redirect} #MitmHelper");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding redirect: {ex.Message}");
                return false;
            }
        }

        public static bool TryRemoveRedirects()
        {
            try
            {
                File.WriteAllLines(HostsFilePath, File.ReadAllLines(HostsFilePath).Where(l => !l.EndsWith("#MitmHelper") && l != string.Empty));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing redirects: {ex.Message}");
                return false;
            }
        }

        public static IPAddress GetIPAddressFromHost(string host)
        {
            try
            {
                return Dns.GetHostEntry(host).AddressList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting IP address from host: {ex.Message}");
                return null;
            }
        }
    }
}