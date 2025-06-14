using UnityEngine;
using System.Collections.Generic;
using System;
public class ObjectPool : MonoBehaviour
{
    public PoolObject[] objectsToPool;
    
    [System.Serializable]
    public struct PoolObject
    {
        public string name;
        public GameObject prefab;
        public int initialCount;
    }

    private Dictionary<string, Queue<GameObject>> objectsPool;

    public void Start()
    {
        if (objectsPool == null)
        {
            objectsPool = new Dictionary<string, Queue<GameObject>>();
        }
        Init();
    }

    public void Init()
    {
        if (objectsToPool == null)
        {
            objectsToPool = new PoolObject[0];
        }
        foreach (var poolObject in objectsToPool)
        {
            if (!objectsPool.ContainsKey(poolObject.name))
            {
                objectsPool.Add(poolObject.name, new Queue<GameObject>());
            }
            for (var i = 0; i < poolObject.initialCount; i++)
            {
                var obj = Instantiate(poolObject.prefab, transform); // FIXED LINE
                obj.SetActive(false);
                objectsPool[poolObject.name].Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (objectsPool.ContainsKey(name) && objectsPool[name].Count > 0)
        {
            var obj = objectsPool[name].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        var poolObject = Array.Find(objectsToPool, x => x.name == name);
        var outPut = Instantiate(poolObject.prefab, transform);
        Debug.LogWarning($"No object available in pool: {name}");
        return outPut;
    }

    public void ReturnObject(string name, GameObject obj)
    {
        if (objectsPool.ContainsKey(name))
        {
            obj.SetActive(false);
            objectsPool[name].Enqueue(obj);
        }
        else

        {
            objectsPool.Add(name, new Queue<GameObject>());
            obj.SetActive(false);
            objectsPool[name].Enqueue(obj);
            Debug.LogWarning($"Attempting to return object to non-existent pool: {name}");
            Destroy(obj);
        }
    }
}
