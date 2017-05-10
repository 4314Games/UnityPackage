using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pathing))]
public class PathingEditor : Editor
{


    //test
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        Pathing myScript = (Pathing)target;
        GUILayout.BeginVertical("box");
        GUILayout.Label("                                     Nodes");
        if (myScript.spawningNodes)      //Toggle spawning nodes on/off
        {
            if (GUILayout.Button("Stop Spawning Nodes"))
            {
                myScript.spawningNodes = false;
            }
        }
        else 
        {
            if (GUILayout.Button("Start Spawning Nodes") && !myScript.isLinking)
            {
                myScript.spawningNodes = true;
            }
        }
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Nodes Spawning Height = " + myScript.heightOffGround.ToString("F2"));
        myScript.heightOffGround = GUILayout.HorizontalSlider(myScript.heightOffGround, 0, 10);
        GUILayout.EndVertical();
        if (myScript.isLinking)      //Toggle spawning nodes on/off
        {
            if (GUILayout.Button("Stop Linking Nodes"))
            {
                myScript.isLinking = false;
            }
        }
        else 
        {
            if (GUILayout.Button("Start Linking Nodes") && !myScript.spawningNodes)
            {
                myScript.isLinking = true;
            }
        }
        if (GUILayout.Button("Clear All Nodes"))
        {
            int count = myScript.city.Count;
            Debug.Log("Count = " + count);
            for(int x  = count - 1; x > -1; x--)
            {
                myScript.city[x].GetComponent<Node>().Destroyer();
            } 
        }
        GUILayout.EndVertical();


    }

    Texture getButtonTexture()
    {
        Pathing myScript = (Pathing)target;
        if (myScript.spawningNodes) return myScript.buttonUp;
        else return myScript.buttonDown;
    }

    void OnSceneGUI()
    {
        Pathing myScript = (Pathing)target;
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            if (myScript.spawningNodes && !myScript.isLinking) PrepareNodeSpawn();
        }
        if (myScript.spawningNodes && !myScript.isLinking) Selection.activeGameObject = myScript.gameObject;
    }

    void PrepareNodeSpawn() //Spawn Node
    {
        Pathing myScript = (Pathing)target;
        Event e = Event.current;
        myScript.SpawnNode(GetHitPos(Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight))));
    }

    Vector3 GetHitPos(Ray p_ray)    //Get Location of Mouse/Collision For NodeSpawn
    {
        Ray ray = p_ray;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
        }
        return hit.point;
    }


}