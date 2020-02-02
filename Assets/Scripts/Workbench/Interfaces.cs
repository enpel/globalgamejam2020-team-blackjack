using System;
using UniRx;

public interface IRepairOrderProvider
{

    RepairOrder RequestRepairOrder(OrderRequestType orderType);
}

public interface IRepairedJankReceiver
{
    IObservable<Unit> ReceivedRepairedJank();
}


public interface IWorkbench
{
    void Repair(RepairPower power);
    IObservable<RepairOrder> ChangeCurrentRepairOrder();
    IObservable<RepairState> UpdateRepairStateAsObservable();
    void RequestPreviewOrder();
    void RequestNextOrder();
    bool IsChallenging { get; }
    Screw CurrentRepairOrderScrew { get; }
    IObservable<Unit> IsAllRepairedAsObservable();
}

public interface IRewardReceiver
{
    IObservable<Reward> ReceiveRewardAsObservable();
}
