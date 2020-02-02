using UnityEngine;
using Zenject;

public class ClickManager : MonoBehaviour
{
    [Inject] IWorkbench workbench;

    public static ClickManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InvokeRepeating("AutoClick", 1f, 1f);
    }

    public void AutoClick()
    {
        workbench.Repair(new RepairPower(UpgradeManager.Instance.GetAutoClickPower()));
    }

    public void Click()
    {
        workbench.Repair(new RepairPower(UpgradeManager.Instance.GetClickPower()));
    }
}
