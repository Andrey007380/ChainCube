using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class TileCubeMover : MonoBehaviour, IMovable
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    
    Color c2 = new Color(1, 1, 1, 0.5f);
    private LineRenderer lineRenderer;
    
    private float _movementSpeed = 25f;

    private Ray _ray;
    private RaycastHit _hit;

    private Camera _camera;
    
    public static Action<Vector3> OnShoot;
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = c2;
        lineRenderer.endColor = c2;
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
        if (!(_rigidbody.position.x > -2) || !(_rigidbody.position.x < 2)) 
            return;
        var position = _rigidbody.position;
        _rigidbody.position = Vector3.Lerp(position, new Vector3(_hit.point.x,position.y,position.z), Time.deltaTime * _movementSpeed);
                    
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.forward * 10;
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
            
        lineRenderer.alignment = LineAlignment.View;

    }

    #endregion
    private void TileCubeShooter() => _rigidbody.AddForce(Vector3.forward * _movementSpeed, ForceMode.Impulse);
    
    private void Thrower(GameObject obj) => obj.GetComponent<Rigidbody>().AddForce(Vector3.up * _movementSpeed, ForceMode.Impulse);
    
    private void OnEnable() => TileCubeSystem.OnCollision += Thrower;

    private void OnDisable() => TileCubeSystem.OnCollision -= Thrower;
    

}

