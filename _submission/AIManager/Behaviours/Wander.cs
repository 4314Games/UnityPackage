using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public bool toWander = false;//To wandeR?
    public bool useAStar = false;//Use A Star?
    public List<GameObject> nodes;//nodes to use
    public float distanceToNextWander = 2.0f;//distance between self and node to goto next node
    public GameObject treeOfNodes;//parent object of nodes
    public bool clearNodesOnTreeAdd = false;//clear list on add of nodes
    private int nodeTraversingTo = 0;//Node that we are going to
    private int randomPosition;//Random node position
    // Use this for initialization
    void Start()
    {
        if (useAStar)
        {
            if (GetComponent<NavMeshAgent>() != null)
                GetComponent<NavMeshAgent>().enabled = false;//Disable navmesh if there is one
            GetComponent<Unit>().GotoPath(nodes[nodeTraversingTo].transform.position);//Goto the first node to start of with
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NavMeshAgent>() != null && GetComponent<NavMeshAgent>().enabled == true)
        {
            if (GetComponent<NavMeshAgent>().remainingDistance < distanceToNextWander && toWander)
            {
                WanderTo();
            }//Wander to a location if we have a navmesh and the remaining distance is low enough
        }

        else if (useAStar && toWander)
            AStarWander();//A star wander if enabled and ready
    }
    public void WanderTo()
    {
        randomPosition = Random.Range(0, nodes.Capacity);
        GetComponent<NavMeshAgent>().destination = nodes[randomPosition].transform.position;
    }//Agent will navigate to a random node
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
    public void AStarWander()
    {
        randomPosition = Random.Range(0, nodes.Count);
        Vector3 destination = nodes[randomPosition].transform.position;
        float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeTraversingTo].transform.position);
        //Get a random point within the node range and set a temp destination.
        if (distance <= distanceToNextWander)
        {
            nodeTraversingTo = randomPosition;
            GetComponent<Unit>().GotoPath(destination);//Request a new path to the temp position from our position
        }//if in distance goto a path
    }
}
