using UnityEngine;
using System.Collections;

public class wavesystem : MonoBehaviour
{
    public elemantaryAffection elementaryAffection;
    public int level;

    private int _spawnCount;
    private int _invaderCount;
    private float _timeBetweenWaves;
    private float _timeBetweenInvaders;

    private timerHelper _invaderTimeHelper = new timerHelper();
    private timerHelper _waveTimeHelper = new timerHelper();
    private triggerWaypoint _startWaypoint;
    private GameObject[] _invadertypes;

    public void initiate(int level, int invaderCount, float timeBetweenInvaders, float timeBetweenWaves)
    {
        this.level = level;

        _invaderCount = invaderCount;
        _spawnCount = invaderCount;
        _timeBetweenWaves = timeBetweenWaves;
        _timeBetweenInvaders = timeBetweenInvaders;

        _invaderTimeHelper.maxTime = _timeBetweenInvaders;
        _waveTimeHelper.maxTime = 0.0f;
    }

    public void initiate(int level, int invaderCount, float timeBetweenInvaders, float timeBetweenWaves, elemantaryAffection elementaryAffection, triggerWaypoint startWaypoint, GameObject[] invadertypes)
    {
        this.level = level;
        this.elementaryAffection = elementaryAffection;

        _invaderCount = invaderCount;
        _spawnCount = invaderCount;
        _timeBetweenWaves = timeBetweenWaves;
        _timeBetweenInvaders = timeBetweenInvaders;

        _invaderTimeHelper.maxTime = _timeBetweenInvaders;
        _waveTimeHelper.maxTime = 0.0f;

        _startWaypoint = startWaypoint;
        _invadertypes = invadertypes;
    }

	public void update () 
    {
        if (_waveTimeHelper.currentTime <= 0.0f)
        {
            if (_invaderTimeHelper.currentTime <= 0.0f)
            {
                spawn();
                _invaderTimeHelper.reset();
            }

            _invaderTimeHelper.update(Time.deltaTime);
        }
        else
        {
            _waveTimeHelper.update(Time.deltaTime);

            if (_waveTimeHelper.currentTime <= 0.0f)
            {
                level++;
            }
        }

        if (invader.invaderCount == 0 && _spawnCount==0)
        {
            _spawnCount = _invaderCount;
            _waveTimeHelper.maxTime = _timeBetweenWaves;
            _waveTimeHelper.reset();
        }
	}
    private void spawn()
    {
        if (_spawnCount > 0)
        {
            if (_startWaypoint != null)
            {

                int element = Random.Range(0, 1000);

                if (element < elementaryAffection.airChance)
                    element = 0;

                if (element >= elementaryAffection.airChance && element < (elementaryAffection.airChance + elementaryAffection.earthChance))
                    element = 1;

                if (element >= (elementaryAffection.airChance + elementaryAffection.earthChance) && element < (elementaryAffection.airChance + elementaryAffection.earthChance + elementaryAffection.fireChance))
                    element = 2;

                if (element >= (elementaryAffection.airChance + elementaryAffection.earthChance + elementaryAffection.fireChance))
                    element = 3;

                if (_invadertypes == null || _invadertypes.Length <= element)
                {
                    Debug.LogError("(wavesystem:spawn) Keine Invasoren zum erstellen verfügbar.");

                    return;
                }

                GameObject obj = null;

                switch (element)
                {
                    case 0:
                        obj = poolmanager.instance.getPooledObject("airInvader");
                        break;
                    case 1:
                        obj = poolmanager.instance.getPooledObject("earthInvader");
                        break;
                    case 2:
                        obj = poolmanager.instance.getPooledObject("fireInvader");
                        break;
                    case 3:
                        obj = poolmanager.instance.getPooledObject("waterInvader");
                        break;
                    default:
                        break;
                }

                if (obj == null)
                {
                    return;
                }

                invader newInvader = obj.GetComponent<invader>();

                if (newInvader == null)
                {
                    return;
                }

                newInvader.transform.position = _startWaypoint.transform.position;
                newInvader.currentWaypoint = null;
                newInvader.nextWaypoint = null;

                /*
                 *
                 * SKALIERUNG ANFANG
                 * 
                 */

                //newInvader.live....

                /*
                 *
                 * SKALIERUNG ENDE
                 * 
                 */

                newInvader.gameObject.SetActive(true);

                _spawnCount--;
            }
        }
    }
}
