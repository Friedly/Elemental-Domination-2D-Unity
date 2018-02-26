[System.Serializable]
public class elemantaryAffection
{
    public static int fireLevel = 0;
    public static int waterLevel = 0;
    public static int airLevel = 0;
    public static int earthLevel = 0;

    public int waterChance;
    public int fireChance;
    public int earthChance;
    public int airChance;
  
    public elemantaryAffection() 
    {
        waterChance = 250;
        fireChance = 250;
        earthChance = 250;
        airChance = 250;
	}
	
	public void updateOnChange () 
    {
        int gesamt = fireLevel + waterLevel + airLevel + earthLevel;

        float fire, water, air, earth;

        water = (0.4f * 0.25f) + ((1f - ((float)fireLevel / gesamt) / 3));
        earth = (0.4f * 0.25f) + ((1f - ((float)waterLevel / gesamt) / 3));
        fire = (0.4f * 0.25f) + ((1f - ((float)airLevel / gesamt) / 3));
        air = (0.4f * 0.25f) + ((1f - ((float)earthLevel / gesamt) / 3));

        waterChance = (int)(water * 1000);
        fireChance = (int)(fire * 1000);
        earthChance = (int)(earth * 1000);
        airChance = (int)(air * 1000);
	}

    public void reset()
    {
        waterChance = 250;
        fireChance = 250;
        earthChance = 250;
        airChance = 250;
    }
}
