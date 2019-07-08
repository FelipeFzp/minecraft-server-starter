using MinecraftServerStarter.Abstractions.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MinecraftServerStarter.Abstractions.Services
{
    public abstract class ServerService
    {
        private readonly ISettings _settings;
        private readonly string _runtimeDirectory;

        public ServerService(ISettings settings, string runtimeDirectory)
        {
            _settings = settings;
            _runtimeDirectory = runtimeDirectory;
        }

        public abstract Process GetServerProcess();
        public abstract void UpdateServerSettings();

        public string GetCurrentIPV4()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return new List<IPAddress>(host.AddressList)
                           .FirstOrDefault(p => p.AddressFamily == AddressFamily.InterNetwork)
                           .ToString();
        }
    }
}
