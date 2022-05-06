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
       
        colliders = Physics.OverlapBox(transform.position, new Vector3(3,1f,4), Quaternion.identity, LayerMask.GetMask($"Cube"));
        if (colliders.Length > 1) OnLose?.Invoke();
        InstantiateTile();
    }

    private void OnTriggerEnter(Collider other)
    {
       

    }

    private void TileCubeMoverOnOnShoot(Vector3 objPosition) => _instantiatePosition = objPosition;

    private void OnEnable() => TileCubeMover.OnShoot += TileCubeMoverOnOnShoot;

    private void OnDisable() => TileCubeMover.OnShoot -= TileCubeMoverOnOnShoot;
    
}
