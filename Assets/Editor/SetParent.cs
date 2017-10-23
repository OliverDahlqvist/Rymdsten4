using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetParent : EditorWindow {
    Transform prefabObject;
    Transform[] selectedObjects;

    [MenuItem("Tools/Set Parent")]
    public static void ShowWindow()
    {
        GetWindow<SetParent>("Set Parent");
    }

    void OnGUI()
    {
        prefabObject = (Transform)EditorGUILayout.ObjectField(prefabObject, typeof(Transform), true);
        if (prefabObject != null)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Parent", GUILayout.Height(20), GUILayout.Width(100)))
            {
                selectedObjects = Selection.GetTransforms(SelectionMode.Editable);

                foreach (Transform obj in selectedObjects)
                {
                    Undo.SetTransformParent(obj, prefabObject, obj.name);
                }
            }
            if(GUILayout.Button("Undo", GUILayout.Height(20), GUILayout.Width(100)))
            {
                Undo.PerformUndo();
            }
            if(GUILayout.Button("Clear", GUILayout.Height(20), GUILayout.Width(100)))
            {
                clearObject();
            }
        }
        else
        {
            if (selectedObjects != null)
            {
                if (selectedObjects.Length > 1)
                {
                    GUILayout.Label("Too many objects selected");
                }
                else if (selectedObjects.Length < 1)
                {
                    GUILayout.Label("Select an object");
                }
                else
                {
                    if (GUILayout.Button("Set Target", GUILayout.Height(20), GUILayout.Width(100)))
                    {
                        prefabObject = selectedObjects[0];
                    }
                }
            }
        }
        
    }

    private void OnSelectionChange()
    {
        selectedObjects = Selection.GetTransforms(SelectionMode.Editable);
        Repaint();
    }

    private void clearObject()
    {
        prefabObject = null;
    }
}
