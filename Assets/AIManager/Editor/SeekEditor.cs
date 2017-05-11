using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Seek))]
public class SeekEditor : Editor
{


    // Use this for initialization
    public override void OnInspectorGUI()
    {
        Seek script = (Seek)target;
        GUILayout.BeginVertical("Box");
        GUILayout.Label("AI Settings", EditorStyles.boldLabel);
        script.useAStarAlgorithm = EditorGUILayout.Toggle("Use A Star Algorithm", script.useAStarAlgorithm);
        if (!script.isUsingNodes)
        {
            GUILayout.Label("Target to track to", EditorStyles.boldLabel);
            script.objectToSeekTo = (GameObject)EditorGUILayout.ObjectField(script.objectToSeekTo, typeof(GameObject), true);
        }
        script.toSeek = EditorGUILayout.Toggle("Currently Seeking?", script.toSeek);
        GUILayout.Label("Node Settings", EditorStyles.boldLabel);
        script.isUsingNodes = EditorGUILayout.Toggle("Use Custom Places Travel Nodes?", script.isUsingNodes);
        if (script.isUsingNodes)
            script.updateRouteEveryFrame = EditorGUILayout.Toggle("Update Route Every Frame?", script.updateRouteEveryFrame);


        GUILayout.EndVertical();
        if (script.isUsingNodes)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Tree Editor", EditorStyles.boldLabel);
            script.clearNodesOnTreeAdd = EditorGUILayout.Toggle("Clear list on Add?", script.clearNodesOnTreeAdd);
            GUILayout.Label("Nodes Parent Object", EditorStyles.boldLabel);
            script.treeOfNodes = (GameObject)EditorGUILayout.ObjectField(script.treeOfNodes, typeof(GameObject), true);
            var list = script.nodes;
            int newCount = Mathf.Max(0, EditorGUILayout.IntField("Amount of Nodes:", list.Count));
            while (newCount < list.Count)
                list.RemoveAt(list.Count - 1);
            while (newCount > list.Count)
                list.Add(null);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = (GameObject)EditorGUILayout.ObjectField(list[i], typeof(GameObject), true);
            }
            //DrawDefaultInspector();
            if (GUILayout.Button("Update List with Tree"))
            {
                script.InsertTreeOfNodes();
            }
            if (GUILayout.Button("Clear list of nodes"))
            {
                script.ClearTreeOfNodes();
            }
            GUILayout.EndVertical();
        }

    }
}
