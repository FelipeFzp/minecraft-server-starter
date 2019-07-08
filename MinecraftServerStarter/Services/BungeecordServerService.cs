using MinecraftServerStarter.Abstractions.Models;
using MinecraftServerStarter.Abstractions.Services;
using MinecraftServerStarter.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace MinecraftServerStarter.Services
{
    public class BungeecordServerService : ServerService
    {
        private readonly BungeecordSettings _settings;
        private readonly string _runtimeDirectory;
        private readonly string _vanillaPort;

        public BungeecordServerService(BungeecordSettings settings, string runtimeDirectory, string vanillaPort) 
            : base(settings, runtimeDirectory)
        {
            _settings = settings;
            _runtimeDirectory = runtimeDirectory;
            _vanillaPort = vanillaPort;
        }

        public override Process GetServerProcess()
        {
            try
            {
                var bungeeDirectory = $@"{_runtimeDirectory}\{_settings.FolderPath}";
                var bungeecordServer = new Process();
                bungeecordServer.StartInfo.WorkingDirectory = bungeeDirectory;
                bungeecordServer.StartInfo.FileName = $"{bungeeDirectory}/{_settings.BootstrapFileName}";
                bungeecordServer.StartInfo.CreateNoWindow = false;

                return bungeecordServer;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message } - Não foi possivel executar o Bungeecord");
                Console.ReadKey();

                return null;
            }
        }

        public override void UpdateServerSettings()
        {
            try
            {
                if (!File.Exists(_settings.FilePath))
                    throw new FileNotFoundException("Não foi possivel encontrar o arquivo de configuração do servidor Bungeecord");

                var ipv4 = GetCurrentIPV4();

                _settings.UpdateProperty("host", $"{ipv4}:{_settings.Port}");
                _settings.UpdateProperty("address", $"{ipv4}:{_vanillaPort}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
