// GratitudeActivity.cs (extra)
using System;
using System.Collections.Generic;

namespace Mindfulness
{
    public class GratitudeActivity : Activity
    {
        private int _noted = 0;
        private static readonly List<string> _gentlePrompts = new()
        {
            "Name one thing you’re grateful for today and why.",
            "Recall a small act of kindness you received recently.",
            "Think of a daily routine that makes your life easier.",
            "What is one ability or talent you appreciate about yourself?",
            "Name a place that brings you peace and why."
        };

        public GratitudeActivity()
            : base(
                "Gratitude Activity",
                "Spend a few minutes noting things you are grateful for. Briefly describe each item.")
        { }

        protected override void ExecuteCore()
        {
            Console.WriteLine("\nDuring the time, jot quick gratitude notes. Short phrases are fine.");
            Console.WriteLine("Here are some gentle prompts to get you started:");
            foreach (var p in _gentlePrompts) Console.WriteLine($" - {p}");

            Console.Write("\nYou may begin in: ");
            Countdown(5);

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            while (DateTime.Now <= end)
            {
                Console.Write("Gratitude> ");
                string? line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    _noted++;
                }
                if (DateTime.Now > end) break;
            }

            Console.WriteLine($"\nNice! You noted {_noted} gratitude items.");
        }

        public override string SessionNote() => $"{_noted} gratitude notes";
    }
}
