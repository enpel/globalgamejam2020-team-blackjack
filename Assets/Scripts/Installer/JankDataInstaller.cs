using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class JankDataInstaller : MonoInstaller<JankDataInstaller>
{
    [SerializeField] private JankData[] jankData;
    public override void InstallBindings()
    {
        Container.Bind<JankData[]>().FromInstance(jankData).AsCached().NonLazy();
        Container.Bind<JankDataHolder>().AsSingle();
    }
}
