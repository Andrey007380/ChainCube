using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Dictionary<int, Queue<GameObject>> poolDictionary = new();
    private Queue<GameObject> availableObjcts = new();

    [System.Serializable]
    private class Pools
    {
        public int tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pools> pools;

    public static Pool Instance { get; private set; }

    private void Awake() => Instance = this;

    public void Start()
    {
        foreach (Pools pool in pools)
        {
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                availableObjcts.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, availableObjcts);
        }
    }

    public GameObject GetFromPool(int tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        if (!objectToSpawn.GetComponent<TileCubeMover>())
        {
            objectToSpawn.AddComponent<TileCubeMover>();
        }
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        return objectToSpawn;
    }
    public void AddToPool(int tag, GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(tag))
            return;

        prefab.SetActive(false);
        prefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
        availableObjcts.Enqueue(prefab);

    }
}
