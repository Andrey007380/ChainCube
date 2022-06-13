using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Collider[] colliders;

    private Vector3 _instantiatePosition;

    public static Action OnLose;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _instantiatePosition = transform.position;
        Instantiate(prefab, _instantiatePosition, Quaternion.identity);
    }
    
    private void InstantiateTile() => Pool.Instance.GetFromPool(1, _instantiatePosition, Quaternion.identity);

    private void OnTriggerExit(Collider other)
    {
        int objectsInDeathZone = Physics.OverlapBoxNonAlloc(transform.position, new Vector3(5f, 1f, 4f), colliders = new Collider[2], Quaternion.identity,
            LayerMask.GetMask($"Cube"));
        int maxObjectBeforeDeath = 1;
        
        if (objectsInDeathZone > maxObjectBeforeDeath) 
            OnLose?.Invoke();
        
        InstantiateTile();
    }

    private void TileCubeMoverOnOnShoot(Vector3 objPosition) => _instantiatePosition = objPosition;

    private void OnEnable() => TileCubeMover.OnShoot += TileCubeMoverOnOnShoot;

    private void OnDisable() => TileCubeMover.OnShoot -= TileCubeMoverOnOnShoot;
    
}
