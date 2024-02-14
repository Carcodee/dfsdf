using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReceiver : MonoBehaviour
{

    public StateMachineController stateMachine;
    public string hitStateName;


    [ContextMenu("Hit")]
    public void Hit(HItManager.hitInfo hitInfo) {
        StateBase.StateParameter parameters = new StateBase.StateParameter {
            value = hitInfo
        };
        StateBase.StateParameter[] parametersArray = new StateBase.StateParameter[] {parameters};
                
        stateMachine.SetState(hitStateName, parametersArray);

    }

}
