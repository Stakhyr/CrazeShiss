using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int health = 30;
    public bool debugMode;
    public Transform player;

    public float moveSpeed = 0.1f;
    public float chaseSpeed =3;
    public float stopTime = 3;
    [Range(0.0f, 1f)]
    public float chaserChance = 0.5f;
    public float chaserDistance = 3;
    public Rigidbody2D mainBody;
    SpriteRenderer render;
    public GameObject rightEdge, leftEdge;
    public GameObject rightBottomEdge, leftBottomEdge;

    private float move, timer;

    private Vector2 direction;
    RaycastHit2D hitLeft, hitRight, hitBottomLeft, hitBottomRight;

    private bool isStopped, isRight;






    // Start is called before the first frame update
    void Start()
    {
        mainBody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // show ray's, which check collision with obstacles and check platform boundaries
        showRays(debugMode);
        patrolTerritory();
        if (detectTarget() == true)
        {
            if (observeLeftObstacles(hitBottomLeft, hitLeft) == true || observeRightObstacles(hitBottomRight, hitRight) == true)
            {
                enemyStopped();
            }
            chaseTarget();
        }
    }

    private void chaseTarget()
    {
        direction = player.position - transform.position;
        direction.Normalize();


        mainBody.MovePosition((Vector2)transform.position + (direction * chaseSpeed));
        if (debugMode == true) 
        {
            print("Chaising");
        }

        //if (playerInRange() == true) 
        //{
        //    atackPlayer();
        //}
    }

    private void atackPlayer()
    {
        throw new NotImplementedException();
    }

    private bool playerInRange()
    {
        throw new NotImplementedException();
    }

    private bool detectTarget()
    {
        float distance = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position);
        if (distance < 5f)
        {
            if (debugMode == true) 
            {
                print("Target in Range");
            }
            return true;
        }
        else return false;
    }

    public void patrolTerritory()
    {
        hitLeft = Physics2D.Raycast(leftEdge.transform.position, Vector2.left);
        hitRight = Physics2D.Raycast(rightEdge.transform.position, Vector2.right);

        hitBottomLeft = Physics2D.Raycast(leftBottomEdge.transform.position, Vector2.down);

        hitBottomRight = Physics2D.Raycast(rightBottomEdge.transform.position, Vector2.down);
        mainBody.MovePosition(mainBody.position + Vector2.left * move);

        //makes a stop when the enemy approaching to the obstacle
        if (isStopped == true)
        {
            enemyStopped();
        }
        else
        {
            if (isRight == false)
            {
                move = moveSpeed;
                if (observeLeftObstacles(hitBottomLeft, hitLeft) == true)
                {
                    
                    isStopped = true;
                    move = 0;

                   
                    isRight = true;
                    Flip();

                }
            }
            if (isRight == true)
            {
                move = -moveSpeed;
                if (observeRightObstacles(hitBottomRight, hitRight) == true)
                {
                    
                    isStopped = true;
                    move = 0;

                    isRight = false;
                    Flip();


                }
            }
        }

    }

    private bool observeLeftObstacles(RaycastHit2D hitBottomLeft, RaycastHit2D hitLeft)
    {
        if (hitLeft.collider != null && hitLeft.distance <= 0.3f)
        {
            if (debugMode == true)
            {
                Debug.Log("Object in the left side!");
            }
            return true;

        }
        else if (hitBottomLeft.collider == null) 
        {
            if (debugMode == true)
            {
                Debug.Log("Abyss in the left side!");
            }
            return true;
        } 
        else return false;
    }

    private bool observeRightObstacles(RaycastHit2D hitBottomRight, RaycastHit2D hitRight)
    {
        if (hitRight.collider != null && hitRight.distance <= 0.3f)
        {
            if (debugMode == true)
            {
                Debug.Log("Object in the right side!");
            }
            return true;
        }

        else if (hitBottomRight.collider == null) 
        {
            if (debugMode == true)
            {
                Debug.Log("Abyss in the right side!");
            }
            return true;
        }
        else return false;
    }

    private void enemyStopped()
    {
        if (debugMode == true) 
        {
            print("Stop!!");
        }
        move = 0;
        timer += Time.deltaTime;
        if (timer >= stopTime)
        {
            isStopped = false;
            timer = 0;
        }
            
        

    }

    //public void TakeDamage(int damage) 
    //{
    //    health -= damage;
    //    Debug.Log(health);
    //    if (health <= 0) 
    //    {
    //        Die();
    //    }
    //}

    //public void Die() 
    //{
    //    Destroy(gameObject);
    //}

    public void showRays(bool debugMode)
    {
        if (debugMode == true)
        {
            Debug.Log("DebugMode");
            Debug.DrawRay(leftEdge.transform.position, Vector2.left, Color.red);
            leftEdge.GetComponent<MeshRenderer>().enabled = true;

            Debug.DrawRay(rightEdge.transform.position, Vector2.right, Color.red);
            rightEdge.GetComponent<MeshRenderer>().enabled = true;

            Debug.DrawRay(leftBottomEdge.transform.position, Vector2.down, Color.green);
            leftBottomEdge.GetComponent<MeshRenderer>().enabled = true;

            Debug.DrawRay(rightBottomEdge.transform.position, Vector2.down, Color.green);
            rightBottomEdge.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void Flip()
    {
        if (isRight == true) 
        {
            render.flipX = true;
        }
        else if(isRight == false) 
        {
            render.flipX = false;
        }
    }

}
