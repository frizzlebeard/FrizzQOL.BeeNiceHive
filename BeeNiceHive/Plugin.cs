using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace BeeNiceHive
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.vhmod.beenicehive";
        public const string PluginName = "FrizzQOL.BeeNiceHive";
        public const string PluginVersion = "0.1.0";

        internal static Plugin Instance { get; private set; }

        internal static ConfigEntry<float> Minutes { get; private set; }

        internal static ConfigEntry<int> Honey { get; private set; }

        private void Awake()
        {
            Instance = this;
            Minutes = Config.Bind("General", "Minutes", 0f, "Real minutes for each honey. 0 keeps the normal time. Below 0 does the same.");
            Honey = Config.Bind("General", "Honey", 0, "How many honey a full hive holds. 0 keeps the normal amount. Below 0 does the same.");
            Config.SettingChanged += (_, __) => HiveApplier.ApplyAll();
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll();
            Logger.LogInfo(PluginName + " " + PluginVersion + " loaded");
        }

        internal static void LogInfo(string message)
        {
            if (Instance != null)
            {
                Instance.Logger.LogInfo(message);
            }
        }

        internal static void LogWarning(string message)
        {
            if (Instance != null)
            {
                Instance.Logger.LogWarning(message);
            }
        }
    }
}
