using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System.Collections;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance { get; private set; }
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

    public Button instantRepairPowerButton;
    public Button doubleClickPowerButton;
    public Button doubleAutoClickPowerButton;
    public TextMeshProUGUI clickPowerDurationText;
    public TextMeshProUGUI autoClickPowerDurationText;
    public Transform clickPowerCooldownImage;
    public Transform autoClickPowerCooldownImage;
    public Transform instantRepairPowerCooldownImage;

    private bool doubleClickPowerEnabled = false;
    private bool doubleAutoClickPowerEnabled = false;

    private float instantRepairPowerCooldown = 30f;
    private int clickPowerDuration = 10;
    private int autoClickPowerDuration = 10;
    private float clickPowerCooldown = 30f;
    private float autoClickPowerCooldown = 30f;

    [Inject] IWorkbench workbench;

    public void InstantRepairPower()
    {
        workbench.Repair(new RepairPower(Mathf.FloorToInt(0.2f * workbench.CurrentRepairOrderScrew.Value)));
        instantRepairPowerButton.interactable = false;
        InvokeRepeating("InstantRepairCooldown", 1f, 1f);
    }

    private void InstantRepairCooldown()
    {
        if (instantRepairPowerCooldown == 1f)
        {
            instantRepairPowerCooldown = 30f;
            instantRepairPowerButton.interactable = true;
            instantRepairPowerCooldownImage.localScale = new Vector2(0f, 1f);
            CancelInvoke("InstantRepairCooldown");
        } else
        {
            instantRepairPowerCooldownImage.localScale = new Vector2(1f - (--instantRepairPowerCooldown / 30f), 1f);
        }
    }

    public void DoubleClickPower()
    {
        doubleClickPowerButton.interactable = false;
        doubleClickPowerEnabled = true;
        UpgradeManager.Instance.DisplayClickPower();
        clickPowerDurationText.SetText("10");
        InvokeRepeating("DisplayClickPowerDuration", 1f, 1f);
        StartCoroutine(ClickDuration());
    }

    private IEnumerator ClickDuration()
    {
        yield return new WaitForSeconds(10);
        doubleClickPowerEnabled = false;
        UpgradeManager.Instance.DisplayClickPower();
        clickPowerDuration = 10;
        CancelInvoke("DisplayClickPowerDuration");
        InvokeRepeating("ClickPowerCooldown", 1f, 1f);
    }

    private void DisplayClickPowerDuration()
    {
        if (doubleClickPowerEnabled)
        {
            clickPowerDurationText.SetText(--clickPowerDuration > 0 ? clickPowerDuration.ToString() : "");
        }
    }

    private void ClickPowerCooldown()
    {
        if (clickPowerCooldown == 1f)
        {
            clickPowerCooldown = 30f;
            doubleClickPowerButton.interactable = true;
            clickPowerCooldownImage.localScale = new Vector2(0f, 1f);
            CancelInvoke("ClickPowerCooldown");
        } else
        {
            clickPowerCooldownImage.localScale = new Vector2(1f - (--clickPowerCooldown / 30f), 1f);
        }
    }

    public void DoubleAutoClickPower()
    {
        doubleAutoClickPowerButton.interactable = false;
        doubleAutoClickPowerEnabled = true;
        UpgradeManager.Instance.DisplayAutoClickPower();
        autoClickPowerDurationText.SetText("10");
        InvokeRepeating("DisplayAutoClickPowerDuration", 1f, 1f);
        StartCoroutine(AutoClickDuration());
    }

    private IEnumerator AutoClickDuration()
    {
        yield return new WaitForSeconds(10);
        doubleAutoClickPowerEnabled = false;
        UpgradeManager.Instance.DisplayAutoClickPower();
        autoClickPowerDuration = 10;
        CancelInvoke("DisplayAutoClickPowerDuration");
        InvokeRepeating("AutoClickPowerCooldown", 1f, 1f);
    }

    private void DisplayAutoClickPowerDuration()
    {
        if (doubleAutoClickPowerEnabled)
        {
            autoClickPowerDurationText.SetText(--autoClickPowerDuration > 0 ? autoClickPowerDuration.ToString() : "");
        }
    }

    private void AutoClickPowerCooldown()
    {
        if (autoClickPowerCooldown == 1f)
        {
            autoClickPowerCooldown = 30f;
            doubleAutoClickPowerButton.interactable = true;
            autoClickPowerCooldownImage.localScale = new Vector2(0f, 1f);
            CancelInvoke("AutoClickPowerCooldown");
        } else
        {
            autoClickPowerCooldownImage.localScale = new Vector2(1f - (--autoClickPowerCooldown / 30f), 1f);
        }
    }

    public bool IsDoubleClickPowerEnabled()
    {
        return doubleClickPowerEnabled;
    }

    public bool IsDoubleAutoClickPowerEnabled()
    {
        return doubleAutoClickPowerEnabled;
    }
}
