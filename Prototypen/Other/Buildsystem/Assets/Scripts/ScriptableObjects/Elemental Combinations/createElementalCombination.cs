using UnityEngine;
using UnityEditor;

public class CreateElementalCombination
{
	[MenuItem("Elemental Domination/Create/Elemental Combinations")]
	public static void createElementalGem()
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
			AssetDatabase.CreateAsset (newElementalCombination, "Assets/Resources/ScriptableObjects/Elemental Combinations/" + gem.gemName + "Combination.asset");

			foreach (elementalGem secondGem in gems) 
			{
				newElementalCombination = ScriptableObject.CreateInstance<elementalCombination>();
				newElementalCombination.combinationName = gem.gemName + secondGem.gemName;
				AssetDatabase.CreateAsset (newElementalCombination, "Assets/Resources/ScriptableObjects/Elemental Combinations/" + gem.gemName + secondGem.gemName + "Combination.asset");
			}
		}

		AssetDatabase.SaveAssets ();
		
		EditorUtility.FocusProjectWindow ();
	}
}
