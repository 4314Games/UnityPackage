using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wander))]
public class WanderEditor : Editor {

    public override void OnInspectorGUI()
    {
        Wander script = (Wander)target;
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
