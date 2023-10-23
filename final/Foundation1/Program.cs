using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> comments = new List<Comment>();

    public void AddComment(string commenterName, string commentText)
    {
        Comment comment = new Comment(commenterName, commentText);
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length (seconds): {LengthInSeconds}");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
        }
        Console.WriteLine();
    }
}

class Comment
{
    public string CommenterName { get; }
    public string CommentText { get; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video
        {
            Title = "Introduction to C# Programming",
            Author = "JohnDoe123",
            LengthInSeconds = 600
        };
        video1.AddComment("User1", "Great video! Thanks for the tutorial.");
        video1.AddComment("User2", "I learned a lot from this.");
        video1.AddComment("User3", "Could you make more videos like this?");

        Video video2 = new Video
        {
            Title = "Cooking Spaghetti Bolognese",
            Author = "ChefCooking101",
            LengthInSeconds = 900
        };
        video2.AddComment("CookingEnthusiast", "This recipe is amazing!");
        video2.AddComment("FoodLover123", "I can't wait to try this at home.");
        video2.AddComment("BeginnerChef", "Clear and easy-to-follow instructions.");

        videos.Add(video1);
        videos.Add(video2);

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
