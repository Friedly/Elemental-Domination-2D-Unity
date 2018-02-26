using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(tower))]
public class towerEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update ();

		tower tower = (tower)target;

		tower.update ();

		serializedObject.ApplyModifiedProperties ();

		DrawDefaultInspector ();
	}
}
