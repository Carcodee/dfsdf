using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Aplica lo aprendido para realizar ods nuevos estados: Salto (characterStateJump) y caída (characterStateFall) con sus correspondiestes
 animaciones y transiciones. Recuerda que debe existir una transición desde el estado de movimiento.*/

public abstract class CharacterStateBase : StateBase
{
    public PlayerController playerController;
    #region StateBase
    public abstract override void StateEnter(StateParameter[] parameters = null);
    public abstract override void StateExit();
    public abstract override void StateLoop();
    public abstract override void StatePhysicsLoop();
    public abstract override void StateLateLoop();
    public abstract override void StateInput();

    #endregion
}
