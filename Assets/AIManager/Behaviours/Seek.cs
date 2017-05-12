using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Seek : MonoBehaviour
{
    public bool toSeek = false;
    public bool useAStarAlgorithm = false;
    public GameObject objectToSeekTo;
    private NavMeshAgent agent;
    public List<GameObject> nodes = new List<GameObject>();
    public GameObject treeOfNodes;
    public bool isUsingNodes = false;
    public bool clearNodesOnTreeAdd = false;
    public int nodeAt = 0;
    private bool isFinished = false;
    public bool updateRouteEveryFrame = true;
    // Use this for initialization
    void Start()
    {
        if (GetComponent<NavMeshAgent>() != null)
            agent = GetComponent<NavMeshAgent>();
        if (objectToSeekTo == null)
        {
            Debug.Log("There is no object to seek to...");
        }
        if (useAStarAlgorithm && isUsingNodes)
        {
            if (GetComponent<NavMeshAgent>() != null)
                agent.enabled = false;
        }

    }
    void Awake()
    {
        if (isUsingNodes)
            InsertTreeOfNodes();
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
    // Update is called once per frame
    void Update()
    {
        if (toSeek && !useAStarAlgorithm)
        {
            if (GetComponent<NavMeshAgent>() != null)
                agent.destination = objectToSeekTo.transform.position;
        }
        if (toSeek && isUsingNodes && useAStarAlgorithm && !isFinished)
        {
            if (nodeAt >= nodes.Count)
                isFinished = true;
            if (nodeAt == 0)
            {
                PathRequestManager.ClearPath();
                GetComponent<Unit>().target = nodes[0].transform;
                PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[0].transform.position,
                GetComponent<Unit>().OnPathFound);
                nodeAt++;
            }
            float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAt - 1].transform.position);
            if (distance <= 2.0f || updateRouteEveryFrame)
            {
                if (!updateRouteEveryFrame && distance <= 2.0f)
                {
                    PathRequestManager.ClearPath();
                    GetComponent<Unit>().targetIndex = 0;
                    GetComponent<Unit>().target = nodes[nodeAt].transform;
                    PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAt].transform.position,
                    GetComponent<Unit>().OnPathFound);
                    nodeAt++;
                }
                else if (updateRouteEveryFrame)
                {
                    PathRequestManager.ClearPath();
                    PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAt - 1].transform.position,
                    GetComponent<Unit>().OnPathFound);
                }
            }
        }
        else if (!isUsingNodes && useAStarAlgorithm && !isFinished && toSeek)
        {
            PathRequestManager.ClearPath();
            GetComponent<Unit>().target = objectToSeekTo.transform;
            PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, objectToSeekTo.transform.position,
            GetComponent<Unit>().OnPathFound);
        }

    }
}
