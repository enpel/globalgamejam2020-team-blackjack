using System;
using UnityEngine;

[Serializable]
public class Jank
{
    public DisplayName Name { get; }
    public Screw Screw { get; }
    public Level Level { get; }
    public Reward Reward { get; }

    public Jank(DisplayName name, Screw screw, Level level, Reward reward)
    {
        Name = name;
        Screw = screw;
        Level = level;
        Reward = reward;
    }
}

[Serializable]
public class DisplayName
{
    public string Name { get; }

    public DisplayName(string name)
    {
        Name = name;
    }
}

[Serializable]
public class Screw
{
    public int Value { get; }

    public Screw(int value)
    {
        Value = value;
    }
}

public class Level
{
    public int Value { get; }

    public Level(int value)
    {
        Value = value;
    }
}

[Serializable]
public class Reward
{
    public RepairCoin Value { get; }

    public Reward(RepairCoin value)
    {
        Value = value;
    }
}

public class RepairCoin
{
    public int Value { get; }

    public RepairCoin(int value)
    {
        Value = value;
    }
}