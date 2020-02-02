using UnityEngine;

public class RepairOrder
{
    public Jank Jank { get; }
    public Sprite OrderImage { get; }
    public int OrderNumber { get; }
    public bool IsFinalOrder { get; }
    public Reward Reward { get; }

    public RepairOrder(Jank jank, Sprite orderImage, int orderNumber, bool isFinalOrder)
    {
        Jank = jank;
        OrderImage = orderImage;
        OrderNumber = orderNumber;
        IsFinalOrder = isFinalOrder;
        Reward = jank.Reward;
    }
}