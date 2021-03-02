using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMgr : MonoBehaviour
{
    public Vector3 lastPoint;

    // Start is called before the first frame update
    void Start()
    {
        lastPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            lastPoint = transform.position;
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
        }

    }

    public void Respawn()
    {
        transform.position = lastPoint;
        PlayerInfos.pi.SetHealth(3);
    }
}
