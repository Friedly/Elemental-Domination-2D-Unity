using UnityEngine;

public class rangeHelper
{
    private int _begin;
    private int _end;

    public rangeHelper(int begin, int end)
    {
        _begin = begin;
        _end = end;
    }
    public int getRandom()
    {
        return Random.Range(_begin, _end+1);
    }
    public int begin
    {
        get
        {
            return _begin;
        }

        set
        {
            _begin = value;
        }
    }
    public int end
    {
        get
        {
            return _end;
        }

        set
        {
            _end = value;
        }
    }
}
