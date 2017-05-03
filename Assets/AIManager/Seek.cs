using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Seek : MonoBehaviour {
    public bool useAStarAlgorithm = false;
    public GameObject objectToSeekTo;
    [HideInInspector] public bool toSeek = false;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        if (objectToSeekTo == null)
        {
            Debug.Log("There is no object to seek to...");
        }
        if (useAStarAlgorithm)
        {
            agent.enabled = false;
            gameObject.AddComponent<Unit>();
            GetComponent<Unit>().target = objectToSeekTo.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (toSeek && !useAStarAlgorithm)
        {
            agent.destination = objectToSeekTo.transform.position;
        }

	}
}
