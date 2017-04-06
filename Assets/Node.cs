using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour {

    public Pathing pathingScript;
    int id;
    Vector3 position;
    public List<GameObject> connectedNodes = new List<GameObject>();

    void Start()
    {
        pathingScript = GameObject.Find("AI/Pathing Prefab").GetComponent<Pathing>();
    }

    public void Construct(int p_id, Vector3 p_position) //Constructs Node
    {
        id = p_id;
        position = p_position;
    }

    public void AddConnectedNode(GameObject p_node) //Adds node to this node
    {
        if (!connectedNodes.Contains(p_node))
        {
            connectedNodes.Add(p_node);
            print("Nodes Linked");
        }
        else print("Nodes Already Linked");
    }

    public void StartConnectingNode()   //Allws node to be connect to this node
    {
        pathingScript.LinkNodes(id);
    }

    public int GetId() { return id; }   //Returns node ID

    void OnDestroy()    //Destroy node and clear from city
    {
        pathingScript.RemoveNode(gameObject);
    }

    void OnDrawGizmos()
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

    public void RemoveConnectedNode(GameObject p_node)  //Remove deleted Node
    {
        if(connectedNodes.Contains(p_node)) connectedNodes.Remove(p_node);
    }

}
