using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    public List<modification> modifikationlist;
    private List<modification> currentModifikationlist = new List<modification>();
    private Dictionary<int, modification> _modifikationdicta = new Dictionary<int, modification>();
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

        listToDict();
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

        listToDict();
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
            _slidertext.text = "Wave " + (_level+1) + " in";
            _valuetext.text = "" + (int)_waveTimeHelper.currentTime + " seconds";
        }
        else if (_invadercountslider.gameObject.activeInHierarchy)
        {
            _slidertext.text = "Current Wave: " + _level;
            _valuetext.text = "" + _invadercountslider.value + "/" + _invaderCount;
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
                        //newInvader.health.reset();

                        newInvader.movement.movementSpeed = 1;
                        newInvader.lifedamage.damage = 1;
                        newInvader.elementalenergy.energy = 10;


                        //modmanage.anwenden(IDS, newInvader);
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

    private void listToDict()
    {
        int i = 0;
        foreach (modification mod in modifikationlist)
        {
            _modifikationdicta.Add(i++, mod);
        }
    }

    private void createRandomModis(int numbertocreate)
    {
        currentModifikationlist.Clear();

        if (numbertocreate > modifikationlist.Count)
        {
            numbertocreate = modifikationlist.Count;
        }

        //List<modification> moditochoose = new List<modification>();
        //moditochoose = modifikationlist;
        randomHelper rndhelper = new randomHelper();
        //rndhelper.setupValues();

        for (int i = 0; i < numbertocreate; i++)
        {
            int rand = rndhelper.getRandom();
            rndhelper.sortoutRandomValue(rand);
            
            //Random.Range(0, _modifikationdicta.Count);
            currentModifikationlist.Add(_modifikationdicta[rand]);
            //_modifikationdicta.Remove(rand);
            //moditochoose.RemoveAt(rand);
        }
    }
}
