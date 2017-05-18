using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Kyle Norton 2017

public class Detection : MonoBehaviour
{
    public bool toDetect = false;//Wether to detect or not
    public bool toChase = false;//Wether to chase the object
    public GameObject objectToDetect;//the object to detect
    public float radius = 10.0f;//detection radius
    public string behaviour = "Seek";//behaviour to peform on detection

    void Awake()
    {
        if(behaviour == "SeeK")
            GetComponent<Seek>().objectToSeekTo = objectToDetect;//Seek to the object to detect
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, objectToDetect.transform.position);
        if (distance <= radius && toDetect && toChase)//If object is being seen do a certain behaviour
        {
            switch (behaviour)
            {
                case "Seek":
                    if (GetComponent<Seek>() != null)
                    {
                        GetComponent<Seek>().toSeek = true;
                        GetComponent<Seek>().objectToSeekTo = objectToDetect;         //Start seeking            
                    }
                    else
                        print("There is no Seek Component Attatched...");
                    break;
                default:
                    break;
            }
        }
        else if(distance > radius && toDetect && toChase)//If out of range
        {
            switch (behaviour)
            {
                case "Seek":
                    if (GetComponent<Seek>() != null)
                    {
                        GetComponent<Seek>().toSeek = false;//Stop seeking
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
        }//Switch the beahviour for use witin editor script
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);//Draw sphere to show radius
    }
}
