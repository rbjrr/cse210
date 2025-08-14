// ListingActivity.cs
using System;
using System.Collections.Generic;

namespace Mindfulness
{
    public class ListingActivity : Activity
    {
        private int _itemCount = 0;
        private readonly List<string> _sessionPromptsPool;

        public static readonly List<string> DefaultPrompts = new()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity(List<string> sessionPromptsPool)
            : base(
                "Listing Activity",
                "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
            _sessionPromptsPool = sessionPromptsPool;
        }

        protected override void ExecuteCore()
        {
            string prompt = TakeFromPool(_sessionPromptsPool, DefaultPrompts);
            Console.WriteLine("\nList as many responses as you can to the following prompt:");
            Console.WriteLine($" --- {prompt} ---");

            Console.Write("\nYou may begin in: ");
            Countdown(5);

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            Console.WriteLine("\nStart listing items (press Enter after each).");

            while (DateTime.Now <= end)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    _itemCount++;
                }
                // If time has elapsed, break after this entry
                if (DateTime.Now > end) break;
            }

            Console.WriteLine($"\nYou listed {_itemCount} items!");
        }

        public override string SessionNote() => $"{_itemCount} items";

        private static string TakeFromPool(List<string> pool, List<string> defaultSrc)
        {
            if (pool.Count == 0)
            {
                pool.AddRange(defaultSrc);
                Shuffle(pool);
            }
            string item = pool[0];
            pool.RemoveAt(0);
            return item;
        }

        private static void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
