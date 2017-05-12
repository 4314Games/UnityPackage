using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wander))]
public class WanderEditor : Editor {

    public override void OnInspectorGUI()
    {
        GameObject script = ((MonoBehaviour)target).gameObject;
        DrawDefaultInspector();
        if (GUILayout.Button("Update List with Tree"))
        {
            script.GetComponent<Wander>().InsertTreeOfNodes();
        }
        if (GUILayout.Button("Clear list of nodes"))
        {
            script.GetComponent<Wander>().ClearTreeOfNodes();
        }
    }
}
