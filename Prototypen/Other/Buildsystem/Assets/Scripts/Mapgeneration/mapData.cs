using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapData : MonoBehaviour 
{
    public List<string> layers = null;

    private int _mapSize = 15;
    private int _minMapSize = 15;
    private int _maxMapSize = 75;
    private Dictionary<string, List<int>> _boardLayers = null;

    private bool isOutOfBoundaries(int positionX, int positionY)
    {
        if (positionX < 0 || positionX >= _mapSize || positionY < 0 || positionY >= _mapSize)
        {
            return true;
        }

        return false;
    }

    public void Awake()
    {
        if (gamemanager.instance.debug)
        {
            Debug.Log("(mapData:Awake) has been instantiated.");
        }
    }
    public void initiate()
    {
        if (layers == null)
        {
            return;
        }

        if (_boardLayers == null)
        {
            _boardLayers = new Dictionary<string, List<int>>();
        }

        _boardLayers.Clear();

        _mapSize = Mathf.Clamp(_mapSize, _minMapSize, _maxMapSize);

        if (gamemanager.instance.debug)
        {
            Debug.Log("(mapData:initiate) The size of the map is set to " + _mapSize + ".");
        }

        foreach (string layerName in layers)
        {
            List<int> layerList = new List<int>();

            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
                {
                    layerList.Add(0);
                }
            }

            if (gamemanager.instance.debug)
            {
                Debug.Log("(mapData:initiate) Created layer: " + layerName + ".");
            }

            _boardLayers.Add(layerName, layerList);
        }

        if (gamemanager.instance.debug)
        {
            Debug.Log("(mapData:initiate) has been initiated.");
        }
    }
    public void setTileOnLayer(int positionX, int positionY, string layerName, int tileID)
    {
        if (isOutOfBoundaries(positionX, positionY))
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapData:setTileOnLayer) Could not set tile on " + positionX + " | " + positionY + " because the position is out of bonds.");
            }

            return;
        }

        if (_boardLayers == null)
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapData:setTileOnLayer) has not been instantiated.");
            }

            return;
        }

        if (_boardLayers.ContainsKey(layerName))
        {
            _boardLayers[layerName][positionY * _mapSize + positionX] = tileID;
        }
        else
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapData:setTileOnLayer) Could not set tile on layer " + layerName + ". This layer does not exist.");
            }
        }
    }
    public int getTileOnLayer(int positionX, int positionY, string layerName)
    {
        if (isOutOfBoundaries(positionX, positionY))
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapData:getTileOnLayer) Could not get tile on " + positionX + " | " + positionY + " because the position is out of bonds.");
            }

            return -1;
        }

        if (_boardLayers == null)
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapData:getTileOnLayer) has not been instantiated.");
            }

            return -1;
        }

        if (_boardLayers.ContainsKey(layerName))
        {
            return _boardLayers[layerName][positionY * _mapSize + positionX];
        }

        if (gamemanager.instance.debug)
        {
            Debug.LogWarning("(mapData:getTileOnLayer) Could not get tile on layer " + layerName + ". This layer does not exist.");
        }

        return -1;
    }
    public List<int> getBoardLayer(string layerName)
    {
        if (_boardLayers == null)
        {
            return null;
        }

        if (_boardLayers.ContainsKey(layerName))
        {
            return _boardLayers[layerName];
        }

        return null;
    }
    public Dictionary<string, List<int>> getBoard()
    {
        return _boardLayers;
    }

    public int mapSize
    {
        set
        {
            _mapSize = value;

            _mapSize = Mathf.Clamp(_mapSize, _minMapSize, _maxMapSize);

            initiate();
        }

        get
        {
            return _mapSize;
        }
    }
}
