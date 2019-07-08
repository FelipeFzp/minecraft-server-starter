namespace MinecraftServerStarter.Models
{
    public class SettingsPattern
    {
        public readonly char[] PropertyValueSeparators;
        public readonly string LineSeparator;
        public SettingsPattern(char[] propertyValueSeparators, string lineSeparator)
        {
            PropertyValueSeparators = propertyValueSeparators;
            LineSeparator = lineSeparator;
        }
    }
}
