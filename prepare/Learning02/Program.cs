using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now;
    }
     public override string ToString()
    {
        return $"{Date:yyyy-MM-dd HH:mm:ss} - Prompt: {Prompt}\nResponse: {Response}\n";
    }
}
class Journal
{
    private List<Entry> entries = new List<Entry>();
    private Random random = new Random();

    public void AddEntry()
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
        string randomPrompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine("Prompt: " + randomPrompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry entry = new Entry(randomPrompt, response);
        entries.Add(entry);
        Console.WriteLine("Entry saved successfully.");
    }
     public void DisplayEntries()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }
    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine($"{entry.Date:yyyy-MM-dd HH:mm:ss}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved to file successfully.");
    }
    public void LoadFromFile(string fileName)
    {
        entries.Clear();

        if (File.Exists(fileName))
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        DateTime date;
                        if (DateTime.TryParse(parts[0], out date))
                        {
                            Entry entry = new Entry(parts[1], parts[2])
                            {
                                Date = date
                            };
                            entries.Add(entry);
                        }
                    }
                }
        }
        Console.WriteLine("Journal loaded from file successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
class Program
{
    static void Main()
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        journal.AddEntry();
                        break;
                    case 2:
                        journal.DisplayEntries();
                        break;
                    case 3:
                        Console.Write("Enter filename to save: ");
                        string saveFileName = Console.ReadLine();
                        journal.SaveToFile(saveFileName);
                        break;
                    case 4:
                        Console.Write("Enter filename to load: ");
                        string loadFileName = Console.ReadLine();
                        journal.LoadFromFile(loadFileName);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid menu option.");
            }
        }
    }
}
