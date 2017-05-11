using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Kyle Norton 2017
public class Patrol : MonoBehaviour
{
    public bool toPatrol = false;
    public List<GameObject> nodes;
    public GameObject treeOfNodes;
    public bool clearNodesOnTreeAdd = false;
    private NavMeshAgent agent;
    private bool isPatrolling = false;  
    private int nodeAt = 0;
    private int nodeAtAStar = 0;
    public bool gotoStart = false;
    private bool reachedEnd = false;
    public bool useAStar = false;
    private bool traverseForwards = true;
    private bool firstNode = true;
    // Use this for initialization
    void Start()
    {
        if (agent != null)
            agent = GetComponent<NavMeshAgent>();
        if (isPatrolling && nodes.Count == 0)
        {
            Debug.Log("There are no nodes to move to...");
        }
        if (isPatrolling && nodes.Count == 1)
        {
            nodes.Add(this.gameObject);
        }
        if (useAStar)
            if (GetComponent<NavMeshAgent>() != null)
                GetComponent<NavMeshAgent>().enabled = false;
    }
    void Awake()
    {
        InsertTreeOfNodes();
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            if (toPatrol && !useAStar && agent != null)
            {
                if (agent.remainingDistance < 0.5f)
                {
                    PatrolTo();
                }
            }
            else if (toPatrol && useAStar)
            {
                if (firstNode)
                {
                    GetComponent<Unit>().targetIndex = 0;
                    GetComponent<Unit>().target = nodes[0].transform;
                    PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[0].transform.position,
                    GetComponent<Unit>().OnPathFound);
                    firstNode = false;
                }

                PatrolToAStar();
            }
        }
        catch (System.ArgumentOutOfRangeException)
        {
            throw;
        }

    }
    public void PatrolTo()
    {

        if (nodeAt > nodes.Count - 1)
        {
            reachedEnd = true;
            if (gotoStart)
            {
                nodeAt = 0;
            }
            else
            {
                nodeAt = nodes.Count - 1;
            }
            //or go back through;
        }
        if (nodeAt < 0)
        {
            reachedEnd = false;
            nodeAt = 0;
        }
        if (!isPatrolling)
        {
            agent.destination = nodes[nodeAt].transform.position;
            if (reachedEnd && !gotoStart)
            {
                nodeAt--;
            }
            else
            {
                nodeAt++;
            }
            isPatrolling = true;
        }
        else
        {
            agent.destination = nodes[nodeAt].transform.position;
            if (reachedEnd && !gotoStart)
            {
                nodeAt--;
            }
            else
            {
                nodeAt++;
            }
            isPatrolling = false;

        }

    }

    public void PatrolToAStar()
    {
        float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position);
        if (traverseForwards && distance <= 2.0f)
        {
            nodeAtAStar++;
            if (nodeAtAStar >= nodes.Count)
            {
                traverseForwards = false;
                nodeAtAStar = nodes.Count - 1;
                distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position);
            }
            GetComponent<Unit>().targetIndex = 0;
            GetComponent<Unit>().target = nodes[nodeAtAStar].transform;
            PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position,
            GetComponent<Unit>().OnPathFound);

        }
        else if (!traverseForwards && distance <= 2.0f)
        {
            if (!gotoStart)
            {
                nodeAtAStar--;
                if (nodeAtAStar <= 0)
                {
                    traverseForwards = true;
                    nodeAtAStar = 0;
                }
                GetComponent<Unit>().targetIndex = 0;
                GetComponent<Unit>().target = nodes[nodeAtAStar].transform;
                PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position,
                GetComponent<Unit>().OnPathFound);
            }
            else
            {
                traverseForwards = true;
                nodeAtAStar = 0;
                GetComponent<Unit>().targetIndex = 0;
                GetComponent<Unit>().target = nodes[nodeAtAStar].transform;
                PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAtAStar].transform.position,
                GetComponent<Unit>().OnPathFound);
            }


        }

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
}
