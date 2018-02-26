using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gamemanager : singleton<gamemanager>
{
    public bool debug = false;
    public int mapSize = 15;

    private GameObject _map = null;
    private GameObject _wavesystem = null;

    private elemantaryAffection _elementarAffection = new elemantaryAffection();

    protected gamemanager()
    {
    }

    public void initiate()
    {
        poolmanager.instance.initiate();

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

            createWavesystem(generator.startWaypoint);
        }
        else
        {
            Destroy(_map);
            _map = null;

            Destroy(_wavesystem);
            _wavesystem = null;

            _elementarAffection.reset();

            initiate();
        }
    }
    public void update()
    {
        wavesystem wavesystem = _wavesystem.GetComponent<wavesystem>();

        if (wavesystem != null)
        {
            wavesystem.update();
        }
    }
    public void createWavesystem(triggerWaypoint startWaypoint)
    {
        _wavesystem = new GameObject("wavesystem");

        wavesystem wavesystem = _wavesystem.AddComponent<wavesystem>();

        Object[] objects = Resources.LoadAll("Invader");

        GameObject[] invadertypes = new GameObject[4];
        invadertypes[0] = (GameObject)objects[0];
        invadertypes[1] = (GameObject)objects[1];
        invadertypes[2] = (GameObject)objects[2];
        invadertypes[3] = (GameObject)objects[3];

        wavesystem.initiate(0, 18, 1f, 5.0f, _elementarAffection, startWaypoint, invadertypes);

        poolmanager.instance.createPool("airInvader", 6, true, invadertypes[0]);
        poolmanager.instance.createPool("earthInvader", 6, true, invadertypes[1]);
        poolmanager.instance.createPool("fireInvader", 6, true, invadertypes[2]);
        poolmanager.instance.createPool("waterInvader", 6, true, invadertypes[3]);
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
