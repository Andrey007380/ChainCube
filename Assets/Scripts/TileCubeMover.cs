using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TileCubeMover : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _movementSpeed = 25f;
    
    private Ray _ray;
    private RaycastHit _hit;
    
    private Camera _camera;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }
    #region BaseMouseControl

    private void OnMouseDrag()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit)) return;
        
        var position = _rigidbody.position;
        _rigidbody.position = Vector3.MoveTowards(position, new Vector3(_hit.point.x,position.y,position.z), Time.deltaTime * _movementSpeed);
    }

    private void OnMouseUp() => TileCubeShooter();
    
    #endregion
    private void TileCubeShooter()
    {
        _rigidbody.AddForce(transform.forward * _movementSpeed, ForceMode.Impulse);
    }
    
}

