using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBoxController : MonoBehaviour
{
    [SerializeField] private float timeToDestroyObjectBox=5f;
    [SerializeField] private float positionYToDestroyObject=-20f;
    // Update is called once per frame
    void Update()
    {
        DetroyObjectBox();
        DestroyObjectBoxAfterTime();
    }
    private void DetroyObjectBox()
    {
        if (transform.position.y<=positionYToDestroyObject)
        {
            Destroy(this.gameObject);
        }
    }
    private void DestroyObjectBoxAfterTime()
    {
        Destroy(this.gameObject, timeToDestroyObjectBox);
    }
}
