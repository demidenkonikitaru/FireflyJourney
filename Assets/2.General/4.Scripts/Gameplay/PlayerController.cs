using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoSingleton<PlayerController> {

    [Tooltip("Controls horizontal movement sensitivity.")]
    [SerializeField]
    private float movementSpeed;

    public float tiltStrength;

    [Tooltip("Controls where is the edge of the level where player is trasfered on the opposite side of the level.")]
    [SerializeField]
    private float xAxisMovementConstraints;

    private float movement = 0f;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
#if UNITY_EDITOR
        movement = Input.GetAxis("Horizontal") * movementSpeed;
#endif

        movement = Input.acceleration.x * movementSpeed * tiltStrength;

    }

    private void FixedUpdate()
    {
        HorizontalMovementControl();

        DetectBoundryCross();
    }

    private void HorizontalMovementControl() //controls player horizontal movement
    {
        Vector2 velocity = rb.velocity; //write players velocity into variable
        velocity.x = movement;          //rewrite velocity according with jump force 
        rb.velocity = velocity;         //assinging velocity to actual player velocity
    }

    /* Summary: Responce to tilt movement on smarthones. Moves the player. */
    private void TiltMovement()
    {
        float xTilt = Input.acceleration.x;
        Vector2 velocity = rb.velocity;
        velocity.x = xTilt;           
        rb.velocity = velocity;
    }

    private void DetectBoundryCross() //keeps the player inside boundries if player crosses the boundary he is being transfered to the opposide side of the level
    {
        if (transform.position.x < (-xAxisMovementConstraints)) 
        {
            transform.position = new Vector2(transform.position.x + (xAxisMovementConstraints * 2f), 
                transform.position.y);
        }
        else if (transform.position.x > xAxisMovementConstraints)
        {
            transform.position = new Vector2(transform.position.x - xAxisMovementConstraints * 2f, 
                transform.position.y);
        }
    }

    public void FreezePlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    public void UnfreezePlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
