using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISystem : MonoBehaviour {
    public GameObject objectToAffect;
    public List<GameObject> nodes;

    private NavMeshAgent agent;
    public bool toPatrol = false;
    private bool isPatrolling = false;
    private int nodeAt = 0;
    public bool gotoStart = false;
    private bool reachedEnd = false;
    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (toPatrol)
        {
            if (agent.remainingDistance < 0.5f)
            {
                Patrol();
            }
        }
	}
    public void Patrol()
    {
       
        if (nodeAt > nodes.Capacity - 1)
        { 
            reachedEnd = true;
            if (gotoStart)
            {
                nodeAt = 0;
            }
            else
            {
                nodeAt = nodes.Capacity - 1;
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
    public void Wander()
    {

    }
    public void Seek()
    {

    }
    public void Flee()
    {

    }
}
