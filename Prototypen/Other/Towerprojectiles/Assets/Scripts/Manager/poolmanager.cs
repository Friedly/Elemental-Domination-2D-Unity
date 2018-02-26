using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/object-pooling

public class poolmanager : singleton<poolmanager>
{
    private Dictionary<string, pool> _pools = null;

    protected poolmanager()
    {
    }

    public void initiate()
    {
        _pools = new Dictionary<string,pool>();
    }

    public void createPool(string poolName, int amountToPool, bool willGrow, GameObject objectToPool, GameObject parentObject = null)
    {
        if (_pools.ContainsKey(poolName))
        {
            return;
        }

        pool newPool = new pool();
        newPool.willGrow = willGrow;
        newPool.pooledAmount = amountToPool;

        newPool.objectToPool = objectToPool;
        newPool.collector = new GameObject("(" + poolName + " | " + newPool.pooledAmount + ")");
        newPool.objects = new List<GameObject>();

        if (!parentObject)
            newPool.collector.transform.SetParent(transform, false);
        else
            newPool.collector.transform.SetParent(parentObject.transform, false);

        _pools.Add(poolName, newPool);

        for(int count = 0; count < amountToPool; ++count)
        {
            GameObject poolObject = Instantiate(objectToPool) as GameObject;

            poolObject.SetActive(false);

            poolObject.transform.SetParent(newPool.collector.transform, false);

            newPool.objects.Add(poolObject);
        }
    }

    public GameObject getPooledObject(string poolName)
    {
        if (!_pools.ContainsKey(poolName))
        {
            return null;
        }

        pool currentPool = _pools[poolName];

        for (int count = 0; count < currentPool.pooledAmount; ++count)
        {
            if (!currentPool.objects[count].activeInHierarchy)
            {
                return currentPool.objects[count];
            }
        }

        if (currentPool.willGrow)
        {
            GameObject poolObject = Instantiate(currentPool.objectToPool) as GameObject;

            poolObject.SetActive(false);
            poolObject.transform.parent = currentPool.collector.transform;

            currentPool.objects.Add(poolObject);
            currentPool.pooledAmount++;
            currentPool.collector.name = "(" + poolName + " | " + currentPool.pooledAmount + ")";

            return poolObject;
        }

        return null;
    }
}
