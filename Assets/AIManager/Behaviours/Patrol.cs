using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Kyle Norton 2017
public class Patrol : MonoBehaviour
{
    public bool toPatrol = false; //To patrol -Editor?
    public List<GameObject> nodes;//Nodes list
    public GameObject treeOfNodes;//Nodes parent gameobject
    public bool clearNodesOnTreeAdd = false;//Clear the list on addition of nodes?
    public bool isPatrolling = false;// Currently Patrolling?
    public float distanceToNextPatrol = 2.0f;//Distance between node and unit 
    private int nodeAt = 0;//Current node at -- NonAstar
    private int nodeAtAStar = 0;//Current node at - A Star
    public bool gotoStart = false;//gotostart when the end is reached?
    private bool reachedEnd = false;//has the end been reached
    public bool useAStar = false;//use a star?
    public bool traverseForwards = true;//go through the list forwards?
    private bool firstNode = true;//going to first node?
    // Use this for initialization
    void Start()
    {
        if (isPatrolling && nodes.Count == 0)
        {
            Debug.Log("There are no nodes to move to...");
        }//If there are no nodes
        if (isPatrolling && nodes.Count == 1)
        {
            nodes.Add(gameObject);
        }//If there is one node add self to list to patrol between its starting location and the node
        if (useAStar)
            if (GetComponent<NavMeshAgent>() != null)
                GetComponent<NavMeshAgent>().enabled = false;//Disable the navmesh if using a star
    }
    void Update()
    {
        if (toPatrol && useAStar && isPatrolling)
        {
            if (firstNode)
            {
                GetComponent<Unit>().GotoPath(nodes[0].transform.position);
                firstNode = false;
            }
            PatrolToAStar();
        }//We are using a star

    }
   
    public void InsertTreeOfNodes()
    {
        if (treeOfNodes != null && clearNodesOnTreeAdd)
        {
            nodes.Clear();
        }
        for (int i = 0; i < treeOfNodes.transform.childCount; i++)
        {
            nodes.Add(treeOfNodes.transform.GetChild(i).gameObject);
        }
        //Add nodes to the list from the parent game object
    }
    public void ClearTreeOfNodes()
    {
        nodes.Clear();//clear the list
    }
    public void PatrolToAStar()
    {
        float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position);//Distance between self and node at
        if (traverseForwards && distance <= distanceToNextPatrol)
        {
            nodeAtAStar++;
            if (nodeAtAStar >= nodes.Count)
            {
                traverseForwards = false;
                nodeAtAStar = nodes.Count - 1;
                distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position);
            }//Reached the last node start going back

            GetComponent<Unit>().GotoPath(nodes[nodeAtAStar].transform.position);
            print("Traversing to - ..." + nodeAtAStar);
            //Follow a path at the nodes location
        }
        else if (!traverseForwards && distance <= distanceToNextPatrol)//going backwards
        {
            if (!gotoStart)
            {
                nodeAtAStar--;
                if (nodeAtAStar <= 0)
                {
                    traverseForwards = true;
                    nodeAtAStar = 0;
                }//goto the start
                GetComponent<Unit>().GotoPath(nodes[nodeAtAStar].transform.position);
            }//not going to the start follow a path at nodes location
            else
            {
                traverseForwards = true;
                nodeAtAStar = 0;
                GetComponent<Unit>().GotoPath(nodes[nodeAtAStar].transform.position);
            }//go back to the start
        }
    }

}
