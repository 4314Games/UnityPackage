using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Patrol))]
public class PatrolEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Patrol script = (Patrol)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Update List with Tree"))
        {
            script.InsertTreeOfNodes();
        }
        if (GUILayout.Button("Clear list of nodes"))
        {
            script.ClearTreeOfNodes();
        }
    }
}
