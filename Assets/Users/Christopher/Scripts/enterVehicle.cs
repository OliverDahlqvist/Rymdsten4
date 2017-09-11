using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class enterVehicle : MonoBehaviour
{
    Steering steeringScript;
    PlayerCar playerCarScript;
    CarAudio carAudioScript;
    UserInput UserInput;
    CarControl carControlScript;
    Car carScript;
    Wings wingsScript;
    EPSSystem EPSSystemScript;
    TCSSystem TCSSystemScript;
    CarEffects carEffectsScript;
    UserInput userInputScript;
    AudioListener audioListener;
    Camera carCamera;

    public CarControl carC;
    public GameObject player;
    public GameObject PlayerParent; 
    public bool Inbounds = false;

    Vector3 SpawnPoint; 
 

    void Start()
    {
        audioListener = GetComponentInChildren<AudioListener>();
        steeringScript = GetComponent<Steering>();
        playerCarScript = GetComponent<PlayerCar>();
        carAudioScript = GetComponent<CarAudio>();
        UserInput = GetComponent<UserInput>();
        carControlScript = GetComponent<CarControl>();
        carScript = GetComponent<Car>();
        wingsScript = GetComponent<Wings>();
        EPSSystemScript = GetComponent<EPSSystem>();
        TCSSystemScript = GetComponent<TCSSystem>();
        carEffectsScript = GetComponent<CarEffects>();
        player = GameObject.FindWithTag("Player");
        carCamera = GameObject.FindWithTag("CarCam").GetComponent<Camera>();
        SpawnPoint = new Vector3(GameObject.FindWithTag("Chassi").transform.position.x, GameObject.FindWithTag("Chassi").transform.position.y, GameObject.FindWithTag("Chassi").transform.position.z);
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
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
        SpawnPoint = GameObject.FindWithTag("Chassi").transform.position;

        //Debug.Log(SpawnPoint);
        if (PlayerClass.inVehicle == true && Input.GetKeyDown(KeyCode.E) && carC.speedInKmH < 1f)
        {
            audioListener.enabled = false;
            player.SetActive(true);
            steeringScript.enabled = false;
            playerCarScript.enabled = false;
            carAudioScript.enabled = false;
            carCamera.enabled = false;
            carAudioScript.StopSound();
            UserInput.enabled = false;
            carControlScript.enabled = false;
            carScript.enabled = false;
            wingsScript.enabled = false;
            EPSSystemScript.enabled = false;
            TCSSystemScript.enabled = false;
            carEffectsScript.enabled = false;
            /*player.transform.position = gameObject.GetComponent<BoxCollider>().transform.position;*/
            player.transform.localPosition = SpawnPoint;
            //Debug.Log(player.transform.position);
                
            /*camera.transform.localEulerAngles = transform.localEulerAngles;*/
            PlayerClass.inVehicle = false; 
            /*Debug.Log(player.transform.parent);*/
        }

        else if (Input.GetKeyDown(KeyCode.E) && !PlayerClass.inVehicle && Inbounds && PlayerClass.CarRepaired == true && !PlayerClass.activeMenu)
        {
            audioListener.enabled = true;
            steeringScript.enabled = true;
            playerCarScript.enabled = true;
            UserInput.enabled = true;
            carAudioScript.enabled = true;
            carCamera.enabled = true;
            carControlScript.enabled = true;
            carScript.enabled = true;
            wingsScript.enabled = true;
            EPSSystemScript.enabled = true;
            TCSSystemScript.enabled = true;
            carEffectsScript.enabled = true;
            player.SetActive(false);
            PlayerClass.inVehicle = true;
            Inbounds = false; 
        }
    }
}
