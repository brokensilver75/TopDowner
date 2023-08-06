using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(DungeonGeneration))]
public class DungeonCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DungeonGeneration dungeoncreator = (DungeonGeneration)target;
        if (GUILayout.Button("CreateNewDungeon"))
        {
            dungeoncreator.CreateDungeon();
        }
    }
    
}
