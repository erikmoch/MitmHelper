using System;
using System.Diagnostics;

namespace MitmHelper
{
    internal class IpRedirector
    {
        public static void Enable()
        {
            RunCommand("netsh", "interface", "ip", "set", "dns", "1", "dhcp");
        }

        public static void AddAddress(string ip)
        {
            RunCommand("netsh", "interface", "ip", "add", "address", "\"Loopback\"", ip, "255.255.255.255");
        }

        public static void RemoveAddress(string ip)
        {
            RunCommand("netsh", "interface", "ip", "delete", "address", "\"Loopback\"", ip);
        }

        private static void RunCommand(params string[] command)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    Arguments = string.Join(" ", command)
                };

                using (Process process = new Process())
                {
                    process.StartInfo = start;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }
    }
}