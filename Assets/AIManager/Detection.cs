using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
    public GameObject objectToDetect;
    public float radius = 10.0f;
    public bool toChase = false;
    public bool toDetect = false;
    public int behaviourIndex = -1;
    //private NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, objectToDetect.transform.position);
        if (distance <= radius)
        {
            switch (behaviourIndex)
            {
                case 0:
                    if (GetComponent<Seek>() != null)
                    {
                        GetComponent<Seek>().StartSeeking();
                        GetComponent<Seek>().objectToSeekTo = objectToDetect;
                        
                    }
                    else
                        print("There is no Seek Component Attatched...");
                    break;
                default:
                    break;
            }
        }
        else if(distance > radius)
        {
            switch (behaviourIndex)
            {
                case 0:
                    if (GetComponent<Seek>() != null)
                    {
                        GetComponent<Seek>().StopSeeking();
                        print("Cannot See " + objectToDetect.ToString() + " - " + distance + " units away.");
                    }
                    else
                        print("There is no Seek Component Attatched...");
                    break;
                default:
                    break;
            }
        }
    }
    public void OnBehaviour(int index)
    {
        switch (index)
        {
            case 0://Seek
                behaviourIndex = 0;
                break;
            default:
                break;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);//Draw sphere to show radius
    }
}
