using MinecraftServerStarter.Models;
using MinecraftServerStarter.Services;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MinecraftServerStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            var runtimeDirectory = Directory.GetCurrentDirectory();
            var configuration = GetConfigurationFile(runtimeDirectory);

            var vanillaServer = new VanillaServerService(configuration.VanillaSettings, runtimeDirectory);
            var bungecoordServer = new BungeecordServerService(configuration.BungeecordSettings, runtimeDirectory, configuration.VanillaSettings.Port);

            vanillaServer.UpdateServerSettings();
            bungecoordServer.UpdateServerSettings();

            vanillaServer.GetServerProcess().Start();
            bungecoordServer.GetServerProcess().Start();
        }


        static RunnerSettings GetConfigurationFile(string directory)
        {
            var settingsDirectory = $"{directory}/runner.json";
            var settings = default(RunnerSettings);

            if (!File.Exists(settingsDirectory))
            {
                Console.WriteLine($"Arquivo de configuração não foi encontrado. Criando arquivo de configuração...");

                settings = new RunnerSettings(new VanillaSettings(new SettingsPattern(new[] { '=' }, Environment.NewLine), "25565", "server.properties", @"\Vanilla", "Start.bat"),
                                           new BungeecordSettings(new SettingsPattern(new[] { ':' }, Environment.NewLine), "25564", "config.yml", @"\Bungeecord", "Start.bat"));

                File.WriteAllText(settingsDirectory, JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
            else
            {
                settings = JsonConvert.DeserializeObject<RunnerSettings>(File.ReadAllText(settingsDirectory));
            }

            return settings;
        }
    }
}
