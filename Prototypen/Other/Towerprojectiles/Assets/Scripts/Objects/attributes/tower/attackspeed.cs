using UnityEngine;
using System.Collections;

[System.Serializable]
public class attackspeed
{
    [readonlyAttribute]
    public float speed;

    private timerHelper _attacktimer = new timerHelper();

    public void reset()
    {
        _attacktimer.maxTime = speed;
        _attacktimer.reset();
    }

    public bool isReadyToAttack(float deltaTime)
    {
        _attacktimer.update(deltaTime);

        if(_attacktimer.currentTime <= 0.0f)
        {
            return true;
        }

        return false;
    }
}
