using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpDist = 600f;
    private Rigidbody2D playerRB;
    private SpriteRenderer render;
    private Animator animator;
    private bool m_FacingRight;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            playerRB.AddForce(Vector2.up * jumpDist);
            isGrounded = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(move * moveSpeed, playerRB.velocity.y);

        if (move < 0 && !m_FacingRight) 
        {
            Flip();
        }
        else if(move > 0 && m_FacingRight)
        {
            Flip();
        }



        if (move!=0) 
        {
            animator.SetBool("isWalking",true);
        }
        else 
        {
            animator.SetBool("isWalking", false);
        }

        if (isGrounded == false) 
        {
            animator.SetBool("hasJumped", true);
        }
        else 
        {
            animator.SetBool("hasJumped", false);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") 
        {
            isGrounded = true;
        }
    }

    public void Hit()
    {
        if (Random.value <= 0.5f) 
        {
            playerRB.AddForce(Vector2.up * (jumpDist * 1.3f));
            playerRB.AddForce(Vector2.right * (jumpDist * 1.3f));
        }
        else 
        {
            playerRB.AddForce(Vector2.up * (jumpDist * 1.3f));
            playerRB.AddForce(Vector2.left * (jumpDist * 1.3f));
        }
    }
}
