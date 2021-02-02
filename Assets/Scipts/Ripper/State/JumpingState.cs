using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    private bool grounded;
    private readonly int jumpParam = Animator.StringToHash("Jump");
    //private int landParam = Animator.StringToHash("land");



    public JumpingState(Character character, StateMashine stateMashine) : base(character, stateMashine) {}


    public override void Enter()
    {
        base.Enter();
        grounded = false;
        Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded) 
        {
            character.OnLand();
            stateMashine.ChangeState(character.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        grounded = character.CheckGroundCollision();
        if (grounded) 
        {
            character.DustEffect();
        }
        
       
    }

    private void Jump()
    {
        character.ApplyImpulse(character.JumpForce);
        character.TriggerAnimation(jumpParam);
    }


}
