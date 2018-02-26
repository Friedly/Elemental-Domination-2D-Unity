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

    protected elementaryAffection() 
    {
	}
    public void initiate()
    {
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
