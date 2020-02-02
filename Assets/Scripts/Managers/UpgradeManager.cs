using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DisplayClickCost();
        DisplayAutoClickCost();
    }

    public Button clickUpgradeButton;
    public Button autoClickUpgradeButton;
    public TextMeshProUGUI clickCostText;
    public TextMeshProUGUI autoClickCostText;
    public TextMeshProUGUI clickPowerText;
    public TextMeshProUGUI autoClickPowerText;

    private int clickPower = 1;
    private int autoClickPower = 0;
    private int currentClickLevelCost = 5;
    private int currentAutoClickLevelCost = 5;

    public void UpgradeClick()
    {
        clickPower++;
        currentClickLevelCost = currentClickLevelCost + 5;
        MoneyManager.Instance.Pay(currentClickLevelCost - 5);
        DisplayClickCost();
        DisplayClickPower();
        
        // 
        var upgrader = FindObjectOfType<ArmUpgrader>();
        if (upgrader != null)
        {
            var grade = (clickPower / 10);
            if (grade >= 4) grade = 3;
            upgrader.SetArmGrade(grade);
        }
    }

    public void UpgradeAutoClick()
    {
        autoClickPower++;
        currentAutoClickLevelCost = currentAutoClickLevelCost + 5;
        MoneyManager.Instance.Pay(currentAutoClickLevelCost - 5);
        DisplayAutoClickCost();
        DisplayAutoClickPower();
    }

    public void CanBuyUpgrades()
    {
        clickUpgradeButton.interactable = MoneyManager.Instance.GetCurrentAmount() >= currentClickLevelCost;
        autoClickUpgradeButton.interactable = MoneyManager.Instance.GetCurrentAmount() >= currentAutoClickLevelCost;
    }

    public void DisplayClickPower()
    {
        clickPowerText.SetText(GetClickPower().ToString());
    }

    public void DisplayAutoClickPower()
    {
        autoClickPowerText.SetText(GetAutoClickPower().ToString());
    }

    private void DisplayClickCost()
    {
        clickCostText.SetText(currentClickLevelCost.ToString());
    }

    private void DisplayAutoClickCost()
    {
        autoClickCostText.SetText(currentAutoClickLevelCost.ToString());
    }

    public int GetClickPower()
    {
        return PowerManager.Instance.IsDoubleClickPowerEnabled() ? clickPower * 2 : clickPower;
    }

    public int GetAutoClickPower()
    {
        return PowerManager.Instance.IsDoubleAutoClickPowerEnabled() ? autoClickPower * 2 : autoClickPower;
    }
}
