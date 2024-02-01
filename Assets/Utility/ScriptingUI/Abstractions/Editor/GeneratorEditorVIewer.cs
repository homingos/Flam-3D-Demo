using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(ISceneGenerator),true)]
public class GeneratorsSCriptingUI : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        ISceneGenerator scene = (ISceneGenerator)target;
        if(GUILayout.Button("GenerateSceneUI"))
        {
            scene.GenerateSceneUI();
        }
        if(GUILayout.Button("GenerateScene"))
        {
            scene.GenerateScene();
        }
        if(GUILayout.Button("CleanScene"))
        {
            scene.CleanScene();
        }
        if(GUILayout.Button("CleanSceneUI"))
        {
            scene.CleanSceneUI();
        }
        if(GUILayout.Button("CleanSceneObjects"))
        {
            scene.CleanSceneObjects();
        }
    }
}
#endif