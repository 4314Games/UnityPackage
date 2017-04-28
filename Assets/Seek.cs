using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Seek : MonoBehaviour {
    public GameObject objectToSeekTo;
    public bool toSeek = false;

    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (toSeek)
        {
            agent.destination = objectToSeekTo.transform.position;
        }
	}
}
