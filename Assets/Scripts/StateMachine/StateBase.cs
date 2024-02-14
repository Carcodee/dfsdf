using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    #region Variables
    [Tooltip("Nombre del estado. Debe ser único, ya que se utilizará para identificar los estados en la máquina de estados")] public string stateName;
    [Tooltip("Referencia al state machine que contiene al estado")] public StateMachineController stateMachine;
    #endregion
    #region TODO
    // Definir referencia a máquina de estados
    // Character statebase que herede todos esto, pero tiene que tener una propiedad que sea playerController
    #endregion

    public struct StateParameter {
        public object value;
    }
    /// <summary>
    /// Código de entrada al estado
    /// </summary>
    public abstract void StateEnter(StateParameter[] parameters = null);
    /// <summary>
    /// Código de salida del estado
    /// </summary>
    public abstract void StateExit();
    /// <summary>
    /// Lógica del estado que debe ejecutarse cada frame (Update)
    /// </summary>
    public abstract void StateLoop();
    /// <summary>
    /// Lógica del estado que debe ejecutarse según el ciclo de físicas (FixedUpdate)
    /// </summary>
    public abstract void StatePhysicsLoop();
    /// <summary>
    /// Lógica del estado que debe ejecutarse al final de cada frame (LateUpdate)
    /// </summary>
    public abstract void StateLateLoop();
    /// <summary>
    /// Lógica de detección de inputs
    /// </summary>
    public abstract void StateInput();
    
}
