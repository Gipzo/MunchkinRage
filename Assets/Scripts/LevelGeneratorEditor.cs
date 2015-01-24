using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		LevelGenerator myScript = (LevelGenerator)target;
		if(GUILayout.Button("Generate"))
		{
			myScript.Generate();
		}if(GUILayout.Button("Clear"))
		{
			myScript.Clear();
		}
	}
}