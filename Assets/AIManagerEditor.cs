using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIManager))]

public class AIManagerEditor : Editor {


    public override void OnInspectorGUI()
    {
        AIManager script = (AIManager)target;
        script.UpdateAIFunction();
        DrawDefaultInspector();
        if (GUILayout.Button("Edit Seek Settings"))
        {
            //close all other dropdowns
            //Open dropdown menu to edit seek settings
            foreach (Component item in script.gameObject.GetComponents<Component>())
            {

                if (item.GetType() == typeof(Seek))
                {
                    DestroyImmediate(item);
                }
              

            }
            script.gameObject.AddComponent<Seek>();

        }
        if (GUILayout.Button("Edit Wander Settings"))
        {
            //close all other dropdowns
            //Open dropdown menu to edit seek settings
            foreach (Component item in script.gameObject.GetComponents<Component>())
            {

                if (item.GetType() == typeof(Wander))
                {
                    DestroyImmediate(item);
                }
              

            }
            script.gameObject.AddComponent<Wander>();

        }
        if (GUILayout.Button("Edit Patrol Settings"))
        {
            //close all other dropdowns
            //Open dropdown menu to edit seek settings
            foreach (Component item in script.gameObject.GetComponents<Component>())
            {

                if (item.GetType() == typeof(Patrol))
                {
                    DestroyImmediate(item);
                }


            }
            script.gameObject.AddComponent<Patrol>();
        }
        if (GUILayout.Button("Edit Flee Settings"))
        {
            //close all other dropdowns
            //Open dropdown menu to edit seek settings
        }
    }
}
