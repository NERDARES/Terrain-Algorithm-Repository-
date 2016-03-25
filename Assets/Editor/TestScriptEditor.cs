using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[CustomEditor(typeof(TestScript))]
public class TestScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (TestScript)target;


        if(GUILayout.Button("Test me"))
        {
            myTarget.Sleep();
        }

    }

}





