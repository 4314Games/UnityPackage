using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pathing))]
public class PathingEditor : Editor {

   
   
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Pathing myScript = (Pathing)target;
        if(myScript.spawningNodes)      //Toggle spawning nodes on/off
        {
            if (GUILayout.Button("Nodes"))
            {
                myScript.spawningNodes = false;
            }
        }
        else
        {
            if (GUILayout.Button("Nodes"))
            {
                myScript.spawningNodes = true;
            }
        }
       

       
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
        if (Event.current.type == EventType.MouseDown)
        {
            if (myScript.spawningNodes)  PrepareNodeSpawn();
        }
        if (myScript.spawningNodes) Selection.activeGameObject = myScript.gameObject;
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
            Pathing myScript = (Pathing)target;
            myScript.PrintStuff(hit.collider.name);
        }
            
        
        return hit.point;
    }




}
