using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControl : MonoBehaviour {

    /* This script controls sprite. Color change, position change and animations. */

    private Animator animatorComponent;

    #region Default methods.

    void Start () {
        animatorComponent = GetComponent<Animator>();
	}
	

	void Update () {
		
	}

    #endregion

    #region Custom methods.

    public void MoveStraightTo(Vector2 target, float movementSpeed)
    {

    }

    public void ChangeColor(Color startColor, Color finishColor, float duration)
    {

    }

    public void playAnimation(string stateName)
    {
        
    }

    #endregion
}
