using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour {

    public GameObject nodedd;
    public GameObject nodeaa;

    void Update()
    {
        FindPath(nodeaa, nodedd);
        
    }

	void FindPath(GameObject p_ANode, GameObject p_BNode)
    {
        print("Finding");
        Node startNode = p_ANode.GetComponent<Node>();
        Node endNode = p_BNode.GetComponent<Node>();

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode == endNode) //If we have found end node, end loop.
            {
                RetracePath(p_ANode, p_BNode);
                print("END");
                return;  
            }
            foreach(GameObject linkedNodes in currentNode.connectedNodes)
            {
                Node neighbour = linkedNodes.GetComponent<Node>();
                if (closedSet.Contains(linkedNodes.GetComponent<Node>())) continue; //if already checked, skip node.

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode.gameObject, linkedNodes);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(linkedNodes, endNode.gameObject);
                    neighbour.parent = currentNode.gameObject;
                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(GameObject startNode, GameObject endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode.GetComponent<Node>();
       
        while (currentNode.id != startNode.GetComponent<Node>().id)
        {
            path.Add(currentNode);
            currentNode = currentNode.GetComponent<Node>().parent.GetComponent<Node>();
        }
        path.Reverse();
        for(int x = 0; x < path.Count; x++)
        {
            print("Node : " + (path[x].id + 1) + " is " + x);
        }
        print("YAh");
    }

    int GetDistance(GameObject nodeA, GameObject nodeB)
    {
        return (int)Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
    }

}
