using System;
using System.Collections.Generic;

class Activity
{
    private DateTime date;
    protected int duration; // in minutes

    public Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    public virtual double GetDistance()
    {
        return 0.0; // Base class does not store distance
    }

    public virtual double GetSpeed()
    {
        return 0.0; // Base class does not store speed
    }

    public virtual double GetPace()
    {
        return 0.0; // Base class does not store pace
    }

    public string GetSummary()
    {
        return $"{date:d} {GetType().Name} ({duration} min) - Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

class Running : Activity
{
    private double distance; // in miles

    public Running(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / duration) * 60; // miles per hour
    }

    public override double GetPace()
    {
        return 60 / GetSpeed(); // minutes per mile
    }
}

class StationaryBicycle : Activity
{
    private double speed; // in mph

    public StationaryBicycle(DateTime date, int duration, double speed)
        : base(date, duration)
    {
        this.speed = speed;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetDistance()
    {
        return speed * (duration / 60); // miles
    }

    public override double GetPace()
    {
        return 60 / speed; // minutes per mile
    }
}

class Swimming : Activity
{
    private int laps; // number of laps

    public Swimming(DateTime date, int duration, int laps)
        : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return (laps * 50) / 1000; // kilometers
    }

    public override double GetSpeed()
    {
        return (GetDistance() / (duration / 60)); // kilometers per hour
    }

    public override double GetPace()
    {
        return 60 / GetSpeed(); // minutes per kilometer
    }
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>();

        Activity running = new Running(DateTime.Now, 30, 3.0);
        Activity cycling = new StationaryBicycle(DateTime.Now, 45, 20.0);
        Activity swimming = new Swimming(DateTime.Now, 60, 30);

        activities.Add(running);
        activities.Add(cycling);
        activities.Add(swimming);

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
