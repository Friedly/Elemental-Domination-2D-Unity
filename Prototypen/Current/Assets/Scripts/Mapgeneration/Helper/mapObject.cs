using UnityEngine;

[System.Serializable]
public class mapObject
{
    public string objectName;
    public GameObject objectPrefab;

    public mapObject(string name = "", GameObject prefab = null)
    {
        objectName = name;
        objectPrefab = prefab;
    }
}
