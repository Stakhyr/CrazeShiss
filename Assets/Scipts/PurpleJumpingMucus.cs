using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleJumpingMucus : MonoBehaviour
{
    public bool debugMode=true;

    private GameObject player;
    public Rigidbody2D mainBody;
    private SpriteRenderer rend;
    private Animator anim;

    public float jumpDist = 300f;
    public float jumpTime = 4f;
    private float jumpTimer;
    public float jumpSpeed =1;
    private float speedTimer;

    public float playerDetectRange = 10f;

    private bool hasJumped;
    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        mainBody = GetComponentInParent<Rigidbody2D>();
        rend = GetComponentInParent<SpriteRenderer>();
        anim = GetComponentInParent<Animator>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(debugMode == true) 
        {
            if (Input.GetKeyDown(KeyCode.Z)) 
            {
                hasJumped = true;
            }
        }
        else 
        {
            if (isDead == false) 
            {
                jumpTimer += Time.deltaTime;

                if (jumpTimer >= jumpTime) 
                {
                    hasJumped = true;
                }
            }
        }

        if (hasJumped == true) 
        {
            Jump();
        }

        if (isDead == true) 
        {
            Die();
        }
        
    }


    void Jump()
    {
        if (hasJumped == true) 
        {
            //find the direction of the player
            Vector2 direction = transform.position - player.transform.position;
            // find the distance of the player
            float distance = Vector2.Distance(transform.position, player.transform.position);

            //set the animation to jumped
            //anim.SetBool("hasJumped", true);

            // timer of the about to jumpanimation before the actual jump
            speedTimer += Time.deltaTime;

            if (speedTimer >= jumpSpeed) 
            {
                mainBody.AddForce(Vector2.up * jumpDist);

                //if the player is within range, check to see what direction he is then jump towards the player
                if (direction.x > 0 && distance < playerDetectRange)
                {
                    mainBody.AddForce(Vector2.left * (jumpDist / 3));

                    rend.flipX = false;
                }

                else if (direction.x > 0 && distance < playerDetectRange)
                {
                    mainBody.AddForce(Vector2.right * (jumpDist / 3));

                    rend.flipX = true;
                }
                else
                {
                    if (UnityEngine.Random.value > 0.50f)
                    {
                        mainBody.AddForce(Vector2.right * (jumpDist / 3));

                        rend.flipX = false;
                    }
                    else
                    {
                        mainBody.AddForce(Vector2.left * (jumpDist / 3));
                        rend.flipX = false;
                    }
                }

                //anim.SetBool("hasJumped", false);
                speedTimer = 0;
                jumpTimer = 0;
                hasJumped = false;

            }
            


        }
    }

    void Die() 
    {
        anim.SetBool("isDead", true);

        GetComponentInParent<CircleCollider2D>().enabled = false;
        mainBody.gameObject.GetComponentInParent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "spikes") 
        {
            isDead = true;
        }

        if(collision.tag=="Player" || collision.tag == "saw") 
        {
            isDead = true;
            if (collision.tag == "Player") 
            {
                Debug.Log("Colision");
                collision.SendMessage("Hit");
            }
        }

        if (collision.tag == "GarbageCollector") 
        {
            Destroy(mainBody.gameObject);
        }
    }


}
