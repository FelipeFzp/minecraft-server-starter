namespace MinecraftServerStarter.Abstractions.Models
{
    public interface ISettings
    {
        string Port { get; }
        string FileName { get; }
        string FolderPath { get; }
        string BootstrapFileName { get; }
    }
}
