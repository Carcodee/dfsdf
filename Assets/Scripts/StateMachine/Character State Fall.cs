using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateFall : CharacterStateBase
{
    [Header("FALL STATE")]
    [Header("Animations")] 
    public string fallAnimation;
    [Header("States")] 
    public string groundState;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        playerController.animator.Play(fallAnimation);
    }
    public override void StateExit()
    {

    }
    public override void StateLoop()
    {
        if (playerController.movement.IsGrounded)
        {
            stateMachine.SetState(groundState);
        }
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        
    }
    public override void StateInput()
    {
        
    }
}
