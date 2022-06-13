using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class TileCubeMover : MonoBehaviour, IMovable
{
    private Rigidbody _rigidbody;
    private Transform _transform;

    private LineRenderer lineRenderer;
    
    private float _movementSpeed = 25f;

    private Ray _ray;
    private RaycastHit _hit;

    private Camera _camera;
    
    public static Action<Vector3> OnShoot;
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }
    #region BaseControl

    private void OnMouseDrag() => Move();

    private void OnMouseUp()
    {
        TileCubeShooter();
        OnShoot?.Invoke(transform.position);
        Destroy(this);
        lineRenderer.enabled = false;
    }
    
    public void Move()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit)) 
            return;

        var position = _rigidbody.position;
        _rigidbody.position = Vector3.Lerp(position, new Vector3(_hit.point.x,position.y,position.z), Time.deltaTime * _movementSpeed);
        
        Vector3[] positions = new Vector3[3];
        positions[0] = transform.position;
        positions[1] = transform.forward * 20 + transform.position;
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    #endregion
    private void TileCubeShooter() => _rigidbody.AddForce(Vector3.forward * _movementSpeed, ForceMode.Impulse);
    
    private void Thrower(GameObject obj) => obj.GetComponent<Rigidbody>().AddForce(Vector3.up * _movementSpeed, ForceMode.Impulse);
    
    private void OnEnable() => TileCubeSystem.OnCollision += Thrower;

    private void OnDisable() => TileCubeSystem.OnCollision -= Thrower;
    

}

