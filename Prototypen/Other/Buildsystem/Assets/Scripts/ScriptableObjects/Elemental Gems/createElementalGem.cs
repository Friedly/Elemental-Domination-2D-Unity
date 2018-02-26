using UnityEngine;
using UnityEditor;

public class CreateElementalGem
{
	[MenuItem("Elemental Domination/Create/Elemental Gem")]
	public static void createElementalGem()
	{
		elementalGem newElementalGem = ScriptableObject.CreateInstance<elementalGem>();

		AssetDatabase.CreateAsset (newElementalGem, "Assets/Resources/ScriptableObjects/Elemental Gems/NewElementalGem.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = newElementalGem;
	}
}
