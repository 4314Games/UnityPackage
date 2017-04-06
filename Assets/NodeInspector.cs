using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeInspector : Editor {

	void OnSceneGUI()
    {
        Node myScript = (Node)target;
        if (Event.current.type == EventType.MouseDown)
        {            
            
            myScript.StartConnectingNode();
        }
    }

   

}
