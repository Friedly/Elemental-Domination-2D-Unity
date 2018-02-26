using UnityEngine;
using System.Collections;

[System.Serializable]
public class elementalGem : ScriptableObject 
{
	public string gemName = "New Elemental Gem";
	public Sprite gemAppearance;
	//public int gemLevel = 0;
	public element gemElement;

	//[readonlyAttribute]
	//public elementalDamage actualDamage;
	public elementalDamage damage;

	public void changeLevel(int amount)
	{
		//gemLevel += amount;

		//updateLevelDependencies ();
	}

	public void updateLevelDependencies()
	{
		//actualDamage.damageAgainstFire = initialDamage.damageAgainstFire + (gemLevel * initialDamage.damageAgainstFire);
		//actualDamage.damageAgainstEarth = initialDamage.damageAgainstEarth + (gemLevel * initialDamage.damageAgainstEarth);
		//actualDamage.damageAgainstWind = initialDamage.damageAgainstWind + (gemLevel * initialDamage.damageAgainstWind);
		//actualDamage.damageAgainstWater = initialDamage.damageAgainstWater + (gemLevel * initialDamage.damageAgainstWater);
	}
}

/*
 * EDITOR SCRIPTING TUTORIALS:
 * 
 * http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/editor-basics/editor-scripting-intro
 * http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/writing-plugins
 * http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/scriptable-objects
 * http://unity3d.com/learn/tutorials/modules/intermediate/live-training-archive/property-drawers-custom-inspectors
 */
