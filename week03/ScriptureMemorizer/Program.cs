using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        // Carrega um versículo aleatório do arquivo
        Scripture scripture = LoadRandomScripture("scriptures.txt");

        while (!scripture.AllWordsHidden())
        {
            scripture.Display();
            Console.WriteLine("\nPressione Enter para esconder mais palavras ou digite 'quit' para sair.");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);
        }

        if (scripture.AllWordsHidden())
        {
            scripture.Display();
            Console.WriteLine("\nTodas as palavras foram escondidas. Muito bem!");
        }
    }

    private static Scripture LoadRandomScripture(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var rnd = new Random();
        var line = lines[rnd.Next(lines.Length)];
        var parts = line.Split('|');

        Reference reference;
        if (parts.Length == 4)
            reference = new Reference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]));
        else
            reference = new Reference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));

        string text = parts[^1];
        return new Scripture(reference, text);
    }
}
