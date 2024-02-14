using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItManager : MonoBehaviour
{

    private static HItManager instance;

    public static HItManager Instance {
        get {
            return instance;
        }
    }
    [System.Serializable]
    public struct hitInfo {
        public Vector2 groundForce;
        public float verticalForce;
        public float damage;
        public float stunTime;
    }

    public float hitMargin = 0.3f;
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public bool CheckHit(Transform instigator, HitReceiver hitReceiver, hitInfo hit) {
        if (Mathf.Abs(instigator.transform.position.y - hitReceiver.transform.position.y) <= hitMargin) {
            hitReceiver.Hit(hit);
            return true;
        }
        return false;
    }

}
