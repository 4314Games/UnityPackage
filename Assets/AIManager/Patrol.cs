using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Kyle Norton 2017
public class Patrol : MonoBehaviour {
    public List<GameObject> nodes;

    private NavMeshAgent agent;
    [HideInInspector] public bool toPatrol = false;
    private bool isPatrolling = false;
    [SerializeField]private int nodeAt = 0;
    public bool gotoStart = false;
    private bool reachedEnd = false;
    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        if (isPatrolling && nodes.Count == 0)
        {
            Debug.Log("There are no nodes to move to...");
        }
        if (isPatrolling && nodes.Count == 1)
        {
            nodes.Add(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (toPatrol)
            {
                if (agent.remainingDistance < 0.5f)
                {
                    PatrolTo();
                }
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
