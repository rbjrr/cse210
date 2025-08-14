using System;

public abstract class Activity
{
    private DateTime _date;
    private int _lengthMinutes;

    public Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    protected int GetLengthMinutes()
    {
        return _lengthMinutes;
    }

    protected DateTime GetDate()
    {
        return _date;
    }

    public abstract double GetDistance(); // km
    public abstract double GetSpeed();    // kph
    public abstract double GetPace();     // min/km

    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {this.GetType().Name} ({_lengthMinutes} min) - " +
               $"Distance: {GetDistance():0.0} km, " +
               $"Speed: {GetSpeed():0.0} kph, " +
               $"Pace: {GetPace():0.00} min/km";
    }
}
