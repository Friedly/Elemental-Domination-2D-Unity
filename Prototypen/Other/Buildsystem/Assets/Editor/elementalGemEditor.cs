using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(elementalGem))]
public class elementalGemEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update ();

		elementalGem gem = (elementalGem)target;
		gem.updateLevelDependencies ();

		serializedObject.ApplyModifiedProperties ();

		DrawDefaultInspector ();
	}
}
