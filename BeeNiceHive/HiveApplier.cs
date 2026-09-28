using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace BeeNiceHive
{
    internal static class HiveApplier
    {
        private static bool _captured;
        private static float _vanillaSeconds = HiveRules.VanillaSecondsPerHoney;
        private static int _vanillaMax = HiveRules.VanillaMaxHoney;

        public static void Apply(Beehive hive)
        {
            if (hive == null)
            {
                return;
            }

            CaptureVanilla(hive);
            float minutes = Plugin.Minutes != null ? Plugin.Minutes.Value : 0f;
            int honey = Plugin.Honey != null ? Plugin.Honey.Value : 0;
            hive.m_secPerUnit = HiveRules.ResolveSecondsPerHoney(minutes, _vanillaSeconds);
            hive.m_maxHoney = HiveRules.ResolveMaxHoney(honey, _vanillaMax);
        }

        public static void ApplyAll()
        {
            ZNetScene scene = ZNetScene.instance;
            if (scene != null)
            {
                ApplyPrefabs(scene);
            }

            Beehive[] hives = Object.FindObjectsByType<Beehive>(FindObjectsSortMode.None);
            if (hives == null)
            {
                return;
            }

            for (int i = 0; i < hives.Length; i++)
            {
                Apply(hives[i]);
            }
        }

        public static void ApplyPrefabs(ZNetScene scene)
        {
            if (scene == null)
            {
                return;
            }

            List<GameObject> prefabs = AccessTools.Field(typeof(ZNetScene), "m_prefabs")?.GetValue(scene) as List<GameObject>;
            if (prefabs == null)
            {
                Plugin.LogWarning("ZNetScene prefab list missing. Hive settings apply when a hive wakes.");
                return;
            }

            for (int i = 0; i < prefabs.Count; i++)
            {
                GameObject prefab = prefabs[i];
                if (prefab == null)
                {
                    continue;
                }

                Beehive hive = prefab.GetComponent<Beehive>();
                if (hive != null)
                {
                    Apply(hive);
                }
            }
        }

        private static void CaptureVanilla(Beehive hive)
        {
            if (_captured)
            {
                return;
            }

            if (hive.m_secPerUnit > 0f)
            {
                _vanillaSeconds = hive.m_secPerUnit;
            }

            if (hive.m_maxHoney > 0)
            {
                _vanillaMax = hive.m_maxHoney;
            }

            _captured = true;
        }
    }
}
