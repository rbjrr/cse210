using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> entries;
    private List<string> prompts;

    public Journal()
    {
        entries = new List<Entry>();
        prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
    }

    public void NewEntry()
    {
        Random random = new Random();
        int promptIndex = random.Next(prompts.Count);
        string prompt = prompts[promptIndex];

        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();
        Entry newEntry = new Entry(date, prompt, response);
        entries.Add(newEntry);
    }

    public void DisplayJournal()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine("-------------------------------");
        }
    }

    public void SaveJournal(string fileName)
    {
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            foreach (Entry entry in entries)
            {
                outputFile.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadJournal(string fileName)
    {
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            entries.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split("~|~");
                Entry loadedEntry = new Entry(parts[0], parts[1], parts[2]);
                entries.Add(loadedEntry);
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
