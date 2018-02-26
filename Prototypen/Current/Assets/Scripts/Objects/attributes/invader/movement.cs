using UnityEngine;
using System.Collections;

[System.Serializable]
public class movement
{
    public float movementSpeed;

    private triggerWaypoint _nextWaypoint;
    private triggerWaypoint _currentWaypoint;

    public void update(float deltaTime, Transform transform)
    {
        if (_currentWaypoint != null)
        {
            Vector3 dir = _currentWaypoint.transform.position - transform.position;
            dir.Normalize();

            transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.transform.position, movementSpeed * deltaTime);

            if (_currentWaypoint.transform.position == transform.position)
            {
                _currentWaypoint = _nextWaypoint;
            }
        }
        else
        {
            _currentWaypoint = _nextWaypoint;
        }
    }

    public triggerWaypoint nextWaypoint
    {
        set { _nextWaypoint = value; }
        get { return _nextWaypoint;  }
    }

    public triggerWaypoint currentWaypoint
    {
        set { _currentWaypoint = value; }
        get { return _currentWaypoint; }
    }
}
