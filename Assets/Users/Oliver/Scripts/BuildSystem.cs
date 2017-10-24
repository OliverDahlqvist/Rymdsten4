using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSystem : MonoBehaviour {
    public GameObject projPrefab;
    public GameObject drillObject;
    public Text priceText;
    GameObject projObject;
    ProjectionScript projVariables;

    //UIScript uiScript;
    Camera cam;
    RaycastHit hit;
    GameObject lastDrill;
    private int x;
    private int y;
    string text;
    public float buildPrice;


    bool projExists = false;

    void Start () {
        //uiScript = GetComponent<UIScript>();
        cam = Camera.main;

        x = Screen.width / 2;
        y = Screen.height / 2;
        priceText.text = PlayerClass.formatValue(buildPrice) + "/" + PlayerClass.formatValue(PlayerClass.credits);
    }

    void OnDisable()
    {
        //PlayerClass.building = !PlayerClass.building;
        Destroy(projObject.gameObject);
        projExists = false;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.B)) {
            PlayerClass.building = !PlayerClass.building;
            Destroy(projObject.gameObject);
            projExists = false;
        }

        if (!projExists)
        {
            projObject = Instantiate(projPrefab, hit.point, Quaternion.Euler(-90, 0, 0));
            projVariables = projObject.transform.GetComponent<ProjectionScript>();
            projExists = true;
        }

        Ray ray = cam.ScreenPointToRay(new Vector3(x, y, 0));
        if(Physics.Raycast(ray, out hit, 10))
        {
            projObject.transform.position = hit.point;

            if (hit.collider.CompareTag("Terrain") && hit.normal.y > 0.70f)
            {
                projVariables.clear = true;
            }
            else
            {
                if (hit.normal.y < 0.70f && hit.collider.CompareTag("Terrain"))
                {
                    projVariables.clear = false;
                    text = "Too steep";
                    PlayerClass.notificationText = text;
                    PlayerClass.displayNotification = true;
                }
            }    
        }
        else if(projExists)
        {
            projObject.transform.position = ray.origin + (cam.transform.forward * 10);
            projVariables.clear = false;
            text = "Not touching ground";
            PlayerClass.notificationText = text;
            PlayerClass.displayNotification = true;
        }

        if(projVariables.objectList.Count > 0)
        {
            text = "Object in the way";
            PlayerClass.notificationText = text;
            PlayerClass.displayNotification = true;
        }

        if (Input.GetMouseButtonDown(0) && projVariables.clear && projVariables.objectList.Count == 0 && PlayerClass.credits >= buildPrice)
        {
            lastDrill = Instantiate(drillObject, projObject.transform.position, projObject.transform.localRotation * Quaternion.Euler(90, 0, 0));
            PlayerClass.amountDrills++;
            lastDrill.GetComponentInChildren<ParticleSystem>().name = "Mining Drill " + PlayerClass.amountDrills;
            PlayerClass.credits -= buildPrice;

            if (buildPrice > PlayerClass.credits)
            {
                priceText.text = "Not enough material";
            }
            else
            priceText.text = PlayerClass.formatValue(buildPrice) + "/" + PlayerClass.formatValue(PlayerClass.credits);
                
        }
	}
}
