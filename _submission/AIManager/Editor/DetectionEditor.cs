using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Detection))]
//Kyle Norton 2017
public class DetectionEditor : Editor
{
    static string[] options = new string[] { "Seek" };
    static int editorIndex = 0;
    // Use this for initialization
    public override void OnInspectorGUI()
    {
        GameObject script = ((MonoBehaviour)target).gameObject;
        DrawDefaultInspector();
        editorIndex = EditorGUILayout.Popup("Behaviour", editorIndex, options);//Dropdown list of behaviours
        script.GetComponent<Detection>().OnBehaviour(editorIndex);
    }//Get the behaviour from the drop down menu and set it accordingly within the script
}
