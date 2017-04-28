using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {
    public GameObject objectToAffect;
    public List<GameObject> nodes;

    private NavMeshAgent agent;
    public bool toPatrol = false;
    private bool isPatrolling = false;
    [SerializeField]private int nodeAt = 0;
    public bool gotoStart = false;
    private bool reachedEnd = false;
    // Use this for initialization
    void Start()
    {
        agent = objectToAffect.GetComponent<NavMeshAgent>();
        if (objectToAffect == null)
        {
            Debug.Log("There is no object to Affect...");
        }
        if (nodes.Count == 0)
        {
            Debug.Log("There are no nodes to move to...");
        }
        if (nodes.Count == 1)
        {
            nodes.Add(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toPatrol)
        {
            if (agent.remainingDistance < 0.5f)
            {
                PatrolTo();
            }
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
            //or go bac kthrough;
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
}
