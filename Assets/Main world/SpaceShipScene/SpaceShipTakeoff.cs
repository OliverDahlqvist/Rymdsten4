using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipTakeoff : MonoBehaviour {

    public Camera shipCamera;
    public Camera spaceCamera;
    public Camera perspective;
    public Camera shipCamera2; 
    public ParticleSystem fire;
    public ParticleSystem afterBurner;
    public ParticleSystem launchSmoke;
    public AudioSource Alarm;
    public CreditsRoll startCredits; 
    public float speed;
    public float countdown = 10f;
    public int camSwitch = 0;

    // Use this for initialization
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime;

        if (countdown <=0)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            afterBurner.Play();
            speed += Time.deltaTime;
        }

        if (speed >= 15)
        {
            speed = 18;
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraSwitch1") && camSwitch == 0)
        {
            Camera.main.enabled = false;
            shipCamera.enabled = true;
            camSwitch++;
        }
        else if (other.CompareTag("CameraSwitch1") && camSwitch == 1)
        {
            shipCamera.enabled = false;
            perspective.enabled = true;
            camSwitch++;
        }
        else if (other.CompareTag("CameraSwitch1") && camSwitch == 2)
        {
            shipCamera.enabled = false;
            shipCamera2.enabled = true;
            camSwitch++;
        }
        else if (other.CompareTag("Sun"))
        {
            Alarm.Play();
            startCredits.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            startCredits.scrollGo = true; 


        }
    }
}
