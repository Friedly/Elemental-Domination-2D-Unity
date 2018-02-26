using UnityEngine;
using System.Collections;

[System.Serializable]
public class towerState
{
	public Sprite state;
	public int onLevel;
}

[System.Serializable]
public class towerAttributes
{
	[readonlyAttribute]
	public float timeTillNextAttack = 0.0f;
	public float rechargeTime;
	
	public float range;
}

public class tower : MonoBehaviour 
{
    public string towerName;

    public Sprite towerIcon;
	//public towerAttributes towerAttributes;
	public towerState[] towerStates;

	//public elementalGem primaryGem;
	//public elementalGem supportGem;

    //[readonlyAttribute]
	//public elementalDamage damage;
    //[readonlyAttribute]
	//public elementalCombination combination;

	public void Awake()
	{
		//update();
	}

    public void Start()
    {
        tower tower = gameObject.GetComponent<tower>();

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = tower.towerStates[0].state;
    }

	public void update()
	{
        /*
		string combinationName = "";

		if(primaryGem != null)
		{
			combinationName = primaryGem.gemName;

			damage.damageAgainstFire = primaryGem.actualDamage.damageAgainstFire;
			damage.damageAgainstEarth = primaryGem.actualDamage.damageAgainstEarth;
			damage.damageAgainstWater = primaryGem.actualDamage.damageAgainstWater;
			damage.damageAgainstWind = primaryGem.actualDamage.damageAgainstWind;
		}

		if(supportGem != null)
		{
			combinationName = primaryGem.gemName + supportGem.gemName;
		}

		combination = Resources.Load("ScriptableObjects/Elemental Combinations/" + combinationName + "Combination") as elementalCombination;
         * * */
    }
         
}
