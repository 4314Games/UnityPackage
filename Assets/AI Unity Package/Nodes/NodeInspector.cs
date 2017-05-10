using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Node))]
public class NodeInspector : Editor
{
    bool first;
    void OnSceneGUI()
    {
        if (!first)
        {
            Node myScript = (Node)target;
            //if (Event.current.type == EventType.MouseDown)  //Double click connects node.
            //{
            //    Debug.Log("LftClick");
            //}
                //Debug.Log("Time:" + EditorApplication.timeSinceStartup + " | Current Node:", myScript.pathingScript.nodeLinking.gameObject);
                myScript.StartConnectingNode();
            //Event.current.Use();
        //}
            first = true;
        }
        //else if (Event.current.type == EventType.MouseMove)
        //{
        //    if (myScript.pathingScript.isLinking) Selection.activeGameObject = myScript.pathingScript.nodeLinking;
        //}
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        Node myScript = (Node)target;

        //Linking Button
        GUILayout.BeginVertical("box");
        //if (myScript.isLinking)      //Toggle Linking nodes on/off
        //{
        //    GUILayout.Label("                                 Currently Linking");
        //    if (GUILayout.Button("Link Nodes"))
        //    {
        //        myScript.isLinking = false;
        //        myScript.LinkerOff();
        //    }
        //}
        //else
        //{
        //    GUILayout.Label("                                   Not Linking");
        //    if (GUILayout.Button("Link Nodes"))    //if another node isnt already linking
        //    {
        //        if (myScript.GetPathLinking())
        //        {
        //            myScript.PrintStuff("Another Node is Currently Linking");
        //        }
        //        else
        //        {
        //            myScript.isLinking = true;
        //            myScript.LinkerOn();
        //        }
        //    }
        //}
        GUILayout.EndVertical();

        GUILayout.Space(10);

        //Linking/Unlinking Inspector Code
        GUILayout.BeginVertical("box");
        GUILayout.BeginHorizontal();
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Linked Nodes", GUILayout.Width(95));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("                   Unlink Nodes");
        GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        for (int x = 0; x < myScript.connectedNodes.Count; x++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(myScript.connectedNodes[x].name, GUILayout.Width(100));
            if (GUILayout.Button("X")) UnconnectNode(myScript.connectedNodes[x]);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        //GUILayout.Space(10);

        //GUILayout.BeginVertical("box");
        //if (GUILayout.Button("Delete Node")) DeleteNode();
        //GUILayout.EndVertical();

    }

    void UnconnectNode(GameObject p_node)
    {
        Node myScript = (Node)target;
        myScript.RemoveConnectedNode(p_node);
        SceneView.RepaintAll();
    }

    //void DeleteNode()
    //{
    //    Node myScript = (Node)target;
    //    myScript.GameOverMan();
    //}
}