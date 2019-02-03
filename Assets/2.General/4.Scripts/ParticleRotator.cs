using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotator : MonoBehaviour {

    [Tooltip("Rotation speed. Degrees per second.")]
    [SerializeField]
    private int degreesPerSecond;

	// Update is called once per frame
	void Update () {
        RotateThisObject();
	}

    private void RotateThisObject()
    {
        transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
    }
}
