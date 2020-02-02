using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AllRepairedView : MonoBehaviour
{
    [Inject] private IWorkbench _workbench;
    // Start is called before the first frame update
    void Start()
    {
        _workbench.IsAllRepairedAsObservable()
            .Take(1)
            .Subscribe(_ => { SceneManager.LoadScene("Result");}).AddTo(this);
    }

    
}
