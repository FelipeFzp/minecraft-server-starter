using MinecraftServerStarter.Abstractions.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace MinecraftServerStarter.Models
{
    public class BungeecordSettings : ISettings
    {
        #region Ignored Properties
        [JsonIgnore]
        public string FilePath => $@"{Directory.GetCurrentDirectory()}{FolderPath}\{FileName}";
        #endregion

        #region Json Properties
        [DefaultValue("25564")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Port { get; private set; }

        [DefaultValue("config.yml")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string FileName { get; private set; }

        [DefaultValue(@"\Bungeecord")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string FolderPath { get; private set; }

        [DefaultValue("Start.bat")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string BootstrapFileName { get; private set; }
        public SettingsPattern Pattern { get; private set; }
        #endregion

        #region Constructor
        public BungeecordSettings(SettingsPattern pattern, string port, string fileName, string folderPath, string bootstrapFileName)
        {
            Pattern = pattern;
            Port = port;
            FileName = fileName;
            FolderPath = folderPath;
            BootstrapFileName = bootstrapFileName;
        }
        #endregion

        #region Public Methods
        public void UpdateProperty(string propertyName, string value)
        {
            var content = File.ReadAllLines(FilePath).ToList();
            var line = content.FirstOrDefault(l => l.Trim().StartsWith(propertyName));

            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException($"A propriedade {propertyName} não foi encontrado no arquivo de configuração do servidor Bungeecord");

            var indexToUpdate = content.IndexOf(line);
            var values = line.Split(Pattern.PropertyValueSeparators);

            var newValues = new[] { values[0], value };

            line = string.Join(new string(Pattern.PropertyValueSeparators), newValues);

            content.RemoveAt(indexToUpdate);
            content.Insert(indexToUpdate, line);

            File.WriteAllLines(FilePath, content);
        }
        #endregion
    }
}
