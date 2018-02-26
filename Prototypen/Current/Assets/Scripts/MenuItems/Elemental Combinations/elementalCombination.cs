using UnityEngine;
using System.Collections;

[System.Serializable]
public class elementalCombination : ScriptableObject 
{
	[readonlyAttribute]
	public string combinationName;
    [readonlyAttribute]
    public string element;
	public GameObject effect;
    public GameObject projectile;
    public float damage;
    public int range;
    public float attackspeed;
}
