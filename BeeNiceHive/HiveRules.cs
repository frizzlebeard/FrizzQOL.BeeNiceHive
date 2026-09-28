using System;

public static class HiveRules
{
    public const float VanillaSecondsPerHoney = 1200f;
    public const int VanillaMaxHoney = 4;

    public static float ResolveSecondsPerHoney(float minutes, float vanillaSeconds)
    {
        float fallback = vanillaSeconds > 0f ? vanillaSeconds : VanillaSecondsPerHoney;
        if (float.IsNaN(minutes) || minutes <= 0f)
        {
            return fallback;
        }

        return minutes * 60f;
    }

    public static int ResolveMaxHoney(int honey, int vanillaMax)
    {
        int fallback = vanillaMax > 0 ? vanillaMax : VanillaMaxHoney;
        if (honey <= 0)
        {
            return fallback;
        }

        return honey;
    }

    public static bool IsFull(int level, int maxHoney, float secondsPerHoney, double progressSeconds)
    {
        int levelAfter;
        double progressAfter;
        Advance(level, maxHoney, secondsPerHoney, progressSeconds, out levelAfter, out progressAfter);
        return maxHoney <= 0 || levelAfter >= maxHoney;
    }

    public static double RemainingSeconds(int level, int maxHoney, float secondsPerHoney, double progressSeconds)
    {
        int levelAfter;
        double progressAfter;
        Advance(level, maxHoney, secondsPerHoney, progressSeconds, out levelAfter, out progressAfter);
        if (maxHoney <= 0 || secondsPerHoney <= 0f || levelAfter >= maxHoney)
        {
            return 0d;
        }

        int left = maxHoney - levelAfter;
        double intoNext = secondsPerHoney - progressAfter;
        if (intoNext < 0d)
        {
            intoNext = 0d;
        }

        return ((left - 1) * (double)secondsPerHoney) + intoNext;
    }

    public static string StatusText(int level, int maxHoney, float secondsPerHoney, double progressSeconds)
    {
        if (IsFull(level, maxHoney, secondsPerHoney, progressSeconds))
        {
            return "Full";
        }

        return FormatTime(RemainingSeconds(level, maxHoney, secondsPerHoney, progressSeconds));
    }

    public static string FormatTime(double seconds)
    {
        if (double.IsNaN(seconds) || seconds <= 0d)
        {
            return "0s";
        }

        int total = (int)Math.Ceiling(seconds);
        if (total < 1)
        {
            total = 1;
        }

        int hours = total / 3600;
        int minutes = (total % 3600) / 60;
        int secs = total % 60;
        if (hours > 0 && minutes > 0)
        {
            return hours + "h " + minutes + "m";
        }

        if (hours > 0)
        {
            return hours + "h";
        }

        if (minutes > 0 && secs > 0)
        {
            return minutes + "m " + secs + "s";
        }

        if (minutes > 0)
        {
            return minutes + "m";
        }

        return secs + "s";
    }

    private static void Advance(int level, int maxHoney, float secondsPerHoney, double progressSeconds, out int levelAfter, out double progressAfter)
    {
        levelAfter = level < 0 ? 0 : level;
        progressAfter = progressSeconds < 0d || double.IsNaN(progressSeconds) ? 0d : progressSeconds;
        if (maxHoney <= 0 || secondsPerHoney <= 0f || float.IsNaN(secondsPerHoney))
        {
            return;
        }

        if (levelAfter >= maxHoney)
        {
            progressAfter = 0d;
            return;
        }

        if (progressAfter <= secondsPerHoney)
        {
            return;
        }

        int gain = (int)(progressAfter / secondsPerHoney);
        if (gain < 1)
        {
            gain = 1;
        }

        int room = maxHoney - levelAfter;
        if (gain > room)
        {
            gain = room;
        }

        levelAfter += gain;
        progressAfter = 0d;
    }
}
