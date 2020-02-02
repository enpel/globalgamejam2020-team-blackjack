using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScrewView : MonoBehaviour,IScrewView
{
    [Inject] private IWorkbench _workbench;
    [SerializeField] private Image screwBar;

    private void Start()
    {
        _workbench.UpdateRepairStateAsObservable().Subscribe(SetRepairState).AddTo(this);
    }

    public void SetRepairState(RepairState state)
    {
        screwBar.fillAmount = state.RepairRatio;
    }
}


public interface IScrewView
{
    void SetRepairState(RepairState state);
}