using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfMucus : MonoBehaviour
{
    public bool debugMode;
    public GameObject rightEdge, leftEdge;
    private Animator anim;
   

    public float moveSpeed = 0.1f;
    public float chaseSpeed = 0.10f;
    public float stopTime = 1;
    [Range(0.0f, 1f)]
    public float chaserChance = 0.5f;
    public float chaserDistance = 3;
    public Rigidbody2D mainBody;

    private float move, timer;

    private bool isStopped, isRight, isChasing, isChaser;

    private int maskLayer = 9;

    // Start is called before the first frame update
    void Start()
    {
        mainBody = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();

        if (Random.value < chaserChance) 
        {
            isChaser = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position);

        Vector2 direction = transform.position - GameObject.Find("Player").transform.position;

        if (debugMode == true) 
        {
            Debug.DrawRay(leftEdge.transform.position, Vector2.down, Color.red);
            leftEdge.GetComponent<MeshRenderer>().enabled = true;

            Debug.DrawRay(rightEdge.transform.position, Vector2.down, Color.red);
            rightEdge.GetComponent<MeshRenderer>().enabled = true;
        }

        mainBody.MovePosition(mainBody.position + Vector2.left * move);

        if (isChaser == true && distance < chaserDistance) 
        {
            if (direction.x > 0) 
            {
                isRight = false;
                move = chaseSpeed;
            }
            else 
            {
                isRight = true;
                move = -chaseSpeed;
            }
            //anim.setBool("isChasing",true);

        }
        else 
        {
            //anim.setBool("isChasing", true);
            if (isStopped == true)
            {
                move = 0;
                timer += Time.deltaTime;
                if (timer >= stopTime)
                {
                    isStopped = false;
                    timer = 0;
                }
            }
            else
            {
                if (isRight == false)
                {
                    move = moveSpeed;
                }
                else
                {
                    move = -moveSpeed;
                }
            }
        }

       



        RaycastHit2D hitLeft = Physics2D.Raycast(leftEdge.transform.position, Vector2.down, 3f, 5<<maskLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rightEdge.transform.position, Vector2.down, 0.7f, 1 << maskLayer);

        if (isRight == false)
        {
            if (hitLeft.collider == null)
            {
                if (debugMode == true)
                {
                    print("Left Detected");
                }
                isStopped = true;
                move = 0;
                if (isChasing == false)
                {
                    isRight = true;

                }
            }
        }
        if (isRight == true)
        {
            if (hitRight.collider == null)
            {
                if (debugMode == true) 
                {
                    print("Right Detected");
                }
                isStopped = true;
                move = 0;
                if (isChasing == false) 
                {
                    isRight = false;

                }
            }
        }
       
        
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.name == "Player")
    //    {
    //        Debug.Log("Hit");
    //        collision.gameObject.SendMessage("Damage");
    //        collision.gameObject.SendMessage("Hit");

    //    }

    //    if (collision.collider.tag == "boxCreate")
    //    {
    //        Destroy(collision.gameObject);
    //    }

    //    if (collision.collider.tag == "Saw")
    //    {
    //        Destroy(collision.gameObject);
    //    }

    //    if (collision.collider.tag == "PurpleMucus")
    //    {
    //        Destroy(collision.gameObject);
    //    }


    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("Hit");
            other.gameObject.SendMessage("Damage");
            other.gameObject.SendMessage("Hit");
        }
    }
   
}
