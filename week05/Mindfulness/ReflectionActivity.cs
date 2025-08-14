// ReflectionActivity.cs
using System;
using System.Collections.Generic;

namespace Mindfulness
{
    public class ReflectionActivity : Activity
    {
        private readonly List<string> _sessionPromptsPool;
        private readonly List<string> _sessionQuestionsPool;
        private int _questionsAsked = 0;

        public static readonly List<string> DefaultPrompts = new()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        public static readonly List<string> DefaultQuestions = new()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity(List<string> sessionPromptsPool, List<string> sessionQuestionsPool)
            : base(
                "Reflection Activity",
                "This activity will help you reflect on times in your life when you have shown strength and resilience. " +
                "This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
            _sessionPromptsPool = sessionPromptsPool;
            _sessionQuestionsPool = sessionQuestionsPool;
        }

        protected override void ExecuteCore()
        {
            // Get a prompt from the session pool (no repeats until exhausted)
            string prompt = TakeFromPool(_sessionPromptsPool, DefaultPrompts);
            Console.WriteLine("\nConsider the following prompt:");
            Console.WriteLine($" --- {prompt} ---");
            Console.Write("\nWhen you have something in mind, press Enter to continue.");
            Console.ReadLine();

            Console.WriteLine("\nNow ponder on each of the following questions.");
            Console.Write("You may begin in: ");
            Countdown(5);

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            while (DateTime.Now < end)
            {
                string question = TakeFromPool(_sessionQuestionsPool, DefaultQuestions);
                Console.Write($"\n> {question} ");
                PauseWithSpinner(6);
                _questionsAsked++;
            }
        }

        public override string SessionNote() => $"{_questionsAsked} reflection questions";

        private static string TakeFromPool(List<string> pool, List<string> defaultSrc)
        {
            if (pool.Count == 0)
            {
                // replenish with a shuffled copy
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
