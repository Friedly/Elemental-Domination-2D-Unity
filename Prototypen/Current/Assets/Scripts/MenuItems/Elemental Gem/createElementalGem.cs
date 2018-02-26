using UnityEngine;
using UnityEditor;

public class createElementalGem
{
	[MenuItem("Elemental Domination/Create/Elemental Gem")]
	public static void createGem()
	{
        Sprite placeholder = Resources.Load<Sprite>("Sprites/placeholder");

		elementalGem newElementalGem = ScriptableObject.CreateInstance<elementalGem>();
        newElementalGem.gemAppearance = placeholder;

		AssetDatabase.CreateAsset (newElementalGem, "Assets/Resources/ScriptableObjects/Elemental Gems/NewElementalGem.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = newElementalGem;
	}
}
