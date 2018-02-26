using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gamemanager : singleton<gamemanager>
{
    public bool debug = false;
    public int mapSize = 15;

    private GameObject _map = null;

    protected gamemanager()
    {
    }

    public void initiate()
    {
        if (_map == null)
        {
            _map = new GameObject("("+mapSize+"x"+mapSize+") map");
            mapData data = _map.AddComponent<mapData>();
            mapGfx gfx = _map.AddComponent<mapGfx>();
            mapGenerator generator = _map.AddComponent<mapGenerator>();

            List<string> layerNames = new List<string>() { "ground", "path", "decoration", "tower" };

            data.mapSize = mapSize;
            data.layers = layerNames;
            data.initiate();

            layerNames.Add("waypoint");

            gfx.prefixIdentifier = new List<string>() 
            {   
                "basis", 
                "ground", 
                "spawn", 
                "bottomLeftPath",
                "bottomRightPath", 
                "horizontalPath",
                "upperLeftPath",
                "upperRightPath",
                "verticalPath"
            };

            gfx.initiate(layerNames);

            generator.buildBoardFoundation(data, gfx);
            generator.buildRandomBoard(data, gfx);

            gfx.instantiateBoard(data.getBoard(), data.mapSize);
        }
        else
        {
            Destroy(_map);
            _map = null;

            initiate();
        }
    }

    public mapData mapData
    {
        get 
        { 
            return _map.GetComponent<mapData>();
        }
    }

    public mapGfx mapGfx
    {
        get
        {
            return _map.GetComponent<mapGfx>();
        }
    }

    public mapGenerator mapGenerator
    {
        get
        {
            return _map.GetComponent<mapGenerator>();
        }
    }
}
