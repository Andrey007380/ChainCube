using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Start() => InstantiateTile();

    private void InstantiateTile() => Instantiate(prefab, transform.position, Quaternion.identity);

    private void OnTriggerExit(Collider other)
    {
        InstantiateTile();
    }
}
