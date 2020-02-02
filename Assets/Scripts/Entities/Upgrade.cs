using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public DisplayName Name { get; }
    public Price Price { get; }
    public UpgradeType Type { get; }

    public Upgrade(DisplayName name, Price price, UpgradeType type)
    {
        Name = name;
        Price = price;
        Type = type;
    }
}

public class Price
{
    public int Value { get; }

    public Price(int value)
    {
        Value = value;
    }
}