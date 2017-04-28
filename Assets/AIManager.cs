using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIManager : MonoBehaviour {
    public bool toWander = false;
    public bool toSeek = false;
    public bool toPatrol = false;
    public bool toFlee = false;

    
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
            GetComponent<Wander>().toWander = true;
        }
        if (toSeek)
        {
            toWander = false;
            toPatrol = false;
            toFlee = false;
            GetComponent<Seek>().toSeek = true;
        }
        if (toPatrol)
        {
            toWander = false;
            toSeek = false;
            toFlee = false;
            GetComponent<Patrol>().toPatrol = true;
        }
        if (toFlee)
        {
            toWander = false;
            toSeek = false;
            toPatrol = false;
        }
	}
}
