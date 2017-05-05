using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Kyle Norton 2017
[CustomEditor(typeof(AIManager))]
public class AIManagerEditor : Editor
{

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

    string detectionButtonString = "Add Detection Component";
    string detectionButtonAddString = "Add Detection Component";
    string detectionButtonRemoveString = "Remove Detection Component";

    public string[] options = new string[] { "Seek", "Wander", "Patrol", "Flee", "Dection" };
    public int index = 0;

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

        if (script.detectionAdded)
            detectionButtonString = detectionButtonRemoveString;
        else if (!script.detectionAdded)
            detectionButtonString = detectionButtonAddString;
    }
    public override void OnInspectorGUI()
    {
        AIManager script = (AIManager)target;
        SetupButtonText(script);
        DrawDefaultInspector();
        if (EditorPrefs.HasKey("index"))
            index = EditorPrefs.GetInt("index");
        index = EditorGUILayout.Popup("Behaviour", index, options);
        EditorPrefs.SetInt("index", index);
        switch (index)
        {
            case 0:
                script.toWander = false;
                script.toPatrol = false;
                script.toFlee = false;
                script.toDetect = false;
                script.toSeek = true;
                break;
            case 1:
                script.toWander = true;
                script.toPatrol = false;
                script.toFlee = false;
                script.toDetect = false;
                script.toSeek = false;
                break;
            case 2:
                script.toWander = false;
                script.toPatrol = true;
                script.toFlee = false;
                script.toDetect = false;
                script.toSeek = false;
                break;
            case 3:
                script.toWander = false;
                script.toPatrol = false;
                script.toFlee = true;
                script.toDetect = false;
                script.toSeek = false;
                break;
            case 4:
                script.toWander = false;
                script.toPatrol = false;
                script.toFlee = false;
                script.toDetect = true;
                script.toSeek = false;
                break;
            default:
                break;
        }

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
        if (GUILayout.Button(detectionButtonString))
        {
            if (script.detectionAdded && script.GetComponent<Detection>() != null)
            {
                DestroyImmediate(script.GetComponent<Detection>());
                script.detectionAdded = false;
            }
            else if (!script.detectionAdded)
            {
                script.gameObject.AddComponent<Detection>();
                script.detectionAdded = true;
            }
        }
        script.UpdateAIFunction();
        if (EditorApplication.isPlaying && EditorPrefs.GetBool("Start"))
        {
            EditorPrefs.SetBool("Start", false);
            CheckScriptsEditor(script);

        }
    }
    public void Awake()
    {
        EditorPrefs.SetBool("Start", true);
    }
    public void CheckScriptsEditor(AIManager script)
    {
        if (!script.CheckScripts())
        {
            if (EditorUtility.DisplayDialog(script.typeOfErrorMessage, script.errorMessage, "Okay"))//Throw error box if scrips areb't correct and then shutdown application/game.
            {
                EditorApplication.isPlaying = false;
            }
        }
    }
}
