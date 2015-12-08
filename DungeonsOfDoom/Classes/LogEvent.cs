using System;
namespace DungeonsOfDoom.Classes
{
    class LogEvent
    {
        public LogEvent(DateTime time, string text)
        {
            Time = time;
            Text = text;
        }

        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
