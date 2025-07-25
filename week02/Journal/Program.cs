using System;

public class Program
{
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write ");
            Console.WriteLine("2. Display ");
            Console.WriteLine("3. Save");
            Console.WriteLine("4. Load ");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.NewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter a filename to save the journal: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveJournal(saveFileName);
                    break;
                case "4":
                    Console.Write("Enter a filename to load the journal: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadJournal(loadFileName);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
