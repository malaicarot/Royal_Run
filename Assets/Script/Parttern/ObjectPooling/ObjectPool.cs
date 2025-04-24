using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Range(10, 50)][SerializeField] uint poolSize;

    [SerializeField] List<PooledObject> pooledList;

    Dictionary<string, Stack<PooledObject>> poolDictionary;


    void Awake()
    {
        SetupPool();
    }

    void SetupPool()
    {
        if (pooledList.Count == 0 || pooledList == null)
        {
            return;
        }
        poolDictionary = new Dictionary<string, Stack<PooledObject>>();
        foreach (var objectPool in pooledList)
        {

            Stack<PooledObject> stackPool = new Stack<PooledObject>();
            for (int i = 0; i < poolSize; i++)
            {
                PooledObject newPooledObject = Instantiate(objectPool);
                newPooledObject.name = objectPool.name;
                newPooledObject._pool = this;
                newPooledObject.gameObject.SetActive(false);
                stackPool.Push(newPooledObject);
            }
            poolDictionary.Add(objectPool.name, stackPool);
        }
    }

    public PooledObject GetPooledObject(string nameObject)
    {
        if (string.IsNullOrEmpty(nameObject) || !poolDictionary.ContainsKey(nameObject))
        {
            Debug.LogError("This object name doesn't contains!");
            return null;
        }

        if (poolDictionary[nameObject].Count == 0)
        {
            PooledObject newPooledObject = Instantiate(pooledList.Find(obj => obj.name == nameObject));
            newPooledObject.name = nameObject;
            newPooledObject._pool = this;
            return newPooledObject;
        }

        PooledObject nextPooledObject = poolDictionary[nameObject].Pop();
        nextPooledObject.gameObject.SetActive(true);
        return nextPooledObject;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        if (pooledObject == null || !poolDictionary.ContainsKey(pooledObject.name))
        {
            Debug.LogError("This pooled object doesn't exist!");
            return;
        }

        poolDictionary[pooledObject.name].Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
