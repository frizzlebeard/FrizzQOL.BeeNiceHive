using System;

namespace BeeNiceHive
{
    internal static class HiveStatus
    {
        public static string Line(Beehive hive)
        {
            if (hive == null)
            {
                return string.Empty;
            }

            int level = 0;
            double progress = 0d;
            ZNetView view = hive.GetComponent<ZNetView>();
            if (view != null && view.IsValid())
            {
                ZDO zdo = view.GetZDO();
                if (zdo != null)
                {
                    level = zdo.GetInt(ZDOVars.s_level, 0);
                    progress = zdo.GetFloat(ZDOVars.s_product, 0f) + ElapsedSeconds(zdo);
                }
            }

            string text = HiveRules.StatusText(level, hive.m_maxHoney, hive.m_secPerUnit, progress);
            return "\n<color=yellow>" + text + "</color>";
        }

        private static double ElapsedSeconds(ZDO zdo)
        {
            if (ZNet.instance == null)
            {
                return 0d;
            }

            DateTime now = ZNet.instance.GetTime();
            long ticks = zdo.GetLong(ZDOVars.s_lastTime, now.Ticks);
            if (ticks <= 0L || ticks > DateTime.MaxValue.Ticks)
            {
                return 0d;
            }

            double elapsed = (now - new DateTime(ticks)).TotalSeconds;
            if (double.IsNaN(elapsed) || elapsed < 0d)
            {
                return 0d;
            }

            return elapsed;
        }
    }
}
