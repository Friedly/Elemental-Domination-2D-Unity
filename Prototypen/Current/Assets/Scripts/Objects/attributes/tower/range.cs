using UnityEngine;
using System.Collections;

[System.Serializable]
public class range
{
    [readonlyAttribute]
    public int _tileRange;
    [readonlyAttribute]
    public int correctedTileRange;

    public void correctTileRange()
    {
        if (_tileRange % 2 == 0)
        {
            correctedTileRange = _tileRange + 1;
        }
        else
        {
            correctedTileRange = _tileRange;
        }
    }

    public int tileRange
    {
        set
        {
            _tileRange = value;
            correctTileRange();
        }
    }
}
