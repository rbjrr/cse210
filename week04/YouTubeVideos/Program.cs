using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Learning C# in 10 Minutes", "Code Academy", 600);
        video1.AddComment(new Comment("Alice", "Great tutorial!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks."));
        video1.AddComment(new Comment("Charlie", "I learned a lot."));

        Video video2 = new Video("Advanced OOP Concepts", "Tech Guru", 1200);
        video2.AddComment(new Comment("Diana", "Clear explanation."));
        video2.AddComment(new Comment("Evan", "Loved the examples."));
        video2.AddComment(new Comment("Fiona", "Can you make one about interfaces?"));

        Video video3 = new Video("LINQ Basics", "Dev Tips", 900);
        video3.AddComment(new Comment("George", "Finally understood LINQ!"));
        video3.AddComment(new Comment("Hannah", "The code examples were great."));
        video3.AddComment(new Comment("Ian", "More LINQ videos please."));

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length (seconds): {video.GetLength()}");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.GetName()}: {comment.GetText()}");
            }
            Console.WriteLine();
        }
    }
}
