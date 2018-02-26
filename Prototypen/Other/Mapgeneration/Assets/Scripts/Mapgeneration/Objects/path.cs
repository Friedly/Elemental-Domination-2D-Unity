public class Path
{
    private int _direction;
    private int _positionX;
    private int _positionY;

    public Path(int direction, int positionX, int positionY)
    {
        _direction = direction;
        _positionX = positionX;
        _positionY = positionY;
    }

    public int direction
    {
        get
        {
            return _direction;
        }

        set
        {
            _direction = value;
        }
    }
    public int positionX
    {
        get
        {
            return _positionX;
        }

        set
        {
            _positionX = value;
        }
    }
    public int positionY
    {
        get
        {
            return _positionY;
        }

        set
        {
            _positionY = value;
        }
    }
}
