using UnityEngine;
using TMPro;
using Zenject;
using UniRx;

public class MoneyManager : MonoBehaviour
{
    [Inject] IRewardReceiver receiver;
    public static MoneyManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        receiver.ReceiveRewardAsObservable()
            .Subscribe(reward=> Gain(reward.Value.Value))
            .AddTo(this);
    }

    public TextMeshProUGUI scoreText;

    private int currentAmount = 0;

    public void Gain(int amount)
    {
        currentAmount += amount;
        RefreshDisplay();
    }

    public void Pay(int amount)
    {
        currentAmount -= amount;
        RefreshDisplay();
    }

    public int GetCurrentAmount()
    {
        return currentAmount;
    }

    public void RefreshDisplay()
    {
        UpgradeManager.Instance.CanBuyUpgrades();
        scoreText.SetText(currentAmount.ToString());
    }
}
