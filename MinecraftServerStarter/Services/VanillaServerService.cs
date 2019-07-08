using MinecraftServerStarter.Abstractions.Services;
using MinecraftServerStarter.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace MinecraftServerStarter.Services
{
    public class VanillaServerService : ServerService
    {
        private readonly VanillaSettings _settings;
        private readonly string _runtimeDirectory;

        public VanillaServerService(VanillaSettings settings, string runtimeDirectory) 
            : base(settings, runtimeDirectory)
        {
            _settings = settings;
            _runtimeDirectory = runtimeDirectory;
        }

        public override Process GetServerProcess()
        {
            try
            {
                var vanillaDirectory = $@"{_runtimeDirectory}\{_settings.FolderPath}";
                var vanillaServer = new Process();
                vanillaServer.StartInfo.WorkingDirectory = vanillaDirectory;
                vanillaServer.StartInfo.FileName = $"{vanillaDirectory}/{_settings.BootstrapFileName}";
                vanillaServer.StartInfo.CreateNoWindow = false;

                return vanillaServer;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message } - Não foi possivel executar o servidor Vanilla");
                Console.ReadKey();

                return null;
            }
        }

        public override void UpdateServerSettings()
        {
            try
            {
                if (!File.Exists(_settings.FilePath))
                    throw new FileNotFoundException("Não foi possivel encontrar o arquivo de configuração do servidor Vanilla");

                _settings.UpdateProperty("server-ip", GetCurrentIPV4());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
