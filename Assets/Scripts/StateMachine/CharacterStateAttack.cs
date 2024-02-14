using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateAttack : CharacterStateBase
{   
    [Header("AttackState")]
    [Header("Animations")]
    public string animationName;
    //Estado al que trancisiona al terminar la animacion
    public string endStateName;
    //area de daño del ataque
    public Bounds hitBox;
    //Frames en los que el ataque hará daño
    public int startHitFrame;
    public int endHitFrame;
    //Estado del siguiente ataque
    public string netAttackState;
    [Header("HitInfo")]
    public HItManager.hitInfo hitInfo;
    //Varuable de control que indica si el hit esta activo
    protected bool _hitIsAcctive;
    //Variable de control que indica si se ha solicitado conexion con el siquiente ataque
    protected bool _hasConnected;
    //variable de control que inica si hay hit
    protected bool _hasHit;
    //Indica el frameactual de la animacion
    protected int _currentFrame;
    //Indica el total de frames de la animacion
    protected int _totalFrames;


    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( playerController.transform.position +  hitBox.center, hitBox.size);
        
    }
    public override void StateEnter(StateParameter[] parameters = null)
    {
        playerController.movement.Move(0,0);
        _hitIsAcctive = false;
        _hasConnected = false;
        _hasHit = false;
        _currentFrame = 0;
        playerController.animator.Play(animationName, -1, 0);
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
       if (_hitIsAcctive) {
        
            PerfomrHit();
       }


    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        _totalFrames = playerController.animator.GetTotalFrames(animationName);
        _currentFrame = playerController.animator.GetCurrentFrame(animationName);

        if (!_hitIsAcctive && _currentFrame >= startHitFrame && (_currentFrame < endHitFrame || startHitFrame >= endHitFrame))
        {
            _hitIsAcctive = true;

        }else if (_hitIsAcctive && (_currentFrame<startHitFrame || (_currentFrame> endHitFrame && _currentFrame >= endHitFrame))) {
            _hitIsAcctive = false;

        }
        
        if(_currentFrame< _totalFrames) return;
        if (_hasConnected && !string.IsNullOrEmpty(netAttackState)) {
            if (_hasHit) {
                stateMachine.SetState(netAttackState);
            } else {
                stateMachine.SetState(stateName);
            }
        }else if(!string.IsNullOrEmpty(endStateName)){
            stateMachine.SetState(endStateName);
        } 

    }
    public override void StateInput()
    {
        if (Input.GetButtonDown("Fire1")) {
            _hasConnected = true;
        }
    }
    protected void PerfomrHit() {
        Vector2 hitboxCenter = hitBox.center;
        hitboxCenter.x= playerController.movement.faceDirection.x > 0 ? Mathf.Abs(hitBox.center.x): -Mathf.Abs(hitBox.center.x);
        hitBox.center = hitboxCenter;

        Collider2D[] others = Physics2D.OverlapAreaAll(playerController.transform.position + hitBox.min , playerController.transform.position + hitBox.max);


        if (others == null) return;

        for (int i = 0; i <others.Length;  i++) {
            bool result = false;

            if (others[i] == this) continue;

            if (others[i].TryGetComponent(out HitReceiver hitReceiver)) {
                float groundForceDirection= playerController.movement.faceDirection.x > 0 ? hitInfo.groundForce.x : -hitInfo.groundForce.x;
                HItManager.hitInfo hitInfoTemp= hitInfo;
                hitInfoTemp.groundForce.x = groundForceDirection;

                result = HItManager.Instance.CheckHit(playerController.transform, hitReceiver, hitInfoTemp);
            }

            if (!_hasHit) {
                _hasHit = result;
            }
        }
    }
}