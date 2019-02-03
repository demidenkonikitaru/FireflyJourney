using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailParticleSystemBehaviour : MonoBehaviour {

    /* This script provides behaviour for player trails particle system. 
     *  It follows player if its position higher then position of this particle system.*/

    [Tooltip("Target to follow.")]
    [SerializeField]
    private Transform targetToFollow;

    //__________Particle system parameters___________

    #region Particle system parameters.
    [Header("Particle system parameters.")]

    [Tooltip("Particle system of trail object in children.")]
    [SerializeField]
    private ParticleSystem trailParticleSystem;

    //contains emission over distance rate from trail particle system
    private bool emissionEnabled;

    [Tooltip("Highest Y position that was achieved.")]
    [SerializeField]
    private float topYPosition;

    [Tooltip("Current Y position.")]
    [SerializeField]
    private float currentYPosition;
    #endregion

    //____________Default methods__________________

    #region Default methods.
    void Start () {
        InitializeVariables();
	}
	
	void Update () {
        //follow player if he is above this object
        FollowPlayer();
    }

    void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }

    #endregion

    //__________________________Custom methods_________________________

    #region Custom methods

    //used for variable initialization
    private void InitializeVariables()
    {
        //assigns player transform to variable
        targetToFollow = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //assigns particle system component to variable
        trailParticleSystem = GetComponent<ParticleSystem>();

        //assigns rateOverDistance of particle system to variable
        emissionEnabled = trailParticleSystem.emission.enabled;

        //assigns current start y position to top
        topYPosition = transform.position.y;

        //assigns current start y position to currentYPosition
        currentYPosition = transform.position.y;
    }

    //cotantly follows player position
    private void FollowPlayer()
    {
        //set particle system position to players position
        transform.position = new Vector3(targetToFollow.position.x, targetToFollow.position.y, transform.position.z);
    }
    
    //checks if velocity goes up or down. If down, switches off particle system emission
    private void CompareCurrentYPositionWithHighest()
    {

        Debug.Log("Method works.");

        //assign emission value to local variable
        var emission = trailParticleSystem.emission;

        //track current position
        currentYPosition = transform.position.y;

        if(currentYPosition > topYPosition) //if current position larger than top
        {
            Debug.Log("Highest position is overwritten.");
            topYPosition = currentYPosition;
            
            emission.enabled = true; //enable emission
        }
        else if (currentYPosition < topYPosition) //if current position smaller than top
        {
            emission.enabled = false; //dissable emission
        }
    }

    #endregion
}
