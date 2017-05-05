using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Detection))]
public class DetectionEditor : Editor {
    static string[] options = new string[] { "Seek" };
    static int editorIndex = 0;
    // Use this for initialization
    public override void OnInspectorGUI()
    {
        Detection script = (Detection)target;
        DrawDefaultInspector();
        editorIndex = EditorGUILayout.Popup("Behaviour", editorIndex, options);//Dropdown list of behaviours
        script.OnBehaviour(editorIndex);
    }
}
