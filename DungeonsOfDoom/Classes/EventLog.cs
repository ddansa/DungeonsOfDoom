using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    class EventLog
    {
        public EventLog()
        {
            Events = new List<GameEvent>();
        }

        public List<GameEvent> Events { get; set; }

        public void AddEvent(string text)
        {
            GameEvent e = new GameEvent(DateTime.Now, text);
            Events.Add(e);
        }

        public void DisplayEventLog(bool limited = false, int length = 5, bool pauseAfterPrint = false)
        {
            // Exit function if EventList is empty
            if (Events.Count == 0)
                return;

            if (pauseAfterPrint)
                Console.Clear();

            Console.WriteLine("----------------");
            int i = 0;
            foreach (var ev in Events.AsEnumerable().Reverse())
            {
                // In the short log (while map is visible), only 5 event will display
                if (i > length && limited)
                    break;
                // Formats the DateTime to hours/minutes/seconds
                string time = ev.Time.ToString("HH:mm:ss");
                // Prints the event
                Console.WriteLine("<" + time + "> " + ev.Text);
                // Counter to limit Events in log
                i++;
            }
            Console.WriteLine("----------------");

            if (!pauseAfterPrint) return;
            Console.SetWindowPosition(0, 0);
            Console.ReadKey();
        }
    }
}
