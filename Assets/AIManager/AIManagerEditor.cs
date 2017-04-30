using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIManager))]

public class AIManagerEditor : Editor {

    string seekButtonString = "Add Seek Component";
    string seekButtonAddString = "Add Seek Component";
    string seekButtonRemoveString = "Remove Seek Component";

    string wanderButtonString = "Add Wander Component";
    string wanderButtonAddString = "Add Wander Component";
    string wanderButtonRemoveString = "Remove Wander Component";

    string patrolButtonString = "Add Patrol Component";
    string patrolButtonAddString = "Add Patrol Component";
    string patrolButtonRemoveString = "Remove Patrol Component";

    string fleeButtonString = "Add Flee Component";
    string fleeButtonAddString = "Add Flee Component";
    string fleeButtonRemoveString = "Remove Flee Component";

    public void SetupButtonText(AIManager script)
    {
        if (script.seekAdded)
            seekButtonString = seekButtonRemoveString;
        else if (!script.seekAdded)
            seekButtonString = seekButtonAddString;

        if (script.wanderAdded)
            wanderButtonString = wanderButtonRemoveString;
        else if (!script.wanderAdded)
            wanderButtonString = wanderButtonAddString;

        if (script.patrolAdded)
            patrolButtonString = patrolButtonRemoveString;
        else if (!script.patrolAdded)
            patrolButtonString = patrolButtonAddString;

        if (script.fleeAdded)
            fleeButtonString = fleeButtonRemoveString;
        else if (!script.fleeAdded)
            fleeButtonString = fleeButtonAddString;
    }
    public override void OnInspectorGUI()
    {
        AIManager script = (AIManager)target;
        SetupButtonText(script);
        script.UpdateAIFunction();
        DrawDefaultInspector(); 

        if (GUILayout.Button(seekButtonString))
        {
            if (script.seekAdded && script.GetComponent<Seek>() != null)
            {
                DestroyImmediate(script.GetComponent<Seek>());
                script.seekAdded = false;
            }
            else if (!script.seekAdded)
            {
                script.gameObject.AddComponent<Seek>();
                script.seekAdded = true;
            }

        }
        if (GUILayout.Button(wanderButtonString))
        {
            if (script.wanderAdded && script.GetComponent<Wander>() != null)
            {
                DestroyImmediate(script.GetComponent<Wander>());
                script.wanderAdded = false;
            }   
            else if (!script.wanderAdded)
            {
                script.gameObject.AddComponent<Wander>();
                script.wanderAdded = true;
            }
        }
        if (GUILayout.Button(patrolButtonString))
        {
            if (script.patrolAdded && script.GetComponent<Patrol>() != null)
            {
                DestroyImmediate(script.GetComponent<Patrol>());
                script.patrolAdded = false;
            }
            else if (!script.patrolAdded)
            {
                script.gameObject.AddComponent<Patrol>();
                script.patrolAdded = true;
            }
        }
        if (GUILayout.Button(fleeButtonString))
        {
            if (script.fleeAdded && script.GetComponent<Flee>() != null)
            {
                DestroyImmediate(script.GetComponent<Flee>());
                script.fleeAdded = false;
            }
            else if (!script.fleeAdded)
            {
                script.gameObject.AddComponent<Flee>();
                script.fleeAdded = true;
            }
        }
    }
}
