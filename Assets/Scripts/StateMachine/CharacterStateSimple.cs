using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Programa un nuevo estado llamado CharacterStatesSimple que pueda realizar las acciones listadas a continuación:
//Parar el movimiento al entrar.
//Reanudar el movimiento al salir.
//Tiempo para salir.
//Estado de salida.
public class CharacterStateSimple : CharacterStateBase
{   
    [Header("Simple State")]
    [Header("Animations")]
    public string animationName;

    [Header("Physics")]
    //Indicca si debemos parar el movimiento al entrar en el estado.
    public bool stopMovementOnEnter;
    //Indica si debemos reanudar el movimiento al salir del estado.
    public bool startMovementOnExit;

    [Header("Finish Conditions")]
    //Indica si debemos salir del estado cuando la animación termine.
    public bool exitOnAnimationEnds;
    //Tiempo de salida en caso de que no se salga por la animación.
    public float timeToExit;
    [Header("States")]
    //Nombre del siguiente estado.
    public string exitStateName;


    private bool _animationHasEnded;
    private float _timer;
    private Vector2 _movementBackup;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        playerController.animator.Play(animationName);
        _animationHasEnded = false;
        _timer = 0;
        if (stopMovementOnEnter) {
            _movementBackup = playerController.movement.GroundVelocity;
            playerController.movement.Move(0.0f, 0.0f);
        }
        Debug.Log("Entrando en el estado " + stateName);
            
    }
    public override void StateExit()
    {
        //si hay que reanuar el movimiento al salir del estado.
        if(stopMovementOnEnter && startMovementOnExit) {
            playerController.movement.Move(_movementBackup.x, _movementBackup.y);
        }

    }
    public override void StateLoop()
    {
        if (!exitOnAnimationEnds) {
            _timer += Time.deltaTime;
        }
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        if (exitOnAnimationEnds && !playerController.animator.IsPlaying(animationName)) {
            _animationHasEnded = true;

        }

    }
    public override void StateInput()
    {
        if((!exitOnAnimationEnds && _timer >= timeToExit )|| _animationHasEnded) {
            stateMachine.SetState(exitStateName);

        }

    }
}