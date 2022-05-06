using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    private Vector3 _instantiatePosition;

    public static event Action OnLose;
    
    [SerializeField] private Collider[] colliders;

    private void Start()
    {
        _instantiatePosition = transform.position;
        Instantiate(prefab, _instantiatePosition, Quaternion.identity);
    }
    private void InstantiateTile() => Pool.Instance.GetFromPool(1, _instantiatePosition, Quaternion.identity);

    private void OnTriggerExit(Collider other)
    {
        InstantiateTile();
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 offset = new Vector3(0, 0, 1);
        colliders = Physics.OverlapBox(transform.position, new Vector3(4,0.5f,3), Quaternion.identity, LayerMask.GetMask($"Cube"));
        
        if (colliders.Length > 1) OnLose?.Invoke();

    }

    private void TileCubeMoverOnOnShoot(Vector3 objPosition) => _instantiatePosition = objPosition;

    private void OnEnable() => TileCubeMover.OnShoot += TileCubeMoverOnOnShoot;

    private void OnDisable() => TileCubeMover.OnShoot -= TileCubeMoverOnOnShoot;
    
}
