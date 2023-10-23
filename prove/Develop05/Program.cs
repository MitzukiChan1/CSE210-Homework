using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Base class for all types of goals
class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsCompleted = false;
    }

    public virtual void Complete()
    {
        IsCompleted = true;
    }

    public override string ToString()
    {
        return $"{Name} [{(IsCompleted ? 'X' : ' ')}]";
    }
}

// Subclass for eternal goals
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }
}

// Subclass for checklist goals
class ChecklistGoal : Goal
{
    public int CompletionCount { get; set; }
    public int CompletionThreshold { get; set;}
    public int CompletionBonus { get; set; }

    public ChecklistGoal(string name, int points, int threshold, int bonus) : base(name, points)
    {
        CompletionCount = 0;
        CompletionThreshold = threshold;
        CompletionBonus = bonus;
    }

    public override void Complete()
    {
        CompletionCount++;
        if (CompletionCount == CompletionThreshold)
        {
            IsCompleted = true;
            Points += CompletionBonus;
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()} Completed {CompletionCount}/{CompletionThreshold} times";
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();

    static void Main()
    {
        LoadGoals(); // Load saved goals from a file

        while (true)
        {
            Console.WriteLine("Eternal Quest - Goals Tracker");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record an event");
            Console.WriteLine("3. List goals");
            Console.WriteLine("4. Display your score");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        CreateNewGoal();
                        break;
                    case 2:
                        RecordEvent();
                        break;
                    case 3:
                        ListGoals();
                        break;
                    case 4:
                        DisplayScore();
                        break;
                    case 5:
                        SaveGoals(); // Save goals to a file before exiting
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }

    static void CreateNewGoal()
    {
        Console.WriteLine("Select the goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");

        if (int.TryParse(Console.ReadLine(), out int goalType))
        {
            Console.Write("Enter the goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the goal point value: ");
            int points = int.Parse(Console.ReadLine());

            switch (goalType)
            {
                case 1:
                    goals.Add(new Goal(name, points));
                    break;
                case 2:
                    goals.Add(new EternalGoal(name, points));
                    break;
                case 3:
                    Console.Write("Enter the completion threshold: ");
                    int threshold = int.Parse(Console.ReadLine());
                    Console.Write("Enter the completion bonus: ");
                    int bonus = int.Parse(Console.ReadLine());
                    goals.Add(new ChecklistGoal(name, points, threshold, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid goal type.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    static void RecordEvent()
    {
        Console.WriteLine("Select a goal to record an event for:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i]}");
        }

        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex >= 1 && goalIndex <= goals.Count)
        {
            Goal selectedGoal = goals[goalIndex - 1];
            selectedGoal.Complete();
            Console.WriteLine($"{selectedGoal.Name} event recorded. You've earned {selectedGoal.Points} points!");
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    static void ListGoals()
    {
        Console.WriteLine("List of Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i]}");
        }
    }

    static void DisplayScore()
    {
        int totalScore = goals.Where(g => g.IsCompleted).Sum(g => g.Points);
        Console.WriteLine($"Your current score is: {totalScore} points");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            string[] goalLines = File.ReadAllLines("goals.txt");
            foreach (string line in goalLines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 4)
                {
                    int goalType = int.Parse(parts[0]);
                    string name = parts[1];
                    int points = int.Parse(parts[2]);
                    switch (goalType)
                    {
                        case 1:
                            goals.Add(new Goal(name, points));
                            break;
                        case 2:
                            goals.Add(new EternalGoal(name, points));
                            break;
                        case 3:
                            int threshold = int.Parse(parts[3]);
                            int bonus = int.Parse(parts[4]);
                            goals.Add(new ChecklistGoal(name, points, threshold, bonus));
                            break;
                    }
                }
            }
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                string goalLine;
                if (goal is ChecklistGoal checklistGoal)
                {
                    goalLine = $"3,{goal.Name},{goal.Points},{checklistGoal.CompletionThreshold},{checklistGoal.CompletionBonus}";
                }
                else if (goal is EternalGoal)
                {
                    goalLine = $"2,{goal.Name},{goal.Points}";
                }
                else
                {
                    goalLine = $"1,{goal.Name},{goal.Points}";
                }
                writer.WriteLine(goalLine);
            }
        }
    }
}
