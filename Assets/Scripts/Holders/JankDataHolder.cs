using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx.Async;
using UnityEngine;

public class JankDataHolder
{
    public List<RepairOrder> SortedRepairOrders { get; }

    public JankDataHolder(JankData[] janks)
    {
        var sourtedJankDataList = janks.OrderBy(x => x.Jank.Level.Value).ToList();
        SortedRepairOrders = sourtedJankDataList.Select(((data, i) => new RepairOrder(data.Jank, data.Sprite, i, i+1 == janks.Length)))
            .ToList();
    }
}
