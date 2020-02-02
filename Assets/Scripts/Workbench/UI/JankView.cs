using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankView : MonoBehaviour,IJankView
{
    public void SetData(RepairOrder order)
    {
    }
}

public interface IJankView
{
    void SetData(RepairOrder order);
}

