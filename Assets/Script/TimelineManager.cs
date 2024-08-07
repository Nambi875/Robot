public static class TimelineManager
{
    private static bool isTimelinePlaying = false;

    public static bool IsTimelinePlaying
    {
        get { return isTimelinePlaying; }
        set { isTimelinePlaying = value; }
    }
}
