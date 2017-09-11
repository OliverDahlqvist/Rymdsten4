using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class EnterExit : MonoBehaviour
{
    private bool inVehicle = false;
    /*Camera camera;*/
    public GameObject rover;
    public GameObject player;
    public GameObject PlayerParent;
    public bool Inbounds = false;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rover = GameObject.FindWithTag("Vehicle");
        /*camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();*/

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inbounds = true;
            Debug.Log("Inbounds is true");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inbounds = false;
        }
    }
    void Update()
    {
        if (inVehicle == true && Input.GetKeyDown(KeyCode.E))
        {
            rover.SetActive(false);
            player.SetActive(true);
            /*player.transform.position = gameObject.GetComponent<BoxCollider>().transform.position;*/
            player.transform.position = GameObject.FindWithTag("Chassi").transform.position;
            /*camera.transform.localEulerAngles = transform.localEulerAngles;*/
            inVehicle = false;
            Debug.Log(player.transform.parent);
        }

        else if (Input.GetKeyDown(KeyCode.E) && !inVehicle && Inbounds)
        {
            player.SetActive(false);
            inVehicle = true;
            Inbounds = false;
        }
    }
}
