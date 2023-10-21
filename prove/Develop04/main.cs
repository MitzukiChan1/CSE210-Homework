using System;
using System.Threading;


class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to Mindfulness Activities Menu");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an activity (1-4): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        BreathingActivity breathingActivity = new BreathingActivity();
                        breathingActivity.Start();
                        break;
                    case 2:
                        ReflectionActivity reflectionActivity = new ReflectionActivity();
                        reflectionActivity.Start();
                        break;
                    case 3:
                        ListingActivity listingActivity = new ListingActivity();
                        listingActivity.Start();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid activity.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}

class Activity
{
    protected int Duration { get; set; }
    private string ActivityName { get; set; }
    private string Description { get; set; }

    public Activity(string activityName, string description)
    {
        ActivityName = activityName;
        Description = description;
    }

    public void Start()
    {
        Console.WriteLine($"{ActivityName} Activity - {Description}");
        Console.Write("Enter the duration (in seconds): ");
        Duration = int.Parse(Console.ReadLine());

        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3);

        ExecuteActivity();

        Console.WriteLine($"Good job! You completed the {ActivityName} Activity for {Duration} seconds.");
        PauseWithSpinner(5);
    }

    protected virtual void ExecuteActivity()
    {
        // Base activity execution (generic messages).
    }

    protected void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly.")
    {
    }

    protected override void ExecuteActivity()
    {
        base.ExecuteActivity();

        for (int i = 0; i < Duration; i++)
        {
            if (i % 2 == 0)
                Console.WriteLine("Breathe in...");
            else
                Console.WriteLine("Breathe out...");

            PauseWithSpinner(3);
        }
    }
}

class ReflectionActivity : Activity
{
    private static string[] Prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static string[] Questions = {
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

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    protected override void ExecuteActivity()
    {
        base.ExecuteActivity();

        string randomPrompt = Prompts[new Random().Next(Prompts.Length)];
        Console.WriteLine(randomPrompt);

        foreach (var question in Questions)
        {
            Console.WriteLine(question);
            PauseWithSpinner(3);
        }
    }
}

class ListingActivity : Activity
{
    private static string[] Prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life.")
    {
    }

    protected override void ExecuteActivity()
    {
        base.ExecuteActivity();

        string randomPrompt = Prompts[new Random().Next(Prompts.Length)];
        Console.WriteLine(randomPrompt);
        Console.WriteLine("Think about it for a few seconds...");

        PauseWithSpinner(3);

        Console.Write("Start listing items: ");
        string input = Console.ReadLine();
        int itemCount = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;

        Console.WriteLine($"You listed {itemCount} items.");
    }
}
