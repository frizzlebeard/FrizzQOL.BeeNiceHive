using HarmonyLib;

namespace BeeNiceHive
{
    [HarmonyPatch(typeof(Beehive), "Awake")]
    internal static class BeehiveAwakePatch
    {
        private static void Postfix(Beehive __instance)
        {
            HiveApplier.Apply(__instance);
        }
    }

    [HarmonyPatch(typeof(ZNetScene), "Awake")]
    internal static class ZNetSceneAwakePatch
    {
        private static void Postfix(ZNetScene __instance)
        {
            HiveApplier.ApplyPrefabs(__instance);
        }
    }

    [HarmonyPatch(typeof(Beehive), nameof(Beehive.GetHoverText))]
    internal static class BeehiveHoverPatch
    {
        private static void Postfix(Beehive __instance, ref string __result)
        {
            if (__instance == null || string.IsNullOrEmpty(__result))
            {
                return;
            }

            __result += HiveStatus.Line(__instance);
        }
    }
}
