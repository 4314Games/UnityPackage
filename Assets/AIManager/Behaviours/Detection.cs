using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Kyle Norton 2017

public class Detection : MonoBehaviour
{
    public bool toDetect = false;
    public bool toChase = false;
    public GameObject objectToDetect;
    public float radius = 10.0f;
    public string behaviour = "Seek";
    //private NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
    }
    void Awake()
    {
        GetComponent<Seek>().objectToSeekTo = objectToDetect;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, objectToDetect.transform.position);
        if (distance <= radius && toDetect && toChase)
        {
            switch (behaviour)
            {
                case "Seek":
                    if (GetComponent<Seek>() != null)
                    {
                        GetComponent<Seek>().toSeek = true;
                        GetComponent<Seek>().objectToSeekTo = objectToDetect;
                        
                    }
                    else
                        print("There is no Seek Component Attatched...");
                    break;
                default:
                    break;
            }
        }
        else if(distance > radius && toDetect && toChase)
        {
            switch (behaviour)
            {
                case "Seek":
                    if (GetComponent<Seek>() != null)
                    {
                        GetComponent<Seek>().toSeek = false;
                        //print("Cannot See " + objectToDetect.ToString() + " - " + distance + " units away.");
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
                behaviour = "Seek";
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
