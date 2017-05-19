using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Seek))]
public class SeekEditor : Editor
{


    // Use this for initialization
    public override void OnInspectorGUI()
    {
        GameObject script = ((MonoBehaviour)target).gameObject;
        GUILayout.BeginVertical("Box");
        GUILayout.Label("AI Settings", EditorStyles.boldLabel);
        script.GetComponent<Seek>().useAStarAlgorithm = EditorGUILayout.Toggle("Use A Star Algorithm", script.GetComponent<Seek>().useAStarAlgorithm);
        if (!script.GetComponent<Seek>().isUsingNodes)
        {
            GUILayout.Label("Target to track to", EditorStyles.boldLabel);
            script.GetComponent<Seek>().objectToSeekTo = (GameObject)EditorGUILayout.ObjectField(script.GetComponent<Seek>().objectToSeekTo, typeof(GameObject), true);
        }
        script.GetComponent<Seek>().toSeek = EditorGUILayout.Toggle("Currently Seeking?", script.GetComponent<Seek>().toSeek);
        GUILayout.Label("Node Settings", EditorStyles.boldLabel);
        script.GetComponent<Seek>().isUsingNodes = EditorGUILayout.Toggle("Use Custom Places Travel Nodes?", script.GetComponent<Seek>().isUsingNodes);
        if (script.GetComponent<Seek>().isUsingNodes)
            script.GetComponent<Seek>().updateRouteEveryFrame = EditorGUILayout.Toggle("Update Route Every Frame?", script.GetComponent<Seek>().updateRouteEveryFrame);
        GUILayout.EndVertical();
        //Creates a GUI box that has options for A star Algorithim? - Target to Seek to - Wether we are seeking? - Use Custom Nodes & Update every frame?
        if (script.GetComponent<Seek>().isUsingNodes)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Tree Editor", EditorStyles.boldLabel);
            script.GetComponent<Seek>().clearNodesOnTreeAdd = EditorGUILayout.Toggle("Clear list on Add?", script.GetComponent<Seek>().clearNodesOnTreeAdd);
            GUILayout.Label("Nodes Parent Object", EditorStyles.boldLabel);
            script.GetComponent<Seek>().treeOfNodes = (GameObject)EditorGUILayout.ObjectField(script.GetComponent<Seek>().treeOfNodes, typeof(GameObject), true);
            var list = script.GetComponent<Seek>().nodes;
            int newCount = Mathf.Max(0, EditorGUILayout.IntField("Amount of Nodes:", list.Count));
            while (newCount < list.Count)
                list.RemoveAt(list.Count - 1);
            while (newCount > list.Count)
                list.Add(null);
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = (GameObject)EditorGUILayout.ObjectField(list[i], typeof(GameObject), true);
            }
            //Adding nodes to a custom editor script list of gameobjects for visual purposes.
            if (GUILayout.Button("Update List with Tree"))
            {
                script.GetComponent<Seek>().InsertTreeOfNodes();
            }
            if (GUILayout.Button("Clear list of nodes"))
            {
                script.GetComponent<Seek>().ClearTreeOfNodes();
            }
            GUILayout.EndVertical();
            //Ability to add a list of nodes and clear the list of nodes
        }

    }
}
