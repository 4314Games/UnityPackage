using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;


[ExecuteInEditMode]
public class Node : MonoBehaviour {

    public Pathing pathingScript;
    public int id;
    public List<GameObject> connectedNodes = new List<GameObject>();
    public bool isLinking;
    //PathFinding
    public int gCost;
    public int hCost;
    public GameObject parent;
    public GameObject platformObject;
    public bool isPlatform;
   

    void Start()
    {
        pathingScript = GameObject.Find("AI_Pathing Prefab").GetComponent<Pathing>();
    }

    public void Construct(int p_id) //Constructs Node
    {
        id = p_id;
        gameObject.name = "Node" + id;
    }

    void Update()
    {
        if (isPlatform)
        {
            transform.position =  new Vector3(platformObject.transform.position.x, platformObject.transform.position.y + pathingScript.heightOffGround, platformObject.transform.position.z);
        }
    }

    public void StartConnectingNode()   //Allows node to be connect to this node
    {
        if (!pathingScript.is1stNode && pathingScript.isLinking)
        {
            pathingScript.nodeLinking = gameObject;
            pathingScript.is1stNode = true;
            return;
        }
        else if(pathingScript.is1stNode && pathingScript.isLinking)
        {
            //If linking enabled, node isnt already connected and it isnt the linking node, add this node.
            if (pathingScript.isLinking && !pathingScript.nodeLinking.GetComponent<Node>().connectedNodes.Contains(gameObject) && pathingScript.nodeLinking != gameObject)
            {
                pathingScript.nodeLinking.GetComponent<Node>().connectedNodes.Add(gameObject);
                Debug.Log("Nodes Linked");
            }
            else if (pathingScript.nodeLinking.GetComponent<Node>().connectedNodes.Contains(gameObject))
            {
                print("Node Already Linked");
            }
            else if (pathingScript.nodeLinking != gameObject && pathingScript.isLinking)
            {
                print("Cant Link This Node");
            }
            pathingScript.nodeLinking = pathingScript.tempNode;
            pathingScript.is1stNode = false;
        }
    }

    public int GetId() { return id; }   //Returns node ID

    void OnDestroy()    //Destroy node and clear from city
    {
        if (isLinking) pathingScript.isLinking = false;
        pathingScript.RemoveNode(gameObject);
    }

    public void Destroyer()
    {
        pathingScript.RemoveNode(gameObject);
        DestroyImmediate(gameObject);
    }

    public void OnDrawGizmos()
    {
        if (connectedNodes.Count != 0)
        {
            for (int x = 0; x < connectedNodes.Count; x++)  //Draw connections between nodes
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, connectedNodes[x].transform.position);
                Vector3 dir = transform.position - connectedNodes[x].transform.position;
                float scaleFactorCube = 0.15f;
                float scaleFactorSphere = 0.225f;
                Gizmos.DrawCube((transform.position - (dir.normalized * ((dir.magnitude / 2) + scaleFactorSphere))), new Vector3(scaleFactorCube, scaleFactorCube, scaleFactorCube));
                Gizmos.color = Color.white;
                Gizmos.DrawSphere((transform.position - (dir.normalized * (dir.magnitude / 2))), scaleFactorSphere);
            }
        }
    }

    public void RemoveConnectedNode(GameObject p_node)  //Remove deleted Node
    {
        if(connectedNodes.Contains(p_node)) connectedNodes.Remove(p_node);
    }

    public void LinkerOn()
    {
        pathingScript.isLinking = true;
        pathingScript.nodeLinking = gameObject;
        //gameObject.
        //TODO : CHANGE ICON
    }

    public void LinkerOff()
    {
        pathingScript.isLinking = false;
        //TODO: CHANGE ICON
    }

    public bool GetPathLinking ()   //Gets Pathing sscript is linking
    {
        return pathingScript.isLinking;
    }

    public void PrintStuff(string p_print)
    {
        print(p_print);
    }   //Print Stuff

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }       //Pathfinding Cost

    
   
}
