using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundInstaller : MonoInstaller<SoundInstaller>
{
    [SerializeField] private SoundEffectData[] _soundEffects;
    [SerializeField] private AudioSource _source;
    public override void InstallBindings()
    {
        Container.Bind<SoundEffectData[]>().FromInstance(_soundEffects).AsCached();
        Container.Bind<AudioSource>().FromInstance(_source).AsCached();
        
        Container.BindInterfacesTo<SoundPlayer>().AsSingle();
    }
}
