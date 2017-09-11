using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    private Vector3 StartP1;
    private Vector3 EndP1;
    private Vector3 StartP2;
    private Vector3 EndP2;

    private float Dist = 4f;

    private float lerpTime = 1f;
    private float currentLerpTime = 0f;

    private bool door1Down = true;
    private bool door2Down = true;

    // Use this for initialization
    void Start()
    {
        StartP1 = Door1.transform.position;
        EndP1 = Door1.transform.position + Vector3.up * Dist;
        StartP2 = Door2.transform.position;
        EndP2 = Door2.transform.position + Vector3.up * Dist;
    }

    // Update is called once per frame
    void Update()
    {
        if (door1Down ==false)//(hit.collider.gameObject.name == "Door1Box" )
        {
            
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;

            }
            float proc = currentLerpTime / lerpTime;
            Door1.transform.position = Vector3.Lerp(StartP1, EndP1, proc);
            Door2.transform.position = Vector3.Lerp( EndP2 ,StartP2, proc);


        }

        if (door1Down)//(hit.collider.gameObject.name == "Door1Box" )
        {
            
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;

            }
            float proc = currentLerpTime / lerpTime;
            Door1.transform.position = Vector3.Lerp(EndP1, StartP1, proc);
            Door2.transform.position = Vector3.Lerp( StartP2,EndP2, proc);


        }


        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2))
            {
                // Air lock telport bottons 
                if ((hit.collider.gameObject.name == "DoorButten1" || hit.collider.gameObject.name == "DoorButten2" || hit.collider.gameObject.name == "DoorButten3") && currentLerpTime == lerpTime && door1Down == true)
                {
                    currentLerpTime = 0;
                    door1Down = false;
                    
                }

                else if ((hit.collider.gameObject.name == "DoorButten1"|| hit.collider.gameObject.name == "DoorButten2" || hit.collider.gameObject.name == "DoorButten3") && currentLerpTime==lerpTime && door1Down == false)
                {
                    currentLerpTime = 0;
                    door1Down = true;
                }




            }
        }
    }
}
