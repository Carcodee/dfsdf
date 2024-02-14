using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateOnGround : CharacterStateBase
{   
    [Header("CharacterStateOnGround")]
    [Header("Animations")]
    public string animationName;
    public float OnGroundTime= 1f;

    private float currentTime = 0f;

    public override void StateEnter(StateParameter[] parameters = null)
    {
        //PLAY ANIMATION
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
       
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= OnGroundTime) {
            stateMachine.SetState("MovementState");
        }
    }
    public override void StateInput()
    {
        
    }
}