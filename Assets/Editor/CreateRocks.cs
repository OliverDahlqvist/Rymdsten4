using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class CreateRocks : EditorWindow {

    public GameObject selectedPrefab;
    public List<Object> rocks = new List<Object>();
    private int buttonWidth = 100;
    private bool editing = false;

    [MenuItem("Tools/Create rocks")]
	public static void ShowWindow()
    {
        GetWindow<CreateRocks>("Create Rocks");
    }

    void OnGUI()
    { 
        if (GUILayout.Button("Update rocks", GUILayout.Height(20)))
        {
            rocks.Clear();

            rocks = Resources.LoadAll("Rocks").ToList();
            AssetPreview.SetPreviewTextureCacheSize(rocks.Count);
        }
        editing = GUILayout.Toggle(editing, "Editing mode");

        GUILayout.BeginHorizontal();

        if (rocks != null)
        {
            int width = -100;
            float maxWidth = EditorGUIUtility.currentViewWidth;
            for(int i = 0; i < rocks.Count; i++)
            {
                width += buttonWidth;
                if(width + buttonWidth > maxWidth)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    width = 0;
                }
                if (GUILayout.Button(AssetPreview.GetAssetPreview(rocks[i]), GUILayout.Width(buttonWidth), GUILayout.Height(buttonWidth)))
                {
                    selectedPrefab = (GameObject)rocks[i];
                }
            }
            if (selectedPrefab != null)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label(AssetPreview.GetAssetPreview(selectedPrefab));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                GUILayout.Label("Selected: " + selectedPrefab.name, EditorStyles.boldLabel);
            }
        }
        /*if (editing && selectedPrefab != null)
        {


            if (Event.current.type == EventType.MouseUp)
            {
                Debug.Log(1);
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x, SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 1000))
                {
                    GameObject newRock = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);
                    selectedPrefab.transform.position = hitInfo.point;
                }
            }
            Event.current.Use();
        }*/
    }

    void OnSceneGUI(SceneView scene)
    {
        if (editing)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (Event.current.type == EventType.MouseUp)
            {
                Vector2 guiPosition = Event.current.mousePosition;
                Ray ray = HandleUtility.GUIPointToWorldRay(guiPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    GameObject newRock = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);
                    selectedPrefab.transform.position = hit.point;
                }
            }
        }
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }
    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }
}