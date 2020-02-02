using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "BlackJack/Create New UpgradeData", fileName = "UpgradeData" )]
public class UpgradeData:ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private int price;
    [SerializeField] private UpgradeType type;

    public Upgrade Upgrade => new Upgrade(new DisplayName(name), new Price(price), type);
}

public enum UpgradeType
{
    Self,
    Automation,
}