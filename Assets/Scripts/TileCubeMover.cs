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
    
    public static event Action<Vector3> OnShoot;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }
    #region BaseControl

    private void OnMouseDrag()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit)) return;
        var position = _rigidbody.position;
        _rigidbody.position = Vector3.Lerp(position, new Vector3(_hit.point.x,position.y,position.z), Time.deltaTime * _movementSpeed);
    }

    private void OnMouseUp()
    {
        TileCubeShooter();
        OnShoot?.Invoke(transform.position);
    }

    #endregion
    private void TileCubeShooter() => _rigidbody.AddForce(Vector3.forward * _movementSpeed, ForceMode.Impulse);
    
    private void Thrower(GameObject obj) => obj.GetComponent<Rigidbody>().AddForce(transform.up, ForceMode.Impulse);
    

    private void OnEnable() => TileCubeSystem.OnCollide += Thrower;

    private void OnDisable() => TileCubeSystem.OnCollide -= Thrower;
}

