using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wavesystem : MonoBehaviour
{
    private int _level;
    private Slider _timebetweenslider;
    private Slider _invadercountslider;
    private Text _slidertext;
    private Text _valuetext;

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
        _level = level;

        _invaderCount = invaderCount;
        _spawnCount = invaderCount;
        _timeBetweenWaves = timeBetweenWaves;
        _timeBetweenInvaders = timeBetweenInvaders;

        _invaderTimeHelper.maxTime = _timeBetweenInvaders;
        _waveTimeHelper.maxTime = timeBetweenWaves;
        _waveTimeHelper.reset();

        searchforui();
        _invadercountslider.gameObject.SetActive(false);
    }

    public void initiate(int level, int invaderCount, float timeBetweenInvaders, float timeBetweenWaves, triggerWaypoint startWaypoint, GameObject[] invadertypes)
    {
        _level = level;

        _invaderCount = invaderCount;
        _spawnCount = invaderCount;
        _timeBetweenWaves = timeBetweenWaves;
        _timeBetweenInvaders = timeBetweenInvaders;

        _invaderTimeHelper.maxTime = _timeBetweenInvaders;
        _waveTimeHelper.maxTime = timeBetweenWaves;
        _waveTimeHelper.reset();

        _startWaypoint = startWaypoint;
        _invadertypes = invadertypes;

        searchforui();
        _invadercountslider.gameObject.SetActive(false);
    }
    private void searchforui()
    { 
        if (_timebetweenslider == null)
        {
            GameObject slider = GameObject.Find("timebetweenslider");
            _timebetweenslider = slider.GetComponent<Slider>();
            _timebetweenslider.maxValue = _timeBetweenWaves;
            _timebetweenslider.minValue = 0;
        }
        if (_invadercountslider == null)
        {
            GameObject slider = GameObject.Find("invadercountslider");
            _invadercountslider = slider.GetComponent<Slider>();
            _invadercountslider.maxValue = _invaderCount;
            _invadercountslider.minValue = 0;
            _invadercountslider.value = _invaderCount;


        }
        if (_slidertext == null)
        {
            GameObject text = GameObject.Find("slidertext");
            _slidertext = text.GetComponent<Text>();
            _slidertext.text = "";
        }

        if (_valuetext == null)
        {
            GameObject text = GameObject.Find("valuetext");
            _valuetext = text.GetComponent<Text>();
            _valuetext.text = "";
        }
    }
	public void update () 
    {
        if (_timebetweenslider.gameObject.activeInHierarchy)
        {
            _slidertext.text = "Welle " + (_level+1) + " in";
            _valuetext.text = "" + (int)_waveTimeHelper.currentTime + " sec";
        }
        else if (_invadercountslider.gameObject.activeInHierarchy)
        {
            _slidertext.text = "Aktuelle Welle "+_level;
            _valuetext.text = "" + _invadercountslider.value;
        }

        if (_waveTimeHelper.currentTime <= 0.0f)
        {
            _timebetweenslider.gameObject.SetActive(false);
            _invadercountslider.gameObject.SetActive(true);
            //_slidertext.text = "Verteidige Dich!";
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
            _timebetweenslider.value = _timeBetweenWaves - _waveTimeHelper.currentTime;
            if (_waveTimeHelper.currentTime <= 0.0f)
            {
                _level++;
            }
        }

        if (invader.invaderCount == 0 && _spawnCount==0)
        {
            _invadercountslider.value = _invaderCount;
            _spawnCount = _invaderCount;
            _waveTimeHelper.maxTime = _timeBetweenWaves;
            _waveTimeHelper.reset();

            _timebetweenslider.value = 0;
            _timebetweenslider.gameObject.SetActive(true);
            _invadercountslider.gameObject.SetActive(false);
            //_slidertext.text = "Ruhephase";
        }
	}
    private void spawn()
    {
        if (_spawnCount > 0)
        {
            if (_startWaypoint != null)
            {

                int element = Random.Range(0, 1000);

                if (element < elementaryAffection.instance.airChance)
                    element = 0;

                if (element >= elementaryAffection.instance.airChance && element < (elementaryAffection.instance.airChance + elementaryAffection.instance.earthChance))
                    element = 1;

                if (element >= (elementaryAffection.instance.airChance + elementaryAffection.instance.earthChance) && element < (elementaryAffection.instance.airChance + elementaryAffection.instance.earthChance + elementaryAffection.instance.fireChance))
                    element = 2;

                if (element >= (elementaryAffection.instance.airChance + elementaryAffection.instance.earthChance + elementaryAffection.instance.fireChance))
                    element = 3;

                if (_invadertypes == null || _invadertypes.Length <= element)
                {
                    Debug.LogError("(wavesystem:spawn) Keine Invasoren zum erstellen verfügbar.");

                    return;
                }

                GameObject invader = null;
                GameObject healthbar = null;

                switch (element)
                {
                    case 0:
                        invader = poolmanager.instance.getPooledObject("airInvader");
                        break;
                    case 1:
                        invader = poolmanager.instance.getPooledObject("earthInvader");
                        break;
                    case 2:
                        invader = poolmanager.instance.getPooledObject("fireInvader");
                        break;
                    case 3:
                        invader = poolmanager.instance.getPooledObject("waterInvader");
                        break;
                    default:
                        break;
                }

                healthbar = poolmanager.instance.getPooledObject("healthbar");

                if (invader)
                {
                    invader newInvader = invader.GetComponent<invader>();

                    followtarget follow = healthbar.GetComponent<followtarget>();
                    healthbar bar = healthbar.GetComponent<healthbar>();

                    if (newInvader)
                    {
                        newInvader.transform.position = _startWaypoint.transform.position;
                        newInvader.movement.currentWaypoint = null;
                        newInvader.movement.nextWaypoint = null;

                        /*
                         *
                         * SKALIERUNG 
                         * 
                         */

                        newInvader.health.maximumHealth = 100.0f;
                        newInvader.health.reset();

                        newInvader.movement.movementSpeed = 1;
                        newInvader.lifedamage.damage = 1;
                        newInvader.elementalenergy.energy = 10;

                        /*
                         *
                         * ENDE 
                         * 
                         */

                        newInvader.gameObject.SetActive(true);

                        if (bar)
                        {
                            bar.healthObject = newInvader.health;
                            bar.reset();
                        }

                        if (follow)
                        {
                            follow.targetToFollow = newInvader.transform;
                            follow.Update();
                            follow.gameObject.SetActive(true);
                        }

                        _spawnCount--;
                    }
                }
            }
        }
    }
}
