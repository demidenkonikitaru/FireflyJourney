using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGrabber : MonoBehaviour {

    /* Summary: Collects all the platforms to pool. */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PooledObject")
        {           
            LevelGenerator.Instance.DistributePlatform(collision);
        }
    }
}
