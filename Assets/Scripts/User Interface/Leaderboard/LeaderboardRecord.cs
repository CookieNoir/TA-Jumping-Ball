using System;

public class LeaderboardRecord : IComparable
{
    public string Name { get; private set; }
    public int Value { get; private set; }

    public LeaderboardRecord(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        LeaderboardRecord record = obj as LeaderboardRecord;
        if (record != null)
            return Value.CompareTo(record.Value);
        else
            throw new ArgumentException("Object is not a LeaderboardRecord");
    }
}