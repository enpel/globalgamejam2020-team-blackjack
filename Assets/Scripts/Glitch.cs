using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Glitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GlitchEffect().Forget();
    }

    async UniTaskVoid GlitchEffect()
    {
        var imageMaterial = this.GetComponentInChildren<Image>().materialForRendering;
        while (true)
        {
            imageMaterial.SetFloat("Vector1_2C75DEE4", 0.15f);
            var waitTime = Random.Range(0.1f, 0.2f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            
            imageMaterial.SetFloat("Vector1_2C75DEE4", 0);
            waitTime = Random.Range(0.3f, 1.0f);
            
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        }
    }
}
