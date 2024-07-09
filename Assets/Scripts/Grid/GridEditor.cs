using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Instantiate Map"))
        {
            GridManager.Instance.InstantiateMap();
        }

        if (GUILayout.Button(""))
        {
            
        }
        
        if (GUILayout.Button(""))
        {
            
        }        

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }
        
        if (GUILayout.Button(""))
        {
            
        }        

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button(""))
        {
            
        }

        if (GUILayout.Button("Delete Map"))
        {
            GridManager.Instance.DestroyMap();
        }
    }
}
