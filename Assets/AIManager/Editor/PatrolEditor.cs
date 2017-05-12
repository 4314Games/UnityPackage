using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Patrol))]
public class PatrolEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GameObject script = ((MonoBehaviour)target).gameObject;
        DrawDefaultInspector();
        if (GUILayout.Button("Update List with Tree"))
        {
            script.GetComponent<Patrol>().InsertTreeOfNodes();
        }
        if (GUILayout.Button("Clear list of nodes"))
        {
            script.GetComponent<Patrol>().ClearTreeOfNodes();
        }
    }
}
