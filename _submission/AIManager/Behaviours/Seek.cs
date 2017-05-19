using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Seek : MonoBehaviour
{
    public bool toSeek = false;//To seek
    public bool useAStarAlgorithm = false;//Using a star
    public GameObject objectToSeekTo;//Object to seek to
    public List<GameObject> nodes = new List<GameObject>();//list of nodes
    public GameObject treeOfNodes;//parent node object
    public bool isUsingNodes = false;//We using nodes?
    public bool clearNodesOnTreeAdd = false;//Clear the list on add of nodes
    private int nodeAt = 0;//Node we are currently at
    private bool isFinished = false;//finished seeking?
    public bool updateRouteEveryFrame = false;//update the route every frame? (Very expensive but accurate)
    // Use this for initialization
    void Start()
    {
        if (objectToSeekTo == null)
        {
            Debug.Log("There is no object to seek to...");
        }//if theres no object to goto
        if (useAStarAlgorithm && isUsingNodes)
        {
            if (GetComponent<NavMeshAgent>() != null)
                GetComponent<NavMeshAgent>().enabled = false;
        }//disable the navmesh if there is  one
        GetComponent<Unit>().GotoPath(objectToSeekTo.transform.position);//goto the location setup (objecttoseekto)
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
        nodes.Clear();
    }//clear the nodes list

    void Update()
    {
        if (toSeek && !useAStarAlgorithm)
        {
            if (GetComponent<NavMeshAgent>() != null)
                GetComponent<NavMeshAgent>().destination = objectToSeekTo.transform.position;
        }//Not using a star then get the navmesh and goto the object to seek to
        if (toSeek && isUsingNodes && useAStarAlgorithm && !isFinished)
        {
            if (nodeAt >= nodes.Count)
                isFinished = true;//Finished is true when at last node
            if (nodeAt == 0)
            {
                GetComponent<Unit>().GotoPath(nodes[0].transform.position);
                nodeAt++;
            }//On the first node goto it
            float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAt - 1].transform.position);//distance betwen self and node
            if (distance <= 2.0f || updateRouteEveryFrame)
            {
                if (!updateRouteEveryFrame && distance <= 2.0f)
                {
                    GetComponent<Unit>().GotoPath(nodes[nodeAt].transform.position);
                    nodeAt++;//Goto node along a path then increase nodeAt
                }//NOT Updating every frame
                else if (updateRouteEveryFrame)
                {
                    GetComponent<Unit>().GotoPath(nodes[nodeAt].transform.position);//Calculate path & position everyframe
                }
            }
        }

    }
}
