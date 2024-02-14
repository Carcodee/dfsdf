using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;

    [Header("References")]
    public Camera cam;
    public float zPosition;

    [Header("Posicionamiento")]
    public Vector2 offset;

    private Vector2 _cameraSize;
    private Vector3 _targetPosition;
    private Bounds _cameraBounds;

    public Vector2 maxPos;
    public Vector2 minPos;

    void Start()
    {
        Initialize();
    }

    void FixedUpdate()
    {
        UpdatePosition();
        ApplyMovement();
    }

    private void OnValidate() {
        Initialize();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        //Vector2 size = maxPos - minPos;
        //Vector2 center= (minPos + size) / 2;

        Gizmos.DrawWireCube(_cameraBounds.center, _cameraBounds.size);
    }
    private void UpdateCameraSize() {
        Vector2 currentMinPosition = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 currentMaxPosition = cam.ViewportToWorldPoint(Vector2.one);
        _cameraSize = currentMaxPosition - currentMinPosition;
        Debug.Log("Camera size: " + _cameraSize);


    }

    private void Initialize() {

        Vector3 position = transform.position;
        position.z = zPosition;
        transform.position = position;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(minPos, maxPos);
        UpdateCameraSize();

    }


    /// <summary>
    /// Obtiene la posición del target y le suma el offset
    /// </summary>
    public void UpdatePosition()
    {
        Vector3 nextPosition= targetTransform.position + (Vector3)offset;
        _targetPosition = ClampPositionInBounds(nextPosition);
        _targetPosition.z = zPosition;
    }

    /// <summary>
    /// Aplica la posición del target al transform de la cámara
    /// </summary>
    public void ApplyMovement() {

        transform.position = _targetPosition;
    }

    private Vector3 ClampPositionInBounds(Vector3 pos) {
        pos.x = Mathf.Clamp(pos.x, _cameraBounds.min.x + _cameraSize.x / 2.0f, _cameraBounds.max.x - _cameraSize.x / 2.0f);
        pos.y = Mathf.Clamp(pos.y, _cameraBounds.min.y + _cameraSize.y / 2.0f, _cameraBounds.max.y - _cameraSize.y / 2.0f);

        return pos;

    }

}
