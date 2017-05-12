using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Kyle Norton 2017
[RequireComponent(typeof(Unit))]
public class AIManager : MonoBehaviour
{
    [HideInInspector]
    public int index = 0;
    [HideInInspector]
    public bool toWander = false;
    [HideInInspector]
    public bool toSeek = false;
    [HideInInspector]
    public bool toPatrol = false;
    [HideInInspector]
    public bool toFlee = false;
    [HideInInspector]
    public bool toDetect = false;
    [HideInInspector]
    public bool seekAdded = false;
    [HideInInspector]
    public bool wanderAdded = false;
    [HideInInspector]
    public bool patrolAdded = false;
    [HideInInspector]
    public bool fleeAdded = false;
    [HideInInspector]
    public bool detectionAdded = false;
    [HideInInspector]
    public string errorMessage;
    [HideInInspector]
    public string typeOfErrorMessage;

    // Update is called once per frame
    public void UpdateAIFunction()
    {
        if (toWander)
        {
            toSeek = false;
            toPatrol = false;
            toFlee = false;
            toDetect = false;
            if (GetComponent<Wander>() != null)
                GetComponent<Wander>().toWander = true;
        }
        if (toSeek)
        {
            toWander = false;
            toPatrol = false;
            toFlee = false;
            toDetect = false;
            if (GetComponent<Seek>() != null)
                GetComponent<Seek>().toSeek = true;
        }
        if (toPatrol)
        {
            toWander = false;
            toSeek = false;
            toFlee = false;
            toDetect = false;
            if (GetComponent<Patrol>() != null)
                GetComponent<Patrol>().toPatrol = true;
        }
        if (toFlee)
        {
            toWander = false;
            toSeek = false;
            toPatrol = false;
            toDetect = false;
            if (GetComponent<Flee>() != null)
                GetComponent<Flee>().toFlee = true;
        }
        if (toDetect)
        {
            toWander = false;
            toSeek = false;
            toPatrol = false;
            toFlee = false;
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
        if (GetComponent<Detection>() != null)
            GetComponent<Detection>().toDetect = toDetect;
    }
    public void Awake()
    {
        //CheckScripts();
        //UpdateAIFunction();
    }
    public bool CheckScripts()//Check if each script has every component needed to work.
    {
        if (GetComponent<Seek>() != null)
        {
            typeOfErrorMessage = "Seek Component";
            if (!GetComponent<Seek>().isUsingNodes &&
               GetComponent<Seek>().objectToSeekTo == null ||
               GetComponent<Seek>().isUsingNodes
               && GetComponent<Seek>().nodes.Count == 0)
            {
                errorMessage = "There is no object to Seek to, Please assign one.\n" + GetComponent<Seek>().ToString();
                if (GetComponent<Seek>().isUsingNodes)
                {
                    errorMessage = "There are no nodes to Seek to, Please assign atleast one.\n" + GetComponent<Seek>().ToString();
                    int counter = 0;
                    foreach (GameObject node in GetComponent<Seek>().nodes)
                    {
                        if (node == null)
                        {
                            errorMessage = "Node (" + counter + ")" + " is not valid, Check if there is a valid gameobject.\n" + GetComponent<Seek>().ToString();
                            return false;
                        }
                        counter++;
                    }
                }
                return false;
            }
        }
        if (GetComponent<Wander>() != null)
        {
            typeOfErrorMessage = "Wander Component";
            if (GetComponent<Wander>().nodes.Count == 0)
            {
                errorMessage = "There is are no nodes to Wander, Please assign atleast one.\n" + GetComponent<Wander>().ToString();
                return false;
            }
            int counter = 0;
            foreach (GameObject node in GetComponent<Wander>().nodes)
            {
                if (node == null)
                {
                    errorMessage = "Node (" + counter + ")" + " is not valid, Check if there is a valid gameobject.\n" + GetComponent<Wander>().ToString();
                    return false;
                }
                counter++;
            }
        }
        if (GetComponent<Patrol>() != null)
        {
            typeOfErrorMessage = "Patrol Component";
            if (GetComponent<Patrol>().nodes.Count == 0)
            {
                errorMessage = "There is are no nodes to Patrol, Please assign atleast one.\n" + GetComponent<Patrol>().ToString();
                return false;
            }
            int counter = 0;
            foreach (GameObject node in GetComponent<Patrol>().nodes)
            {
                if (node == null)
                {
                    errorMessage = "Node (" + counter + ")" + " is not valid, Check if there is a valid gameobject.\n" + GetComponent<Patrol>().ToString();
                    return false;
                }
                counter++;
            }

        }
        if (GetComponent<Flee>() != null)
        {
            typeOfErrorMessage = "Flee Component";
            if (GetComponent<Flee>().objectToFleeFrom == null)
            {
                errorMessage = "There is no valid gameobject, please attach one.\n" + GetComponent<Flee>().ToString();
                return false;
            }
        }
        if (GetComponent<Detection>() != null)
        {
            typeOfErrorMessage = "Detection Component";
            if (GetComponent<Detection>().objectToDetect == null)
            {
                errorMessage = "There is no valid gameobject, please attach one.";
                return false;
            }
            if(GetComponent<Detection>().behaviour == "Seek" && GetComponent<Seek>() == null)
            {
                errorMessage = "There is no Seek component Attatched, please attach one.\n" + GetComponent<Seek>().ToString();
                return false;
            }
        }
        return true;
    }
}
