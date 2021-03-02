using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnim : MonoBehaviour
{
    public Vector3 amount;
    public float time;

    void Start()
    {
        float randomTime = Random.Range(time - 0.3f, time + 0.3f);

        iTween.ShakeScale(gameObject, iTween.Hash(
            "amount", amount,
            "time", randomTime,
            "looptype", iTween.LoopType.loop
        ));
    }
}
