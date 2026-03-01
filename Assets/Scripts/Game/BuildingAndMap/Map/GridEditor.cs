using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapGridManager))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGridManager mapGridManager = (MapGridManager)target;


        if (GUILayout.Button("Show Grid"))
        {
            mapGridManager.VisualiseGrid();
        }
    }
}
