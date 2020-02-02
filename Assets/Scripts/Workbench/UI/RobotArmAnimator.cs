using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RobotArmAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOLocalJump(new Vector3(0, -0.112f, 0), 0.005f, 1, 1.0f)
            .SetDelay(0.3f).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
