using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [Tooltip("Target to follow.")]
    [SerializeField]
    private Transform targetToFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveCameraUp();
	}

    private void MoveCameraUp()
    {
        if (targetToFollow.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, targetToFollow.position.y, transform.position.z);
        }
    }
}
