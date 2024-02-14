using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("References")] 
    [Tooltip("Componente para aplicar movimiento al personaje")] public CharacterMovement movement;
    [Tooltip("Referencia al animator del personaje")] public Animator animator;
    public StateMachineController stateMachine;
    #endregion
    void Start()
    {
        stateMachine.Initialize();
    }
    void Update()
    {
        stateMachine.Step();
    }
    private void FixedUpdate()
    { 
        stateMachine.PhysicsStep();
    }
    private void LateUpdate()
    { 
        stateMachine.LateLoop();
    }
}
