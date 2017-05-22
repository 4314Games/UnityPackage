using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Kyle Norton 2017
[RequireComponent(typeof(Unit))]
public class AIManager : MonoBehaviour
{
    [HideInInspector]
    public bool toWander = false;
    [HideInInspector]
    public bool toSeek = false;
    [HideInInspector]
    public bool toPatrol = false;
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
    //What behaviour has been added or not and what behaviours are currently running
    [HideInInspector]
    public string errorMessage;//actual error message
    [HideInInspector]
    public string typeOfErrorMessage;//Type of error message e.g - type of script

    public int index = 0;//behaviour index

    // Update is called once per frame
    public void UpdateAIFunction()
    {
        if (toWander)
        {
            toSeek = false;
            toPatrol = false;
            toDetect = false;
            if (GetComponent<Wander>() != null)
                GetComponent<Wander>().toWander = true;
        }
        if (toSeek)
        {
            toWander = false;
            toPatrol = false;
            toDetect = false;
            if (GetComponent<Seek>() != null)
                GetComponent<Seek>().toSeek = true;
        }
        if (toPatrol)
        {
            toWander = false;
            toSeek = false;
            toDetect = false;
            if (GetComponent<Patrol>() != null)
                GetComponent<Patrol>().toPatrol = true;
        }
        if (toDetect)
        {
            toWander = false;
            toSeek = false;
            toPatrol = false;
            if (GetComponent<Detection>() != null)
                GetComponent<Detection>().toDetect = true;
        }
        if (GetComponent<Seek>() != null)
            GetComponent<Seek>().toSeek = toSeek;
        if (GetComponent<Patrol>() != null)
            GetComponent<Patrol>().toPatrol = toPatrol;
        if (GetComponent<Wander>() != null)
            GetComponent<Wander>().toWander = toWander;
        if (GetComponent<Detection>() != null)
            GetComponent<Detection>().toDetect = toDetect;
    }//Will check what behaviours are enabled/ added an go into the appropriate script and enable them

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
        }//Checks the Seek script to see if all necc. components are filled
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
        }//Checks the Wander script to see if all necc. components are filled
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

        }//Checks the Patrol script to see if all necc. components are filled
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
        }//Checks the Detection script to see if all necc. components are filled
        return true;
    }
}
