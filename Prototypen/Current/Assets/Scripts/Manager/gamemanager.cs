using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gamemanager : singleton<gamemanager>
{
    public bool debug = false;
    public int mapSize = 15;

    private GameObject _map = null;
    private GameObject _player = null;
    private GameObject _wavesystem = null;

    protected gamemanager()
    {
    }

    public void initiate()
    {
        poolmanager.instance.initiate();
        elementaryAffection.instance.initiate();

        if (_player == null)
        {
            _player = new GameObject("player");

            playermanager playermanager = _player.AddComponent<playermanager>();

            playermanager.lives = 10;

            playermanager.resources.fireResources  = 100;
            playermanager.resources.airResources = 100;
            playermanager.resources.earthResources = 100;
            playermanager.resources.waterResources = 100;

            playermanager.initiate();
            playermanager.updatePlayerUI();
        }

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
            layerNames.Add("border");

            gfx.prefixIdentifier = new List<string>() 
            {   
                "basis",
                "border",
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

            elementaryAffection.instance.reset();

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

        Object[] objects = Resources.LoadAll("PrefabObjects/Elemental Invader");

        GameObject healthbar = Resources.Load<GameObject>("PrefabObjects/UI Elements/healthbar");

        GameObject[] invadertypes = new GameObject[4];
        invadertypes[0] = (GameObject)objects[0];
        invadertypes[1] = (GameObject)objects[1];
        invadertypes[2] = (GameObject)objects[2];
        invadertypes[3] = (GameObject)objects[3];

        poolmanager.instance.createPool("airInvader", 6, true, invadertypes[0]);
        poolmanager.instance.createPool("earthInvader", 6, true, invadertypes[1]);
        poolmanager.instance.createPool("fireInvader", 6, true, invadertypes[2]);
        poolmanager.instance.createPool("waterInvader", 6, true, invadertypes[3]);

        poolmanager.instance.createPool("healthbar", 10, true, healthbar, GameObject.Find("worldinterface"));

        wavesystem.initiate(0, 10, 1f, 10.0f, startWaypoint, invadertypes);
    }

    public mapData mapData
    {
        get 
        {
            if (_map == null)
            {
                return null;
            }

            return _map.GetComponent<mapData>();
        }
    }
    public mapGfx mapGfx
    {
        get
        {
            if (_map == null)
            {
                return null;
            }

            return _map.GetComponent<mapGfx>();
        }
    }
    public mapGenerator mapGenerator
    {
        get
        {
            if (_map == null)
            {
                return null;
            }

            return _map.GetComponent<mapGenerator>();
        }
    }
    public playermanager playermanager
    {
        get
        {
            return _player.GetComponent<playermanager>();
        }
    }
}
