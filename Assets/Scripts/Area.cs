using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] PolygonCollider2D collider2D;
    
    public bool IsInArea(Collider2D collider, out Vector2 fixedDirection)
    {
        // Obtenemos el objeto colliderDistance de los dos collider implicados
        ColliderDistance2D colliderDistance2D = collider2D.Distance(collider);
        // A partir de la información de dicho objeto, obtenemos el resultado y corregimos la dirección.
        bool result = colliderDistance2D.distance < 0 && colliderDistance2D.isOverlapped;
        fixedDirection = result ? Vector2.zero : colliderDistance2D.normal * colliderDistance2D.distance;

        return result;
    }
}
