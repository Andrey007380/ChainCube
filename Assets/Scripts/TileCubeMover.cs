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
    private LineRenderer _lineRenderer;

    private Camera _camera;
    
    public static event Action OnShoot;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
    }
    #region BaseControl

    private void OnMouseDrag()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit)) return;
        var position = _rigidbody.position;
        LineRenderSettings();
        _rigidbody.position = Vector3.Lerp(position, new Vector3(_hit.point.x,position.y,position.z), Time.deltaTime * _movementSpeed);
    }

    private void OnMouseUp()
    {
        TileCubeShooter();
        _lineRenderer.enabled = false;
        
        OnShoot?.Invoke();
    }

    #endregion
    private void TileCubeShooter()
    {
        _rigidbody.AddForce(transform.forward * _movementSpeed, ForceMode.Impulse);
    }

    private void LineRenderSettings()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.forward * 10 + transform.position);
        _lineRenderer.material.color = Color.blue;
    }
    
}

