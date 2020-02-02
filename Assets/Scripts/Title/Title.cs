using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.OnClickAsObservable()
            .Take(1)
            .Subscribe(_ => { LoadMainScene(); }).AddTo(this);
    }

    async UniTask LoadMainScene()
    {
        await SceneManager.LoadSceneAsync("Main");
        await SceneManager.LoadSceneAsync("Workbench", LoadSceneMode.Additive);
    }
}