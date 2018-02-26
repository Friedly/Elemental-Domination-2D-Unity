using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapGfx : MonoBehaviour 
{
    public List<mapObject> tileObjects;
    public List<string> prefixIdentifier;

    private Dictionary<string, rangeHelper> _prefixedObjects = null;
    private Dictionary<string, GameObject> _tileObjects = null;
    private Dictionary<string, GameObject> _layers = null;
    private Dictionary<string, List<GameObject>> _layersGameObject = null;

    public void Awake()
    {
        if (gamemanager.instance.debug)
        {
            Debug.Log("(mapGfx:Awake) has been instantiated.");
        }
    }

    public void initiate(List<string> layerNames)
    {
        if (_prefixedObjects == null)
        {
            _prefixedObjects = new Dictionary<string, rangeHelper>();
        }

        if (_tileObjects == null)
        {
            _tileObjects = new Dictionary<string, GameObject>();
        }

        _tileObjects.Clear();

        if (tileObjects == null)
        {
            tileObjects = new List<mapObject>();
        }

        if (_layers == null)
        {
            _layers = new Dictionary<string, GameObject>();
        }

        if (_layersGameObject == null)
        {
            _layersGameObject = new Dictionary<string, List<GameObject>>();
        }

        foreach (KeyValuePair<string, GameObject> layer in _layers)
        {
            Destroy(layer.Value);
        }

        _layers.Clear();

        foreach (KeyValuePair<string, List<GameObject>> layer in _layersGameObject)
        {
            foreach (GameObject objectToDestroy in layer.Value)
            {
                Destroy(objectToDestroy);
            }
        }

        _layersGameObject.Clear();

        foreach (string prefix in prefixIdentifier)
        {
            _prefixedObjects.Add(prefix, new rangeHelper(-1, -1));
        }

        foreach (string layerName in layerNames)
        {
            if (_layers.ContainsKey(layerName))
            {
                continue;
            }

            GameObject layer = new GameObject("(layer)" + layerName);

            layer.transform.SetParent(gameObject.transform);

            _layersGameObject.Add(layerName, new List<GameObject>());
            _layers.Add(layerName, layer);

            if (gamemanager.instance.debug)
            {
                Debug.Log("(mapGfx:initiate) Created hierarchy layer: " + layerName + ".");
            }
        }

        Object[] objects = Resources.LoadAll("Mapgeneration");

        tileObjects.Add(new mapObject("allocable", null));
        tileObjects.Add(new mapObject("reserved", null));
        tileObjects.Add(new mapObject("path", null));
        
        int objectID = 3;

        foreach(Object mapObject in objects)
        {
            mapObject newMapObject = new mapObject();

            newMapObject.objectName = mapObject.name;
            newMapObject.objectPrefab = (GameObject)mapObject;

            foreach (string prefix in prefixIdentifier)
            {
                if (mapObject.name.StartsWith(prefix))
                {
                    if (_prefixedObjects[prefix].begin == -1)
                    {
                        _prefixedObjects[prefix].begin = objectID;
                        _prefixedObjects[prefix].end = objectID;
                    }
                    else
                    {
                        _prefixedObjects[prefix].end++;
                    }
                }
            }

            tileObjects.Add(newMapObject);
            _tileObjects.Add(newMapObject.objectName, newMapObject.objectPrefab);
            objectID++;

            if (gamemanager.instance.debug)
            {
                Debug.Log("(mapGfx:initiate) Loaded gameobject: " + newMapObject.objectName + ".");
            }
        }

        if (gamemanager.instance.debug)
        {
            foreach (KeyValuePair<string, rangeHelper> prefixedObject in _prefixedObjects)
            {
                Debug.Log("(mapGfx: initiate) Objects with prefix " + prefixedObject.Key + " start with ID " + prefixedObject.Value.begin + " and end with ID " + prefixedObject.Value.end + ".");
            }

            Debug.Log("(mapGfx:initiate) has been initiated.");
        }
    }
    public void instantiateBoard(Dictionary<string, List<int>> board, int mapSize)
    {
        if (board == null)
        {
            return;
        }

        foreach (KeyValuePair<string, List<int>> layer in board)
        {
            if (_layers.ContainsKey(layer.Key) && _layersGameObject.ContainsKey(layer.Key))
            {
                for (int positionX = 0; positionX < mapSize; positionX++)
                {
                    for (int positionY = 0; positionY < mapSize; positionY++)
                    {
                        GameObject toInstantiate = null;

                        if (layer.Value[positionY * mapSize + positionX] < 0 || layer.Value[positionY * mapSize + positionX] >= tileObjects.Count)
                        {
                            continue;
                        }

                        if (tileObjects[layer.Value[positionY * mapSize + positionX]].objectPrefab == null)
                        {
                            continue;
                        }

                        toInstantiate = Instantiate(tileObjects[layer.Value[positionY * mapSize + positionX]].objectPrefab, new Vector3(positionX, positionY), Quaternion.identity) as GameObject;

                        _layersGameObject[layer.Key].Add(toInstantiate);
                        toInstantiate.transform.SetParent(_layers[layer.Key].transform);

                        if (gamemanager.instance.debug)
                        {
                            Debug.Log("(mapGfx:instantiateBoard) Gameobject with name " + tileObjects[layer.Value[positionY * mapSize + positionX]].objectName + " on layer " + layer.Key  + " with position " + positionX + " | " + positionY + " has been instantiated.");
                        }
                    }
                }
            }
        }

        float verticalExtent = Camera.main.orthographicSize;
        float horizontalExtent = verticalExtent * Screen.width / Screen.height;
        int borderTileCountX = (int)(horizontalExtent - ((float)mapSize / 2.0f)) + 1;
        int borderTileCountY = (int)(verticalExtent - ((float)mapSize / 2.0f)) + 1;

        for (int positionX = -borderTileCountX; positionX < mapSize + borderTileCountX; ++positionX)
        {
            for (int positionY = -borderTileCountY; positionY < mapSize + borderTileCountY; ++positionY)
            {
                if ((positionX >= 0 && positionX < mapSize) && (positionY >= 0 && positionY < mapSize))
                {
                    continue;
                }

                GameObject toInstantiate = Instantiate(tileObjects[_prefixedObjects["border"].getRandom()].objectPrefab, new Vector3(positionX, positionY), Quaternion.identity) as GameObject;
                toInstantiate.transform.SetParent(_layers["border"].transform);
            }
        }

        if (gamemanager.instance.debug)
        {
            Debug.Log("(mapGfx:instantiateBoard) Map has been instantiated.");
        }
    }
    public void instantiateGameObject(string gameObjectName, int positionX, int positionY, string layerName)
    {
        if (gameObjectName == "" || _layers == null || layerName == "" || _layersGameObject == null)
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapGfx:instantiateGameObject) Gameobject with name " + gameObjectName + " on layer " + layerName + " with position " + positionX + " | " + positionY + " could not be instantiated.");
            }

            return;
        }

        if (_layers.ContainsKey(layerName) && _layersGameObject.ContainsKey(layerName) && _tileObjects.ContainsKey(gameObjectName))
        {
            if (_tileObjects[gameObjectName] == null)
            {
                return;
            }

            GameObject newGameObject = Instantiate(_tileObjects[gameObjectName], new Vector3(positionX, positionY), Quaternion.identity) as GameObject;

            _layersGameObject[layerName].Add(newGameObject);
            newGameObject.transform.SetParent(_layers[layerName].transform);

            if (gamemanager.instance.debug)
            {
                Debug.Log("(mapGfx:instantiateGameObject) Gameobject with name " + gameObjectName + " on layer " + layerName + " with position " + positionX + " | " + positionY + " has been instantiated.");
            }
        }
    }
    public void addGameObject(GameObject gameObject, string layerName)
    {
        if (gameObject == null || layerName == "")
        {
            return;
        }

        if (_layers.ContainsKey(layerName) && _layersGameObject.ContainsKey(layerName))
        {
            _layersGameObject[layerName].Add(gameObject);
            gameObject.transform.SetParent(_layers[layerName].transform);
        }
    }
    public rangeHelper getPrefixedObjectRange(string prefix)
    {
        if (_prefixedObjects == null)
        {
            return null;
        }

        if (_prefixedObjects.ContainsKey(prefix))
        {
            return _prefixedObjects[prefix];
        }

        return null;
    }
}
