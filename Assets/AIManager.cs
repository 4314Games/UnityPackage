using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIManager : MonoBehaviour {
    public bool toWander = false;
    public bool toSeek = false;
    public bool toPatrol = false;
    public bool toFlee = false;
    [HideInInspector] public bool seekAdded = false;
    [HideInInspector] public bool wanderAdded = false;
    [HideInInspector] public bool patrolAdded = false;
    [HideInInspector] public bool fleeAdded = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public void UpdateAIFunction () {
        if (toWander)
        {
            toSeek = false;
            toPatrol = false;
            toFlee = false;
            if (GetComponent<Wander>() != null)
                GetComponent<Wander>().toWander = true;
        }
        if (toSeek)
        {
            toWander = false;
            toPatrol = false;
            toFlee = false;
            if (GetComponent<Seek>() != null)
                GetComponent<Seek>().toSeek = true;
        }
        if (toPatrol)
        {
            toWander = false;
            toSeek = false;
            toFlee = false;
            if (GetComponent<Patrol>() != null)
                GetComponent<Patrol>().toPatrol = true;
        }
        if (toFlee)
        {
            toWander = false;
            toSeek = false;
            toPatrol = false;
            if (GetComponent<Flee>() != null)
                GetComponent<Flee>().toFlee = true;
        }
        if (GetComponent<Seek>() != null)
            GetComponent<Seek>().toSeek = toSeek;
        if (GetComponent<Patrol>() != null)
            GetComponent<Patrol>().toPatrol = toPatrol;
        if (GetComponent<Wander>() != null)
            GetComponent<Wander>().toWander = toWander;
        if (GetComponent<Flee>() != null)
            GetComponent<Flee>().toFlee = toFlee;
    }
}
