using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJump : CharacterStateBase
{
    [Header("JUMP STATE")]
    [Header("Animations")] 
    public string jumpAnimation;
    [Header("States")] 
    public string fallState;
    public float maxJump;
    public Vector2 prueba;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        playerController.movement.Jump();
        playerController.animator.Play(jumpAnimation);
        prueba = playerController.movement.VerticalVelocity;
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
        if (playerController.movement.VerticalVelocity.y <= maxJump)
        {
            stateMachine.SetState(fallState);
        }
    }
    public override void StatePhysicsLoop()
    {
        /*if (!playerController.movement.IsGrounded && playerController.movement.VerticalVelocity.magnitude>0.1f)
        {
            playerController.animator.Play(jumpAnimation);
        }*/
    }
    public override void StateLateLoop()
    {
        /*if (playerController.movement.IsGrounded && playerController.movement.VerticalVelocity.magnitude<0.1f)
        {
            stateMachine.SetState("MovementState");
        }*/
    }
    public override void StateInput()
    {
        
    }
}
