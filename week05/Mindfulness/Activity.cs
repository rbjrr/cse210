// Activity.cs
using System;
using System.Threading;

namespace Mindfulness
{
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _durationSeconds;

        public string Name => _name;
        public string Description => _description;
        public int DurationSeconds => _durationSeconds;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationSeconds = 0;
        }

        public void Run()
        {
            Console.Clear();
            ShowStart();
            ExecuteCore();
            ShowEnd();
        }

        protected void ShowStart()
        {
            Console.WriteLine($"Welcome to the {_name}.\n");
            Console.WriteLine(_description + "\n");

            _durationSeconds = AskForDuration();
            Console.WriteLine("\nGet ready to begin...");
            PauseWithSpinner(3);
        }

        protected void ShowEnd()
        {
            Console.WriteLine("\nGreat job!");
            PauseWithSpinner(2);
            Console.WriteLine($"You have completed the {_name} for {_durationSeconds} seconds.");
            PauseWithSpinner(3);
        }

        private int AskForDuration()
        {
            while (true)
            {
                Console.Write("How long, in seconds, would you like for your session? ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive integer number of seconds.");
            }
        }

        protected abstract void ExecuteCore();

        public virtual string SessionNote() => string.Empty;

        // Shared animations & pauses (inherited behavior)
        public static void PauseWithSpinner(int seconds)
        {
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            string[] frames = new[] { "|", "/", "-", "\\", "-", "/", };
            while (DateTime.Now < end)
            {
                Console.Write(frames[i % frames.Length]);
                Thread.Sleep(120);
                Console.Write('\b');
                i++;
            }
        }

        public static void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                string s = i.ToString();
                Console.Write(s);
                Thread.Sleep(1000);
                foreach (var _ in s) Console.Write('\b');
            }
        }

        protected static void BreathingBar(int durationSeconds, bool inhale)
        {
            // Expands (inhale) or contracts (exhale) a bar in fixed steps over durationSeconds
            int steps = Math.Max(5, Math.Min(20, durationSeconds * 2));
            int delay = Math.Max(50, (int)Math.Round(durationSeconds * 1000.0 / steps));
            if (inhale)
            {
                for (int i = 1; i <= steps; i++)
                {
                    string bar = new string('#', i);
                    Console.Write($" [{bar}]");
                    Thread.Sleep(delay);
                    // erase
                    Console.Write('\r');
                    Console.Write(new string(' ', bar.Length + 3));
                    Console.Write('\r');
                }
            }
            else
            {
                for (int i = steps; i >= 1; i--)
                {
                    string bar = new string('#', i);
                    Console.Write($" [{bar}]");
                    Thread.Sleep(delay);
                    Console.Write('\r');
                    Console.Write(new string(' ', bar.Length + 3));
                    Console.Write('\r');
                }
            }
        }
    }
}
