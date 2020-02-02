using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorkbenchInstaller : MonoInstaller<WorkbenchInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<RepairOrderProvider>().AsSingle();
        Container.BindInterfacesTo<Workbench>().AsSingle();
        Container.BindInterfacesTo<RepairedJankReceiver>().AsSingle();

    }
}
