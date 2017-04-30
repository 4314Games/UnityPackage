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

    public override void OnInspectorGUI()
    {
        AIManager script = (AIManager)target;
      
        script.UpdateAIFunction();
        DrawDefaultInspector(); 

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

        if (GUILayout.Button(seekButtonString))
        {
            if (script.seekAdded)
            {
                DestroyImmediate(script.gameObject.GetComponent<Seek>());
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
            if (script.wanderAdded)
            {
                DestroyImmediate(script.gameObject.GetComponent<Wander>());
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
            if (script.patrolAdded)
            {
                DestroyImmediate(script.gameObject.GetComponent<Patrol>());
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
            if (script.fleeAdded)
            {
                DestroyImmediate(script.gameObject.GetComponent<Flee>());
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
