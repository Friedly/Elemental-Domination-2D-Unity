using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class pool
{
    public int pooledAmount;
    public bool willGrow;
    public GameObject collector;
    public GameObject objectToPool;
    public List<GameObject> objects;
}