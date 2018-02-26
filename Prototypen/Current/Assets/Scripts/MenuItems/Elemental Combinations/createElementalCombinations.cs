using UnityEngine;
using UnityEditor;

public class createElementalCombinations
{
	[MenuItem("Elemental Domination/Create/Elemental Combinations")]
    public static void createCombinations()
	{
		elementalGem[] gems = Resources.FindObjectsOfTypeAll<elementalGem> ();

		if(gems == null)
		{
			return ;
		}

		elementalCombination newElementalCombination = null;

		foreach (elementalGem gem in gems) 
		{
			newElementalCombination = ScriptableObject.CreateInstance<elementalCombination>();
			newElementalCombination.combinationName = gem.gemName;
            newElementalCombination.element = gem.gemName;
            newElementalCombination.range = 1;
            newElementalCombination.damage = 1;
			AssetDatabase.CreateAsset (newElementalCombination, "Assets/Resources/ScriptableObjects/Elemental Combinations/" + gem.gemName + "Combination.asset");

			foreach (elementalGem secondGem in gems) 
			{
				newElementalCombination = ScriptableObject.CreateInstance<elementalCombination>();
				newElementalCombination.combinationName = gem.gemName + secondGem.gemName;
                newElementalCombination.element = gem.gemName;
                newElementalCombination.range = 2;
                newElementalCombination.damage = 1;
                newElementalCombination.attackspeed = 1.0f;
                AssetDatabase.CreateAsset (newElementalCombination, "Assets/Resources/ScriptableObjects/Elemental Combinations/" + gem.gemName + secondGem.gemName + "Combination.asset");
			}
		}

		AssetDatabase.SaveAssets ();
		
		EditorUtility.FocusProjectWindow ();
	}
}
