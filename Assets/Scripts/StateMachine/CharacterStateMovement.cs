using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Añade e código necesario a PlayerController y refactoriza la clase CharacterMovement haciendo los cambios
 necesarios para el personaje pueda estar en idle y andar con sus animaciones reproduciendosse perfectamente.*/
public class CharacterStateMovement : CharacterStateBase
{
    [Header("MOVEMENT STATE")]
    [Header("Animations")] 
    public string idleAnimationName;
    public string walkAnimationName;

    [Header("States")] 
    public string jumpStaticState;
    public string jumpMovementState;
    public string attackState;

    public Vector2 moveInput;
   
    public override void StateEnter(StateParameter[] parameters = null)
    {
        
    }
    public override void StateExit()
    {
        
    }
    public override void StateInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(horizontal, vertical);
        
        if (Input.GetButtonDown("Jump")&& moveInput.x == 0)
        {
            stateMachine.SetState(jumpStaticState);

        }
        if (Input.GetButtonDown("Jump") && moveInput.x != 0) {
            stateMachine.SetState(jumpMovementState);
        }
        if (Input.GetButtonDown("Fire1")) {
            stateMachine.SetState(attackState);
        }
    }
    public override void StateLoop()
    {
        playerController.movement.Move(moveInput.x,moveInput.y);
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        if (playerController.movement.GroundVelocity.magnitude > 0.1f)
        {
            playerController.animator.Play(walkAnimationName);
        }
        else
        {
            playerController.animator.Play(idleAnimationName);
        }
    }
}
