using System;

class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;

    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public override string ToString()
    {
        return $"{streetAddress}, {city}, {state}, {country}";
    }
}

class Event
{
    protected string eventTitle;
    protected string eventDescription;
    protected DateTime eventDate;
    protected string eventTime; // Changed the type to string
    protected Address eventAddress;

    public Event(string title, string description, DateTime date, string time, Address address) // Updated the constructor
    {
        eventTitle = title;
        eventDescription = description;
        eventDate = date;
        eventTime = time;
        eventAddress = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {eventTitle}\nDescription: {eventDescription}\nDate: {eventDate:d}\nTime: {eventTime}\nAddress: {eventAddress}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return "";
    }
}

class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Lecture: {eventTitle}, {eventDate:d}";
    }
}

class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Reception: {eventTitle}, {eventDate:d}";
    }
}

class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }

    public override string GetShortDescription()
    {
        return $"Outdoor Gathering: {eventTitle}, {eventDate:d}";
    }
}

class Program
{
    static void Main()
    {
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Elm St", "Los Angeles", "CA", "USA");
        Address address3 = new Address("789 Oak St", "Miami", "FL", "USA");

        Lecture lecture = new Lecture("C# Basics", "Learn the basics of C# programming", DateTime.Now, "14:30", address1, "John Doe", 50);
        Reception reception = new Reception("Company Party", "Join us for a fun evening", DateTime.Now, "19:00", address2, "rsvp@example.com");
        OutdoorGathering gathering = new OutdoorGathering("Picnic in the Park", "Enjoy a picnic in the park", DateTime.Now, "12:00", address3, "Sunny with a high of 75°F");

        Console.WriteLine("Lecture Details:\n" + lecture.GetFullDetails());
        Console.WriteLine("\nReception Details:\n" + reception.GetFullDetails());
        Console.WriteLine("\nOutdoor Gathering Details:\n" + gathering.GetFullDetails());

        Console.WriteLine("\nShort Descriptions:");
        Console.WriteLine(lecture.GetShortDescription());
        Console.WriteLine(reception.GetShortDescription());
        Console.WriteLine(gathering.GetShortDescription());
    }
}
