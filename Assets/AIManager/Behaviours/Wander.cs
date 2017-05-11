using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public bool toWander = false;
    public bool useAStar = false;
    private bool firstTraversal = true;
    public List<GameObject> nodes;
    public float distanceToNextWander = 0.5f;
    private NavMeshAgent agent;


    public GameObject treeOfNodes;
    public bool clearNodesOnTreeAdd = false;

    int nodeTraversingTo = 0;
    int randomPosition;
    // Use this for initialization
    void Start()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (useAStar)
        {
            if (GetComponent<Unit>() == null)
                gameObject.AddComponent<Unit>();
            if (GetComponent<NavMeshAgent>() != null)
            {
                agent.enabled = false;
            }
            randomPosition = Random.Range(0, nodes.Count);
            nodeTraversingTo = randomPosition;
            GetComponent<Unit>().target = nodes[randomPosition].transform;
        }
       
    }
    void Awake()
    {
        InsertTreeOfNodes();
    }
    // Update is called once per frame
    void Update()
    {
        if (agent != null && agent.enabled == true)
        {
            if (agent.remainingDistance < distanceToNextWander && toWander)
            {
                WanderTo();
            }
        }

        else if (useAStar && toWander)
        {
            if (GetComponent<Unit>() != null)
                AStarWander();
            else
            {
                print("There is no Unit attached");
                
            }

        }
    }
    public void WanderTo()
    {
        randomPosition = Random.Range(0, nodes.Capacity);
        agent.destination = nodes[randomPosition].transform.position;
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
    }
    public void ClearTreeOfNodes()
    {
        nodes.Clear();
    }
    public void AStarWander()
    {
        if (firstTraversal)
        {
            firstTraversal = !firstTraversal;
            PathRequestManager.RequestPath(GetComponent<Unit>().transform.position,
            nodes[randomPosition].transform.position, GetComponent<Unit>().OnPathFound);
        }
        randomPosition = Random.Range(0, nodes.Count);
        Vector3 destination = nodes[randomPosition].transform.position;
        float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeTraversingTo].transform.position);
        //print("Distance Between Nodes: " + distance);
        //Get a random point within the node range and set a temp destination.
        if (distance <= 2.0f)
        {
            nodeTraversingTo = randomPosition;
            GetComponent<Unit>().targetIndex = 0;
            GetComponent<Unit>().target = nodes[randomPosition].transform;
            PathRequestManager.RequestPath(GetComponent<Unit>().transform.position,
            destination, GetComponent<Unit>().OnPathFound);//Request a new path to the temp position
                                                           //from our position
        }
    }
}
