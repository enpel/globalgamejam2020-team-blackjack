using UniRx;
using UnityEngine;
using Zenject;

public class RepairSoundPlayer : MonoBehaviour
{
    [Inject] private ISoundEffectPlayer _soundEffectPlayer;

    [Inject] private IWorkbench _workbench;
    // Start is called before the first frame update
    void Start()
    {

        _workbench.UpdateRepairStateAsObservable()
            .Select(x => x.TotalRepairPower)
            .Pairwise()
            .Where(x => x.Previous.Value < x.Current.Value)
            .Subscribe(_ => _soundEffectPlayer.Play(SoundEffectType.Tap))
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
