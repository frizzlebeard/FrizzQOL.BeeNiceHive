using Xunit;

public class HiveRulesTests
{
    [Fact]
    public void Minutes_zero_or_negative_keeps_vanilla_time()
    {
        Assert.Equal(1200f, HiveRules.ResolveSecondsPerHoney(0f, 1200f));
        Assert.Equal(900f, HiveRules.ResolveSecondsPerHoney(-1f, 900f));
    }

    [Fact]
    public void Minutes_sets_seconds_per_honey()
    {
        Assert.Equal(600f, HiveRules.ResolveSecondsPerHoney(10f, 1200f));
    }

    [Fact]
    public void Honey_zero_or_negative_keeps_vanilla_amount()
    {
        Assert.Equal(4, HiveRules.ResolveMaxHoney(0, 4));
        Assert.Equal(4, HiveRules.ResolveMaxHoney(-2, 4));
    }

    [Fact]
    public void Honey_sets_the_full_amount()
    {
        Assert.Equal(6, HiveRules.ResolveMaxHoney(6, 4));
    }

    [Fact]
    public void Empty_hive_counts_every_honey()
    {
        Assert.Equal(4800d, HiveRules.RemainingSeconds(0, 4, 1200f, 0d));
        Assert.Equal("1h 20m", HiveRules.StatusText(0, 4, 1200f, 0d));
    }

    [Fact]
    public void More_honey_adds_time()
    {
        Assert.Equal(7200d, HiveRules.RemainingSeconds(0, 6, 1200f, 0d));
        Assert.Equal(2400d, HiveRules.RemainingSeconds(4, 6, 1200f, 0d));
    }

    [Fact]
    public void Shorter_minutes_shrink_the_time_left()
    {
        Assert.Equal(2400d, HiveRules.RemainingSeconds(0, 4, 600f, 0d));
        Assert.Equal(2300d, HiveRules.RemainingSeconds(2, 4, 1200f, 100d));
    }

    [Fact]
    public void Stored_honey_shortens_the_time_left()
    {
        Assert.Equal(2400d, HiveRules.RemainingSeconds(2, 4, 1200f, 0d));
    }

    [Fact]
    public void Extra_progress_past_a_honey_drops_the_leftover_like_the_game()
    {
        Assert.Equal(2400d, HiveRules.RemainingSeconds(0, 4, 1200f, 2500d));
    }

    [Fact]
    public void Full_hive_says_full()
    {
        Assert.True(HiveRules.IsFull(4, 4, 1200f, 10d));
        Assert.Equal("Full", HiveRules.StatusText(4, 4, 1200f, 500d));
        Assert.Equal(0d, HiveRules.RemainingSeconds(4, 4, 1200f, 500d));
    }

    [Fact]
    public void Lowering_the_cap_below_stored_honey_says_full()
    {
        Assert.Equal("Full", HiveRules.StatusText(4, 2, 1200f, 0d));
    }

    [Fact]
    public void Time_under_an_hour_keeps_seconds()
    {
        Assert.Equal("19m 50s", HiveRules.FormatTime(1190d));
        Assert.Equal("45s", HiveRules.FormatTime(45d));
        Assert.Equal("2m", HiveRules.FormatTime(120d));
    }
}
