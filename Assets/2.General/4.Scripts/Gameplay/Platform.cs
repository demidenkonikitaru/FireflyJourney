using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    [Tooltip("Force of player jumping.")]
    [SerializeField]
    private float jumpForce = 10f;

    //container for game manager
    private GameObject gameManager;

    [Tooltip("Animator for platform glow.")]
    [SerializeField]
    private Animator glowAnimator;


	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
	

	void Update () {
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.relativeVelocity.y <= 0f) //if player fals on platform from above
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>(); //get rigidbody from player
            if (rb != null)
            {
                //write players velocity into variable
                Vector2 velocity = rb.velocity;

                //rewrite velocity according with jump force
                velocity.y = jumpForce;

                //assinging velocity to actual player velocity
                rb.velocity = velocity;         

                //calls jump animation
                collision.gameObject.GetComponentInChildren<Animator>().Play("Jump", -1, 0f);
                //collision.gameObject.GetComponentInChildren<Animator>().Play("DefaultCharacterJump");

                glowAnimator.Play("PlatformGlow", -1, 0f);
            }
        }
    }
}
