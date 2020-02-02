using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RepairOrderListView : MonoBehaviour, IRepairOrderListView
{
    [SerializeField] private Image prevLevel;
    [SerializeField] private Image currentLevel;
    [SerializeField] private Image nextLevel;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;

    [Inject] private JankDataHolder _jankDataHolder;

    [Inject] private IWorkbench _workbench;
    [Inject] private ISoundEffectPlayer _soundEffectPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _workbench.ChangeCurrentRepairOrder()
            .Subscribe(ChangeCurrentOrder)
            .AddTo(this);

        prevButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _soundEffectPlayer.Play(SoundEffectType.SelectButton);
                _workbench.RequestPreviewOrder();
            })
            .AddTo(this);
        nextButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _soundEffectPlayer.Play(SoundEffectType.SelectButton);
                _workbench.RequestNextOrder();
            })
            .AddTo(this);
    }

    public void ChangeCurrentOrder(RepairOrder order)
    {
        var currentIndex =
            _jankDataHolder.SortedRepairOrders.FindIndex(x => x.Jank.Level.Value == order.Jank.Level.Value);

        var currentOrder = _jankDataHolder.SortedRepairOrders.ElementAt(currentIndex);
        currentLevel.sprite = currentOrder.OrderImage;

        var hasPrevOrder = currentIndex - 1 >= 0;
        prevLevel.gameObject.SetActive(hasPrevOrder);
        prevButton.gameObject.SetActive(hasPrevOrder);
        if (hasPrevOrder)
        {
            var prevOrder = _jankDataHolder.SortedRepairOrders.ElementAt(currentIndex - 1);
            prevLevel.sprite = prevOrder.OrderImage;
        }

        var hasNext = currentIndex + 1 < _jankDataHolder.SortedRepairOrders.Count;
        var isOpenNextButton = hasNext && !_workbench.IsChallenging;
        nextLevel.gameObject.SetActive(hasNext);
        nextLevel.material.SetFloat("Vector1_C927CED6", isOpenNextButton ? 1.0f: 0);
        nextButton.gameObject.SetActive(isOpenNextButton);
        if (hasNext)
        {
            var nextOrder = _jankDataHolder.SortedRepairOrders.ElementAt(currentIndex + 1);
            nextLevel.sprite = nextOrder.OrderImage;
            nextLevel.material.SetTexture("_MainTex", nextOrder.OrderImage.texture);
        }
    }
}

public interface IRepairOrderListView
{
    void ChangeCurrentOrder(RepairOrder order);
}