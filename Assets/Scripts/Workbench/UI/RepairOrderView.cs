using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RepairOrderView : MonoBehaviour
{
    [Inject] private IWorkbench _workbench;

    [SerializeField] private Image repairImage;

    [SerializeField] private Text orderName;

    // Start is called before the first frame update
    void Awake()
    {
        repairImage.materialForRendering.SetFloat("Vector1_6967C50C", 0.0f);
    }

    void Start()
    {
        orderName.gameObject.SetActive(false);
        _workbench.ChangeCurrentRepairOrder()
            .Subscribe(x =>
            {
                repairImage.sprite = x.OrderImage;
                var aspectRatio = (float)x.OrderImage.texture.width / (float)x.OrderImage.texture.height;
                repairImage.rectTransform.localScale = new Vector3(aspectRatio,1.0f,0);
                repairImage.materialForRendering.mainTexture = x.OrderImage.texture;
                repairImage.materialForRendering.SetFloat("Vector1_5111E148", 5.0f + x.Jank.Level.Value);
                
            })
            .AddTo(this);


        _workbench.UpdateRepairStateAsObservable().Subscribe(UpdateRepairState).AddTo(this);
    }

    private void UpdateRepairState(RepairState state)
    {
        repairImage.materialForRendering.SetFloat("Vector1_6967C50C", 1.0f - state.RepairRatio);
    }
}