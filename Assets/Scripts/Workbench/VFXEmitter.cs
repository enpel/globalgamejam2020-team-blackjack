using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class VFXEmitter : MonoBehaviour
{
    [Inject] private IRewardReceiver _rewardReceiver;
    [Inject] private ISoundEffectPlayer _soundEffectPlayer;

    [SerializeField] private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _rewardReceiver.ReceiveRewardAsObservable()
            .Subscribe(_ =>
            {
                _soundEffectPlayer.Play(SoundEffectType.GetRC);
                _particleSystem.Play();
            }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
