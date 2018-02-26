using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapGenerator : MonoBehaviour 
{
    /*path private members*/
    private int _minPathLength = 0;
    private int _maxPathLength = 0;
    private int _targetPathLength = 0;
    private int _actualPathLength = 0;
    private int _pathLengthReduktion = 0;
    private List<Path> _pathList = null;

    /*waypoint private members*/
    private waypoint _startWaypoint = null;

    /*basis/spawn private members*/
    private int _spawnX = 0;
    private int _spawnY = 0;
    private int _baseX = 0;
    private int _baseY = 0;

    public void Awake()
    {
        if(gamemanager.instance.debug)
        {
            Debug.Log("(mapGenerator:Awake) has been instantiated.");
        }
    }
    public void initiate(int mapSize, int pathLengthReduktion)
    {
        _maxPathLength = ((mapSize / 2) * (mapSize - 2)) + (mapSize / 2 - 1);
        _minPathLength = _maxPathLength / 2;
        _targetPathLength = _maxPathLength - pathLengthReduktion;
        _pathLengthReduktion = pathLengthReduktion;
        _actualPathLength = 0;

        _spawnX = 0;
        _spawnY = 0;
        _baseX = Random.Range(1, mapSize - 1);
        _baseY = Random.Range(1, mapSize - 1);

        if (_pathList == null)
        {
            _pathList = new List<Path>();
        }

        _pathList.Clear();

        _startWaypoint = null;
    }
    public void buildBoardFoundation(mapData mapData, mapGfx mapGfx)
    {
        if (mapData == null || mapGfx == null)
        {
            if(gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapGenerator:buildBoardFoundation) Could not build foundation because the mapData/Gfx is a nullptr.");
            }

            return;
        }

        List<string> layers = mapData.layers;

        foreach (string layer in layers)
        {
            for (int positionX = 0; positionX < mapData.mapSize; ++positionX)
            {
                for (int positionY = 0; positionY < mapData.mapSize; ++positionY)
                {
                    switch (layer)
                    {
                        case "ground":
                            rangeHelper range = mapGfx.getPrefixedObjectRange("ground");
                            if (range != null)
                            {
                                mapData.setTileOnLayer(positionX, positionY, layer, Random.Range(range.begin, range.end+1));
                            }
                            else
                            {
                                if(gamemanager.instance.debug)
                                {
                                    Debug.LogError("(mapGenerator:buildBoardFoundation) Ground ID range is null.");
                                }
                                mapData.setTileOnLayer(positionX, positionY, layer, 0);
                            }
                            break;
                        case "path":
                            if (positionX == 0 || positionY == 0 || positionY == mapData.mapSize - 1 || positionX == mapData.mapSize - 1)
                            {
                                mapData.setTileOnLayer(positionX, positionY, layer, 1);
                            }
                            else
                            {
                                mapData.setTileOnLayer(positionX, positionY, layer, 0);
                            }
                            break;
                        case "waypoint":
                            break;
                        default:
                            mapData.setTileOnLayer(positionX, positionY, layer, 0);
                            break;
                    }
                }
            }
        }

        if (gamemanager.instance.debug)
        {
            Debug.Log("(mapGenerator:buildBoardFoundation) Foundation built.");
        }
    }
    public void buildBoardFoundationOnLayer(mapData mapData, mapGfx mapGfx, string layerName)
    {
        if (mapData == null || mapGfx == null)
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapGenerator:buildBoardFoundation) Could not build foundation because the mapData/Gfx is a nullptr.");
            }

            return;
        }

        for (int positionX = 0; positionX < mapData.mapSize; ++positionX)
        {
            for (int positionY = 0; positionY < mapData.mapSize; ++positionY)
            {
                switch (layerName)
                {
                    case "ground":
                        rangeHelper range = mapGfx.getPrefixedObjectRange("ground");
                        if (range != null)
                        {
                            mapData.setTileOnLayer(positionX, positionY, layerName, Random.Range(range.begin, range.end + 1));
                        }
                        else
                        {
                            if (gamemanager.instance.debug)
                            {
                                Debug.LogError("(mapGenerator:buildBoardFoundation) Ground ID range is null.");
                            }
                            mapData.setTileOnLayer(positionX, positionY, layerName, 0);
                        }
                        break;
                    case "path":
                        if (positionX == 0 || positionY == 0 || positionY == mapData.mapSize - 1 || positionX == mapData.mapSize - 1)
                        {
                            mapData.setTileOnLayer(positionX, positionY, layerName, 1);
                        }
                        else
                        {
                            mapData.setTileOnLayer(positionX, positionY, layerName, 0);
                        }
                        break;
                    case "waypoint":
                        break;
                    default:
                        mapData.setTileOnLayer(positionX, positionY, layerName, 0);
                        break;
                }
            }
        }
    }
    public void buildRandomBoard(mapData mapData, mapGfx mapGfx)
    {
        int pathBuildTryCount = 1;

        do
        {
            if (gamemanager.instance.debug)
            {
                Debug.LogWarning("(mapGenerator:buildRandomBoard) " + pathBuildTryCount + ". try to build path.");
            }

            buildBoardFoundationOnLayer(mapData, mapGfx, "path");
            buildBoardFoundationOnLayer(mapData, mapGfx, "tower");
            initiate(mapData.mapSize, _pathLengthReduktion);

            mapData.setTileOnLayer(_baseX, _baseY, "path", 2);

            ++pathBuildTryCount;
           _pathLengthReduktion += 5;
        } 
        while (!buildPath(_baseX, _baseY, 1, -1, mapData));

        setupBuiltPath(mapData, mapGfx);
    }

    /*(random generation)waypoint functions*/
    private waypoint createWaypoint(int positionX, int positionY, mapGfx mapGfx, waypoint connectWith, int identification)
    {
        GameObject newWaypoint = new GameObject("waypoint " + identification);

        waypoint waypoint = newWaypoint.AddComponent<waypoint>();

        BoxCollider2D boxCollider2D = newWaypoint.AddComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(0.32f, 0.32f);

        newWaypoint.transform.position = new Vector3(positionX, positionY);

        mapGfx.addGameObject(newWaypoint, "waypoint");

        if (connectWith != null)
        { 
            connectWith.nextwaypoint = waypoint; 
        }

        return waypoint;
    }
    public waypoint startWaypoint
    {
        get
        {
            return _startWaypoint;
        }
    }

    /*(random generation)path functions*/
    private bool buildPath(int positionX, int positionY, int pathLength, int oppositeDirection, mapData mapData)
    {
        randomHelper directionRandomness = new randomHelper();

        /*Initialise all possible direction for the next path tile.*/
        /*Sort out the opposite direction of the previous pathtile to reduce unnecessary checks.*/
        directionRandomness.setupValues(new List<int>() { 0, 1, 2, 3 });
        directionRandomness.sortoutRandomValue(oppositeDirection);

        /*While there is still a possible direction to check.*/
        while(!directionRandomness.isEmpty())
        {
            /*Get a random direction.*/
            int direction = directionRandomness.getRandom();

            /*Set the next tile position to the previous one.*/
            int nextPositionX = positionX;
            int nextPositionY = positionY;

            /*Calculate the opposite direction and the next tile position with the set random direction
			 - no diagonal moves possible.*/
            switch (direction)
            {
                case 0:
                    oppositeDirection = 1;
                    ++nextPositionY;
                    break;
                case 1:
                    oppositeDirection = 0;
                    --nextPositionY;
                    break;
                case 2:
                    oppositeDirection = 3;
                    ++nextPositionX;
                    break;
                case 3:
                    oppositeDirection = 2;
                   --nextPositionX;
                    break;
                default:
                    return false;
            }

            /*Is the next tile not a possible path...*/
            if (!isPossiblePath(nextPositionX, nextPositionY, direction, mapData))
            {
                /*The random direction will be sorted out of the possible directions.*/
                directionRandomness.sortoutRandomValue(direction);

                /*New try with a new direction.*/
                continue;
            }

            /*Is the current path length equal to the target pathlength...*/
            if (pathLength == _targetPathLength)
            {
                /*Create a spawn.*/
                mapData.setTileOnLayer(nextPositionX, nextPositionY, "path", 2);

                /*Set spawn position.*/
                _spawnX = nextPositionX;
                _spawnY = nextPositionY;

                /*Set the pathlength.*/
                _actualPathLength = _targetPathLength;

                /*Reset the target pathlength back to the maximum path length.*/
                _targetPathLength = _maxPathLength;

                _pathList.Add(new Path(direction, nextPositionX, nextPositionY));

                return true;
            }

            /*If it is a possible path create a path tile on that position.*/
            mapData.setTileOnLayer(nextPositionX, nextPositionY, "path", 2);

            /*If the path generation finds a valid path it returns true.*/
            if (buildPath(nextPositionX, nextPositionY, pathLength + 1, oppositeDirection, mapData))
            {
                _pathList.Add(new Path(direction, nextPositionX, nextPositionY));

                return true;
            }
            else
            {
                /*If somewhere the path generation got stucked - the path generation will go back to
                 the previous tile and tries out another direction till he found a valid way. The direction
                 that does not work will be sorted out and the path tile that has been created will be destroyed
                 and a wall will be placed there to symbolize the path generation there is no way in that direction.*/
                directionRandomness.sortoutRandomValue(direction);
                mapData.setTileOnLayer(nextPositionX, nextPositionY, "path", 1);
            }
        }

        /*If all possible direction do not include a possible path the target path length will be reduced
          - to avoid that the path generation gets stucked but it will never go lower than the minimum pathlength
          to generate maps that are worth playing.*/
        if (_targetPathLength > _minPathLength)
        {
            _targetPathLength--;
        }

        return false;
    }
    private void setupBuiltPath(mapData mapData, mapGfx mapGfx)
    {
        waypoint lastWaypoint = null;
        int waypointCount = 0;

        mapData.setTileOnLayer(_spawnX, _spawnY, "path", mapGfx.getPrefixedObjectRange("spawn").getRandom());
        mapData.setTileOnLayer(_baseX, _baseY, "path", mapGfx.getPrefixedObjectRange("basis").getRandom());

        lastWaypoint = createWaypoint(_baseX, _baseY, mapGfx, lastWaypoint, waypointCount++);

        mapData.setTileOnLayer(_baseX, _baseY, "tower", 1);

        for (int pathtileIndex = _pathList.Count - 1; pathtileIndex >= 1; pathtileIndex--)
        {
            int positionX = _pathList[pathtileIndex].positionX;
            int positionY = _pathList[pathtileIndex].positionY;
            int currentDirection = _pathList[pathtileIndex].direction;
            int nextDirection = _pathList[pathtileIndex - 1].direction;

            if(gamemanager.instance.debug)
            {
                Debug.Log("(mapGenerator:setupBuiltPath) Pathtile on " + positionX + "|" + positionY + ".");
            }

            if (currentDirection != nextDirection)
            {
                if ((currentDirection == 0 && nextDirection == 2) || (currentDirection == 3 && nextDirection == 1))
                {

                    mapData.setTileOnLayer(positionX, positionY, "path", mapGfx.getPrefixedObjectRange("upperLeftPath").getRandom());
                }
                else if ((currentDirection == 1 && nextDirection == 2) || (currentDirection == 3 && nextDirection == 0))
                {
                    mapData.setTileOnLayer(positionX, positionY, "path", mapGfx.getPrefixedObjectRange("bottomLeftPath").getRandom());
                }
                else if ((currentDirection == 0 && nextDirection == 3) || (currentDirection == 2 && nextDirection == 1))
                {
                    mapData.setTileOnLayer(positionX, positionY, "path", mapGfx.getPrefixedObjectRange("upperRightPath").getRandom());
                }
                else if ((currentDirection == 1 && nextDirection == 3) || (currentDirection == 2 && nextDirection == 0))
                {
                    mapData.setTileOnLayer(positionX, positionY, "path", mapGfx.getPrefixedObjectRange("bottomRightPath").getRandom());
                }

                lastWaypoint = createWaypoint(positionX, positionY, mapGfx, lastWaypoint, waypointCount++);
            }
            else
            {
                if (currentDirection == 0 || currentDirection == 1)
                {
                     mapData.setTileOnLayer(positionX, positionY, "path", mapGfx.getPrefixedObjectRange("verticalPath").getRandom());
                }
                else if (currentDirection == 2 || currentDirection == 3)
                {
                     mapData.setTileOnLayer(positionX, positionY, "path", mapGfx.getPrefixedObjectRange("horizontalPath").getRandom());
                }
            }

            mapData.setTileOnLayer(positionX, positionY, "tower", 1);
        }

        lastWaypoint = createWaypoint(_spawnX, _spawnY, mapGfx, lastWaypoint, waypointCount++);
        _startWaypoint = lastWaypoint;

        mapData.setTileOnLayer(_spawnX, _spawnY, "tower", 1);
    }
    private bool isPossiblePath(int positionX, int positionY, int direction, mapData data)
    {
        if (data.getTileOnLayer(positionX, positionY, "path") == 1 || data.getTileOnLayer(positionX, positionY, "path") == 2)
        {
            return false;
        }
        /**/
        if (direction == 0 || direction == 1)
        {
            if (data.getTileOnLayer(positionX - 1, positionY, "path") == 2 ||
                data.getTileOnLayer(positionX + 1, positionY, "path") == 2)
            {
                return false;
            }

            if (direction == 0 && data.getTileOnLayer(positionX, positionY + 1, "path") == 2)
            {
                return false;
            }

            if (direction == 1 && data.getTileOnLayer(positionX, positionY - 1, "path") == 2)
            {
                return false;
            }
        }

        if (direction == 2 || direction == 3)
        {
            if (data.getTileOnLayer(positionX, positionY - 1, "path") == 2 ||
                data.getTileOnLayer(positionX, positionY + 1, "path") == 2)
            {
                return false;
            }

            if (direction == 2 && data.getTileOnLayer(positionX + 1, positionY, "path") == 2)
            {
                return false;
            }

            if (direction == 3 && data.getTileOnLayer(positionX - 1, positionY, "path") == 2)
            {
                return false;
            }
        }

        return true;
    }
}
