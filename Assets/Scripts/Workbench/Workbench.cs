using System;
using UniRx;
using Zenject;

public class Workbench : IWorkbench, IInitializable, IRewardReceiver, IDisposable
{
    private readonly RepairState.Factory _repairStateFactory;
    private IRepairOrderProvider Provider { get; }
    private Subject<Reward> _rewardSubject;
    private Subject<RepairState> updateRepairStateSubject = new Subject<RepairState>();
    private ReactiveProperty<RepairState> currentOrderState = new ReactiveProperty<RepairState>();
    private ReactiveProperty<RepairOrder> currentRepairOrder = new ReactiveProperty<RepairOrder>();
    RepairState.Factory repairStateFactory = new RepairState.Factory();
    private bool isLooping = false;
    private int nextChallengeOrderNumber = 0;
    private Subject<Unit> allRepairedSubject = new Subject<Unit>();

    public Workbench(IRepairOrderProvider provider)
    {
        _repairStateFactory = repairStateFactory;
        Provider = provider;
        _rewardSubject = new Subject<Reward>();
    }

    private void UpdateRepairOrderState()
    {
        updateRepairStateSubject.OnNext(currentOrderState.Value);
    }

    private void SetNextRepairOrder(RepairOrder repairOrder)
    {
        currentOrderState.Value = _repairStateFactory.Create(repairOrder, nextChallengeOrderNumber);
        currentRepairOrder.Value = repairOrder;
        UpdateRepairOrderState();
    }

    public void Repair(RepairPower power)
    {
        currentOrderState.Value.AddRepairPower(power);
        UpdateRepairOrderState();

        if (currentOrderState.Value.IsRepaired)
        {
            if (currentRepairOrder.Value.IsFinalOrder)
            {
                allRepairedSubject.OnNext(Unit.Default);
            }
            
            if (currentOrderState.Value.IsChallenge)
            {
                nextChallengeOrderNumber++;
            }
            _rewardSubject.OnNext(GetRewart(currentRepairOrder.Value));
            var nextRequest = isLooping ? OrderRequestType.Current : OrderRequestType.Next;
            ChangeRepairOrder(nextRequest);

        }
    }

    public IObservable<RepairOrder> ChangeCurrentRepairOrder()
    {
        return currentRepairOrder;
    }

    public IObservable<RepairState> UpdateRepairStateAsObservable()
    {
        return updateRepairStateSubject;
    }

    public void RequestPreviewOrder()
    {
        isLooping = true;
        ChangeRepairOrder(OrderRequestType.Prev);
    }

    public void RequestNextOrder()
    {
        isLooping = false;
        ChangeRepairOrder(OrderRequestType.Next);
    }

    public bool IsChallenging => currentOrderState.Value.IsChallenge;
    public Screw CurrentRepairOrderScrew => currentOrderState.Value.Screw;
    public IObservable<Unit> IsAllRepairedAsObservable()
    {
        return allRepairedSubject;
    }

    private Reward GetRewart(RepairOrder order)
    {
        return order.Reward;
    }

    private void ChangeRepairOrder(OrderRequestType type)
    {
        var repairOrder = Provider.RequestRepairOrder(type);
        SetNextRepairOrder(repairOrder);
    }

    public void Initialize()
    {
        ChangeRepairOrder(0);
    }

    public IObservable<Reward> ReceiveRewardAsObservable()
    {
        return _rewardSubject;
    }

    public void Dispose()
    {
        _rewardSubject?.Dispose();
        updateRepairStateSubject?.Dispose();
        currentOrderState?.Dispose();
        currentRepairOrder?.Dispose();
        allRepairedSubject?.Dispose();
    }
}

public class RepairState
{
    public DisplayName DisplayName { get; }
    public Screw Screw { get; }
    public RepairPower TotalRepairPower { get; private set; }
    public bool IsRepaired => Screw.Value <= TotalRepairPower.Value;
    public bool IsChallenge { get; }

    public float RepairRatio => ((float) Screw.Value - (float) TotalRepairPower.Value) / (float) Screw.Value;

    public RepairState(RepairOrder repairOrder, int challengeOrderNumber)
    {
        DisplayName = repairOrder.Jank.Name;
        Screw = repairOrder.Jank.Screw;
        TotalRepairPower = new RepairPower(0);
        IsChallenge = repairOrder.OrderNumber == challengeOrderNumber;
    }

    public void AddRepairPower(RepairPower power)
    {
        TotalRepairPower = power + TotalRepairPower;
    }

    public class Factory : IFactory<RepairOrder, int, RepairState>
    {
        public RepairState Create(RepairOrder param, int challengeNumber)
        {
            return new RepairState(param, challengeNumber);
        }
    }
}