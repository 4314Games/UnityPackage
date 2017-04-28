using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Node))]
public class NodeInspector : Editor {
   // bool first;
	void OnSceneGUI()
    {
        //if (!first)
        //{
            Node myScript = (Node)target;
            if (Event.current.type == EventType.MouseDown)  //Double click connects node.
            {

                //Debug.Log("Time:" + EditorApplication.timeSinceStartup + " | Current Node:", myScript.pathingScript.nodeLinking.gameObject);
                myScript.StartConnectingNode();
           // Event.current.Use();
             }
            //first = true;
       // }
        else if (Event.current.type == EventType.MouseMove)
        {
            if (myScript.pathingScript.isLinking) Selection.activeGameObject = myScript.pathingScript.nodeLinking;
        }        
    }    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Node myScript = (Node)target;
        if (myScript.isLinking)      //Toggle Linking nodes on/off
        {
            if (GUILayout.Button("Link Nodes"))
            {
                myScript.isLinking = false;
                myScript.LinkerOff();
            }
        }
        else
        {
            if (GUILayout.Button("Link Nodes"))    //if another node isnt already linking
            {
                if (myScript.GetPathLinking())
                {
                    myScript.PrintStuff("Another Node is Currently Linking");
                } else { 
                    myScript.isLinking = true;
                    myScript.LinkerOn();
                }
            }
        }
    }
}
