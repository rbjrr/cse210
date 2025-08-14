// Program.cs
// Mindfulness Program - Exceeds Requirements
// Extras implemented:
// 1) Added "Gratitude Activity" (extra activity type).
// 2) Session log saved to "mindfulness_log.txt" (tracks date, activity, duration, counts).
// 3) Reflection questions and listing prompts are cycled without repeats until all used once per session.
// 4) Breathing includes an expanding/contracting progress bar animation in addition to countdowns.
// 5) "View Log" option in the menu to quickly see recent history.

using System;
using System.Collections.Generic;
using System.IO;

namespace Mindfulness
{
    public class Program
    {
        private static readonly string _logPath = "mindfulness_log.txt";

        public static void Main(string[] args)
        {
            Console.Title = "Mindfulness Program";
            bool running = true;

            // Shared pools (no repeats until exhausted) kept for the session
            var reflectionQuestionsPool = new List<string>(ReflectionActivity.DefaultQuestions);
            var reflectionPromptsPool = new List<string>(ReflectionActivity.DefaultPrompts);
            var listingPromptsPool = new List<string>(ListingActivity.DefaultPrompts);

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Mindfulness Program\n");
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Breathing Activity");
                Console.WriteLine("  2. Reflection Activity");
                Console.WriteLine("  3. Listing Activity");
                Console.WriteLine("  4. Gratitude Activity (extra)");
                Console.WriteLine("  5. View Log (extra)");
                Console.WriteLine("  6. Quit");
                Console.Write("\nSelect a choice from the menu: ");
                string? choice = Console.ReadLine();
                Activity? activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity(reflectionPromptsPool, reflectionQuestionsPool);
                        break;
                    case "3":
                        activity = new ListingActivity(listingPromptsPool);
                        break;
                    case "4":
                        activity = new GratitudeActivity();
                        break;
                    case "5":
                        ShowLog();
                        Activity.PauseWithSpinner(3);
                        continue;
                    case "6":
                        running = false;
                        continue;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        Activity.PauseWithSpinner(2);
                        continue;
                }

                if (activity != null)
                {
                    activity.Run();
                    WriteLog(activity);
                }
            }

            Console.WriteLine("\nGoodbye! Keep up your mindfulness practice.");
        }

        private static void WriteLog(Activity activity)
        {
            try
            {
                using var sw = new StreamWriter(_logPath, append: true);
                sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{activity.Name}\t{activity.DurationSeconds}s\t{activity.SessionNote()}");
            }
            catch { /* ignore logging errors */ }
        }

        private static void ShowLog()
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Log ===\n");
            if (!File.Exists(_logPath))
            {
                Console.WriteLine("No log yet. Do an activity and come back!");
                return;
            }
            try
            {
                string[] lines = File.ReadAllLines(_logPath);
                int start = Math.Max(0, lines.Length - 20);
                for (int i = start; i < lines.Length; i++)
                {
                    Console.WriteLine(lines[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to read log: {ex.Message}");
            }
        }
    }
}
