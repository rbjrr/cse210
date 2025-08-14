using System;

namespace EternalQuest
{
    class Program
    {
        // Creativity / extra: implemented a simple leveling system:
        // every 1000 points => level up. This is described here as required.
        // Also included friendly messages when leveling up and when bonus points are awarded.
        //
        // To run: compile with `csc` or open as a C# project in Visual Studio. The files are organized
        // so each class is in its own file and follows naming conventions.
        //
        // Program behavior: menu-driven console app that allows creating goals, listing them,
        // recording events, saving, loading, and showing the score and level.

        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            string dataFile = "eternalquest_save.txt";

            // Try auto-load if exists
            try
            {
                manager.Load(dataFile);
                Console.WriteLine("Loaded saved progress if any.");
            }
            catch { }

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("Eternal Quest - Main Menu");
                Console.WriteLine($"Score: {manager.Score}  Level: {GetLevel(manager.Score)}");
                Console.WriteLine("1. Create new goal");
                Console.WriteLine("2. List goals");
                Console.WriteLine("3. Record event (complete/advance a goal)");
                Console.WriteLine("4. Save progress");
                Console.WriteLine("5. Load progress");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateGoalFlow(manager);
                        break;
                    case "2":
                        manager.ListGoals();
                        break;
                    case "3":
                        RecordEventFlow(manager);
                        break;
                    case "4":
                        manager.Save(dataFile);
                        Console.WriteLine("Saved.");
                        break;
                    case "5":
                        manager.Load(dataFile);
                        Console.WriteLine("Loaded.");
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }

            // On exit, save automatically
            manager.Save(dataFile);
            Console.WriteLine("Progress saved. Goodbye!");
        }

        static int GetLevel(int score)
        {
            return (score / 1000) + 1;
        }

        static void CreateGoalFlow(GoalManager manager)
        {
            Console.WriteLine("Choose goal type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Type: ");
            var t = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Description: ");
            var desc = Console.ReadLine();
            Console.Write("Points (integer): ");
            int points = ReadIntWithDefault(100);

            switch (t)
            {
                case "1":
                    manager.AddGoal(new SimpleGoal(name, desc, points));
                    Console.WriteLine("Simple goal created.");
                    break;
                case "2":
                    manager.AddGoal(new EternalGoal(name, desc, points));
                    Console.WriteLine("Eternal goal created.");
                    break;
                case "3":
                    Console.Write("Target count (how many times to complete): ");
                    int target = ReadIntWithDefault(5);
                    Console.Write("Bonus points on completion: ");
                    int bonus = ReadIntWithDefault(500);
                    manager.AddGoal(new ChecklistGoal(name, desc, points, target, bonus));
                    Console.WriteLine("Checklist goal created.");
                    break;
                default:
                    Console.WriteLine("Invalid type.");
                    break;
            }
        }

        static void RecordEventFlow(GoalManager manager)
        {
            if (manager.Goals.Count == 0)
            {
                Console.WriteLine("No goals to record.");
                return;
            }

            manager.ListGoals();
            Console.Write("Choose goal number to record event: ");
            int choice = ReadIntWithDefault(1) - 1;
            try
            {
                int gained = manager.RecordEvent(choice);
                Console.WriteLine($"You gained {gained} points!");
                int newLevel = GetLevel(manager.Score);
                Console.WriteLine($"Total Score: {manager.Score}  Level: {newLevel}");
                // level-up message (simple)
                if ((manager.Score - gained) / 1000 < newLevel - 1)
                {
                    Console.WriteLine("Congratulations! You leveled up!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not record event: {ex.Message}");
            }
        }

        static int ReadIntWithDefault(int defaultValue)
        {
            var s = Console.ReadLine();
            if (int.TryParse(s, out int v)) return v;
            return defaultValue;
        }
    }
}
