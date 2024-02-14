using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHit : CharacterStateBase
{   
    [Header("CharacterStateHit")]
    [Header("Animations")]
    public string animationName;

    [Header("Configurations")]
    public float time;
    
    public string endStateName;

    [Range(0,1)]
    public float shakeIntensity= 0.5f;

    private float _shakeMaxVariation= 0.05f;
    
    private float _shakeVariation;

    private float _timer;

    private HItManager.hitInfo _currentHitInfo;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        _timer = 0;
        _shakeVariation = shakeIntensity * _shakeMaxVariation;
        playerController.animator.Play(animationName, -1, 0);
        if (parameters != null && parameters.Length>0) {
            if (parameters[0].value is not HItManager.hitInfo) return;

            _currentHitInfo= (HItManager.hitInfo)parameters[0].value;
            playerController.movement.SetVelocity(_currentHitInfo.groundForce , _currentHitInfo.verticalForce);
        }
        
    }
    public override void StateExit()
    {
        StopShake();
    }
    public override void StateLoop()
    {
        _timer += Time.deltaTime;

    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        Shake();
    }
    public override void StateInput()
    {
        if (_timer > time) {
            stateMachine.SetState(endStateName);
            return;
        }
    }

    private void Shake() {

        Vector2 currentBodyPos= playerController.movement.characterBody.localPosition;

        currentBodyPos.x= currentBodyPos.x>0? -_shakeVariation: _shakeVariation;

        playerController.movement.characterBody.localPosition= currentBodyPos;
    }

    private void StopShake() {
        Vector2 currentBodyPos = playerController.movement.characterBody.localPosition;

        currentBodyPos.x = 0;

        playerController.movement.characterBody.localPosition = currentBodyPos;
    }
}