using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if Unity_Editor
using UnityEditor;


[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Test test = (Test)target;

        GUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        // GUILayout.Height(20);
        if (GUILayout.Button("Name", GUILayout.Width(200), GUILayout.Height(40)))
        {
            test.ClickButton();
        }
        GUILayout.FlexibleSpace();
        // GUILayout.Height(20);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);
    }

}

#endif