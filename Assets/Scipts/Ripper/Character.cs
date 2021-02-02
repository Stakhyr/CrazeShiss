using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    /// <summary>
    /// / check what happen with ground check
    /// </summary>
    #region Variables
    public Rigidbody2D characterBody;
    protected StateMashine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public SlachAtackSate slashAtack;
    public MagicAtackState magicAtack;
    public SlidingState sliding;
    public GroundedState movingSt;

    private readonly int horizonalMoveParam = Animator.StringToHash("Speed");
    [SerializeField]
    GameObject dustCloud;
    [SerializeField]
    private int maxMagicAmount = 100;

    [SerializeField]
    internal int currentMagicAmount;

    public MagicBar magicBar;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private GameObject fireBall;

    [SerializeField]
    private Transform launchPoint;

    protected float Timer;


    private float timeBtwShots;
    [SerializeField]
    private float startTimeBtwShots;

    public bool isRight = true;

    public int DelayAmount = 1;

    #endregion

    #region Properties
    public float GroundCollisionRadius = 0.2f;
    public float movementSpeed = 2f;
    public float movementSmoothing = 0.05f;
    public float JumpForce = 400f;
    private Vector3 velocity = Vector3.zero;
    #endregion

    #region MovementBlock
    public void Move(float speed)
    {
        Vector3 targetVelocity = new Vector2(speed * 5f, characterBody.velocity.y);

        characterBody.velocity = Vector3.SmoothDamp(characterBody.velocity, targetVelocity, ref velocity, movementSmoothing);

        anim.SetFloat(horizonalMoveParam, Mathf.Abs(characterBody.velocity.x));
    }
    public void ResetMoveParams()
    {
        characterBody.velocity = Vector2.zero;
        anim.SetFloat(horizonalMoveParam, 0f);
    }
    #endregion

    #region AtackBlock
    public void PlayAtack()
    {
        anim.Play("PlayerSlashing");
    }
    #endregion

    #region JumpBlock
    public void ApplyImpulse(float imp)
    {
        characterBody.AddForce(new Vector2(0f, imp));
        anim.SetBool("isOnGround", false);
    }

    public void OnLand()
    {
        anim.SetBool("isOnGround", true);
    }
    public bool CheckGroundCollision()
    {
        return Physics2D.OverlapCircle(groundCheck.position, GroundCollisionRadius, whatIsGround);
    }
    #endregion

    public void DustEffect() 
    {
        Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
    }
    public void UseMagic() 
    {
        if (timeBtwShots <= 0) 
        {
            Instantiate(fireBall, launchPoint.position, launchPoint.rotation);
            timeBtwShots = startTimeBtwShots;
            

        }
        else 
        {
            timeBtwShots -= Time.deltaTime;
        }
        
    }

    public void MagicAmountRecovery() 
    {
        if (currentMagicAmount <= 100) 
        {
            Timer += Time.deltaTime;

            if (Timer >= DelayAmount)
            {
                Timer = 0f;
                currentMagicAmount+=4;
                magicBar.SetMagicAmount(currentMagicAmount);

            }
        }
    } 
    public void TriggerAnimation(int param) 
    {
        anim.SetTrigger(param);
    }
    
    public void MagicConsumption(int magicPerHit) 
    {
        currentMagicAmount -= magicPerHit;
        magicBar.SetMagicAmount(currentMagicAmount);
    }

    #region MonoBehavior Callbacks
    void Start()
    {
        currentMagicAmount = maxMagicAmount;
        magicBar.SetMaxMagicAmount(maxMagicAmount);
        anim = GetComponent<Animator>();
        characterBody = GetComponent<Rigidbody2D>();
        movementSM = new StateMashine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        slashAtack = new SlachAtackSate(this, movementSM);
        magicAtack = new MagicAtackState(this,movementSM);
        sliding = new SlidingState(this, movementSM);
        movingSt = new GroundedState(this, movementSM);

        movementSM.InitializeState(standing);
    }

   

    void Update()
    {
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        
        movementSM.CurrentState.PhysicsUpdate();
    }

   
    #endregion
}
