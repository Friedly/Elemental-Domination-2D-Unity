using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class elementaryAffection : singleton<elementaryAffection>
{
    public int fireLevel = 0;
    public int waterLevel = 0;
    public int airLevel = 0;
    public int earthLevel = 0;

    private int _waterChance;
    private int _fireChance;
    private int _earthChance;
    private int _airChance;

    private GameObject _parentPanel;
    private GameObject _text;
    private Text[] _texts = new Text[4];
  
    protected elementaryAffection() 
    {
	}
    public void initiate()
    {
        if (_parentPanel == null)
        {
            _parentPanel = GameObject.Find("affectionpanel");
        }

        _texts[0] = _parentPanel.transform.Find("airchancetext").GetComponent<Text>();
        _texts[1] = _parentPanel.transform.Find("firechancetext").GetComponent<Text>();
        _texts[2] =  _parentPanel.transform.Find("earthchancetext").GetComponent<Text>();
        _texts[3] = _parentPanel.transform.Find("waterchancetext").GetComponent<Text>();

        reset();
    }
	public void updateOnChange () 
    {
        int gesamt = fireLevel + waterLevel + airLevel + earthLevel;

        float fire, water, air, earth;

        if (gesamt > 0)
        {
            air = (0.2f * 0.25f) + (0.4f * ((1f - ((float)fireLevel / gesamt)) / 3)) + (0.4f * ((((float)earthLevel / gesamt))));
            fire = (0.2f * 0.25f) + (0.4f * ((1f - ((float)waterLevel / gesamt)) / 3)) + (0.4f * ((((float)airLevel / gesamt))));
            earth = (0.2f * 0.25f) + (0.4f * ((1f - ((float)airLevel / gesamt)) / 3)) + (0.4f * ((((float)waterLevel / gesamt))));
            water = (0.2f * 0.25f) + (0.4f * ((1f - ((float)earthLevel / gesamt)) / 3)) + (0.4f * ((((float)fireLevel / gesamt))));
        }
        else
        { 
            water=0.25f;
            earth=0.25f;
            fire=0.25f;
            air=0.25f;
        }

        _waterChance = (int)(water * 1000);
        _fireChance = (int)(fire * 1000);
        _earthChance = (int)(earth * 1000);
        _airChance = (int)(air * 1000);

        _texts[0].text = "" + _airChance / 10.0f + "%";
        _texts[1].text = "" + _fireChance / 10.0f + "%";
        _texts[2].text = "" + _earthChance / 10.0f + "%";
        _texts[3].text = "" + _waterChance / 10.0f + "%";
	}

    public void reset()
    {
        fireLevel = 0;
        waterLevel = 0;
        airLevel = 0;
        earthLevel = 0;

        updateOnChange();
    }
    public int waterChance
    {
        get
        {
            return _waterChance;
        }
    }
    public int fireChance
    {
        get
        {
            return _fireChance;
        }
    }
    public int earthChance
    {
        get
        {
            return _earthChance;
        }
    }
    public int airChance
    {
        get
        {
            return _airChance;
        }
    }
}
