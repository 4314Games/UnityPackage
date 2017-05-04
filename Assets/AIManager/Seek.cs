using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Seek : MonoBehaviour
{
    public bool useAStarAlgorithm = false;
    public GameObject objectToSeekTo;
    [HideInInspector]
    public bool toSeek = false;
    private NavMeshAgent agent;
    public List<GameObject> nodes;
    public GameObject treeOfNodes;
    public bool isUsingNodes = false;
    public bool clearNodesOnTreeAdd = false;
    private int nodeAt = 0;
    private bool isFinished = false;

    public bool updateRouteEveryFrame = true;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (objectToSeekTo == null)
        {
            Debug.Log("There is no object to seek to...");
        }
        if (useAStarAlgorithm && isUsingNodes)
        {
            agent.enabled = false;
            gameObject.AddComponent<Unit>();
            //GetComponent<Unit>().target = nodes[0].transform;
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
    // Update is called once per frame
    void Update()
    {
        if (toSeek && !useAStarAlgorithm)
        {
            agent.destination = objectToSeekTo.transform.position;
        }
        if (isUsingNodes & useAStarAlgorithm && !isFinished)
        {
            print("Node at: " + nodeAt);
            if (nodeAt >= nodes.Count)
                isFinished = true;
            if (nodeAt == 0)
            {
                GetComponent<Unit>().target = nodes[0].transform;
                PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[0].transform.position,
                GetComponent<Unit>().OnPathFound);
                nodeAt++;
            }
            float distance = Vector3.Distance(GetComponent<Unit>().transform.position, nodes[nodeAt-1].transform.position);
            print("Distance Between Nodes: " + distance);
            if (distance <= 2.0f || updateRouteEveryFrame)
            {
                GetComponent<Unit>().targetIndex = 0;
                if (!updateRouteEveryFrame || distance <= 2.0f)
                {             
                    PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAt].transform.position,
                    GetComponent<Unit>().OnPathFound);
                    print("Requesting New Path...");
                    nodeAt++;
                }
                else if (updateRouteEveryFrame)
                {
                    PathRequestManager.RequestPath(GetComponent<Unit>().transform.position, nodes[nodeAt-1].transform.position,
                    GetComponent<Unit>().OnPathFound);
                }

            }


        }

    }
}
