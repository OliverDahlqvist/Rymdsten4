using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectionScript : MonoBehaviour {
    public bool clear;
    public List<GameObject> objectList = new List<GameObject>();
    Material mat;
    BuildSystem buildSystem;

	void Start () {
        clear = true;
        mat = GetComponent<Renderer>().material;
        buildSystem = Camera.main.GetComponent<BuildSystem>();
	}
    private void Update()
    {
        // Rotate //
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back);
        }

        // Change projection color //

        if (objectList.Count == 0 && clear)
        {
            mat.SetColor("_Color", new Color32(128, 210, 232, 65));
        }
        else
        {
            mat.SetColor("_Color", new Color32(225, 0, 0, 150));
            //PlayerClass.displayNotification = true;
        }
    }

    private void LateUpdate()
    {
        if(PlayerClass.credits < buildSystem.buildPrice)
        {
            PlayerClass.notificationText = "Not enough credits";
            PlayerClass.displayNotification = true;
            mat.SetColor("_Color", new Color32(225, 0, 0, 150));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Terrain"))
        {
            objectList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Terrain"))
        {
            objectList.Remove(other.gameObject);
        }
    }
}
