using Code.CoreSystem;

namespace Work.JES.Code.Event
{
    public enum TimeOfDay
    {
        Day,
        Night
    }
    public struct DateChangeEvent : IEvent
    {
        public int Date { get; }
        public DateChangeEvent(int date)
        {
            Date = date;
        }
    }
    public struct DayNightChangeEvent : IEvent
    {
        public TimeOfDay CurrentTime { get; }
        public DayNightChangeEvent(TimeOfDay currentTime)
        {
            CurrentTime = currentTime;
        }
    }
}