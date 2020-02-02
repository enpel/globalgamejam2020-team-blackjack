using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public enum OrderRequestType
{
    Prev,
    Current,
    Next
}

public class RepairOrderProvider : IRepairOrderProvider, IInitializable, IDisposable
{
    public JankDataHolder Holder { get; }
    public IRepairedJankReceiver Receiver { get; }

    private CompositeDisposable _disposable;
    private List<RepairOrder> sortedJankList;
    private int currentIndex = 0;

    public RepairOrderProvider(JankDataHolder holder, IRepairedJankReceiver receiver)
    {
        Holder = holder;
        Receiver = receiver;
        _disposable = new CompositeDisposable();
        sortedJankList = Holder.SortedRepairOrders;
    }

    public RepairOrder RequestRepairOrder(OrderRequestType orderRequestType)
    {
        var diff = orderRequestType == OrderRequestType.Prev ? -1 :
            orderRequestType == OrderRequestType.Next ? 1 :
            0;
        
        return GetNextRepairOrder(diff);
    }

    private RepairOrder GetNextRepairOrder(int diff)
    {
        currentIndex += diff;
        if (currentIndex < 0) currentIndex = 0;
        if (currentIndex >= sortedJankList.Count) currentIndex = sortedJankList.Count - 1;
        var nextJank = sortedJankList.ElementAt(currentIndex);

        return nextJank;
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }

    public void Initialize()
    {
        
    }
}

public class RepairedJankReceiver : IRepairedJankReceiver
{
    public IObservable<Unit> ReceivedRepairedJank()
    {
        return Observable.ReturnUnit();
    }
}