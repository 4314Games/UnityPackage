using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour {
    [SerializeField] private GameObject nodeMovingTo;
    public List<GameObject> nodes;
    public float distanceToNextWander = 0.5f;
    [HideInInspector] public bool toWander = false;
    private NavMeshAgent agent;


    public GameObject treeOfNodes;
    public bool clearNodesOnTreeAdd = false;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
       
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.remainingDistance < distanceToNextWander  && toWander)
        {
            WanderTo();
        }
	}
    public void WanderTo()
    {
        int randomPosition = Random.Range(0, nodes.Capacity);
        agent.destination = nodes[randomPosition].transform.position;
        nodeMovingTo = nodes[randomPosition];
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
