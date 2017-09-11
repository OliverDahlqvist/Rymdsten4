using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_AirLock : MonoBehaviour
{



    bool air = true;
    


    // Use this for initialization
    void Start()
    {


    }

    
    void Update()
    {


        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown (KeyCode.E))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2))
            {
                // Air lock telport bottons 
                if (hit.collider.gameObject.name == "TeleporterStart1" || (hit.collider.gameObject.name == "TeleporterStart4"))
                {
                    Debug.Log("hit TeleporterStart1");
                    transform.position = hit.transform.GetChild(0).position;
                }

                else if (hit.collider.gameObject.name == "TeleporterStart2" && air == false)
                {
                    
                    transform.position = hit.transform.GetChild(0).position;
                }

                else if (hit.collider.gameObject.name == "TeleporterStart3" && air == true)
                {
                    
                    transform.position = hit.transform.GetChild(0).position;
                }
                else if (hit.collider.gameObject.name == "TeleporterStart4")
                {
                    transform.position = hit.transform.GetChild(0).position;
                }






                // Air lock butten GREEN and RED


                 if (hit.collider.gameObject.name == "air lock button green" && air == false)
                {
                    air = true;
                    Debug.Log("air is now ON");
                   
                }

                else if (hit.collider.gameObject.name == "air lock button red" && air == true)
                {
                    air = false;
                    Debug.Log(" air is now OFF ");
                   
                }



                else { }

                }


                else
            {
                //  hit = new RaycastHit();

            }
        }

    }

}

