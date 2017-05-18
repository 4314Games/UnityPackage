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

    string detectionButtonString = "Add Detection Component";
    string detectionButtonAddString = "Add Detection Component";
    string detectionButtonRemoveString = "Remove Detection Component";

    public string[] options = new string[] { "Seek", "Wander", "Patrol","Detection" };
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

        if (script.detectionAdded)
            detectionButtonString = detectionButtonRemoveString;
        else if (!script.detectionAdded)
            detectionButtonString = detectionButtonAddString;
    }
    public override void OnInspectorGUI()
    {
        GameObject script = ((MonoBehaviour)target).gameObject;
        SetupButtonText(script.GetComponent<AIManager>());
        DrawDefaultInspector();
        script.GetComponent<AIManager>().index = EditorGUILayout.Popup("Behaviour", script.GetComponent<AIManager>().index, options);
        switch (script.GetComponent<AIManager>().index)
        {
            case 0:
                if (script.GetComponent<Seek>() != null)
                    script.GetComponent<Seek>().toSeek = true;
                break;
            case 1:
                if (script.GetComponent<Wander>() != null)
                    script.GetComponent<Wander>().toWander = true;
                break;
            case 2:
                if (script.GetComponent<Patrol>() != null)
                    script.GetComponent<Patrol>().toPatrol = true;
                break;
            case 4:
                if (script.GetComponent<Detection>() != null)
                    script.GetComponent<Detection>().toDetect = true;
                break;
            default:
                break;
        }

        if (GUILayout.Button(seekButtonString))
        {
            if (script.GetComponent<AIManager>().seekAdded && script.GetComponent<Seek>() != null)
            {
                DestroyImmediate(script.GetComponent<Seek>());
                script.GetComponent<AIManager>().seekAdded = false;
            }
            else if (!script.GetComponent<AIManager>().seekAdded)
            {
                script.gameObject.AddComponent<Seek>();
                script.GetComponent<AIManager>().seekAdded = true;
            }

        }
        if (GUILayout.Button(wanderButtonString))
        {
            if (script.GetComponent<AIManager>().wanderAdded && script.GetComponent<Wander>() != null)
            {
                DestroyImmediate(script.GetComponent<Wander>());
                script.GetComponent<AIManager>().wanderAdded = false;
            }
            else if (!script.GetComponent<AIManager>().wanderAdded)
            {
                script.gameObject.AddComponent<Wander>();
                script.GetComponent<AIManager>().wanderAdded = true;
            }
        }
        if (GUILayout.Button(patrolButtonString))
        {
            if (script.GetComponent<AIManager>().patrolAdded && script.GetComponent<Patrol>() != null)
            {
                DestroyImmediate(script.GetComponent<Patrol>());
                script.GetComponent<AIManager>().patrolAdded = false;
            }
            else if (!script.GetComponent<AIManager>().patrolAdded)
            {
                script.gameObject.AddComponent<Patrol>();
                script.GetComponent<AIManager>().patrolAdded = true;
            }
        }
        if (GUILayout.Button(detectionButtonString))
        {
            if (script.GetComponent<AIManager>().detectionAdded && script.GetComponent<Detection>() != null)
            {
                DestroyImmediate(script.GetComponent<Detection>());
                script.GetComponent<AIManager>().detectionAdded = false;
            }
            else if (!script.GetComponent<AIManager>().detectionAdded)
            {
                script.gameObject.AddComponent<Detection>();
                script.GetComponent<AIManager>().detectionAdded = true;
            }
        }
        if (EditorApplication.isPlaying && EditorPrefs.GetBool("Start"))
        {
            EditorPrefs.SetBool("Start", false);
            CheckScriptsEditor(script.GetComponent<AIManager>());

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
