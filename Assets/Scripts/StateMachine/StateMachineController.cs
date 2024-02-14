using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    [Tooltip("Estados que contiene esta máquina de estados.")] public StateBase[] states;
    private StateBase _currentState;
    /// <summary>
    /// Método de inicialización
    /// </summary>
    public void Initialize()
    {
        if (states!=null&&states.Length>0)
        {
            // todo: initializamos el estado inicial
            string initialStateName = states[0].stateName;
            SetState(initialStateName);
        }
    }
    /// <summary>
    /// Lógica del Update
    /// </summary>
    public void Step()
    {
        if (_currentState!=null)
        {
            _currentState.StateInput();
            _currentState.StateLoop();
        }
    }
    /// <summary>
    /// Lógica del FixedUpdate
    /// </summary>
    public void PhysicsStep()
    {
        if (_currentState!=null)
        {
            _currentState.StatePhysicsLoop();
        }
    }
    /// <summary>
    /// Lógica del LateUpdate
    /// </summary>
    public void LateLoop()
    {
        if (_currentState!=null)
        {
            _currentState.StateLateLoop();
        }
    }
    public void SetState(string stateName, StateBase.StateParameter[] parameters= null)
    {
        //Obtenemos el estado a través del nombre
        StateBase nextState = GetStateWithName(stateName);
        // Si no existe, cortamos el cambio de estado
        if (nextState==null)return;
        // Ejecutamos la salida del estado acutual, si la hay
        if (_currentState!=null)
        {
            _currentState.StateExit();
        }
        // Actualizamos el estado actual
        _currentState = nextState;
        _currentState.StateEnter(parameters);
        
    }
    /// <summary>
    /// Devuelve el estado cuyo nombre coincida con el indicado. 
    /// </summary>
    /// <param name="stateName"></param>
    /// <returns></returns>
    private StateBase GetStateWithName(string stateName)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].stateName == stateName)
            {
                return states[i];
            }
        }
        return null;
    }
}
