namespace MinecraftServerStarter.Models
{
    public class RunnerSettings
    {
        #region Properties
        public BungeecordSettings BungeecordSettings { get; private set; }
        public VanillaSettings VanillaSettings { get; private set; }
        #endregion

        #region Constructor
        public RunnerSettings(VanillaSettings vanillaSettings, BungeecordSettings bungeecordSettings)
        {
            VanillaSettings = vanillaSettings;
            BungeecordSettings = bungeecordSettings;
        }
        #endregion
    }
}
