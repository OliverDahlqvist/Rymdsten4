using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(SphereCollider))]

public class ProxLight : MonoBehaviour {
    
    private Light otherLight;
    private SphereCollider trigger;
    private CharacterController playerController;

    [SerializeField] private float baseIntensity = 0;
    [SerializeField] private float maxIntensity = 1;
    [SerializeField] private float minIntensity = 0;
    [SerializeField] private float increaseIntensity = 5;
    [SerializeField] private float decreaseIntensity = 1;

    private bool playerEnterExit = true;

    // Awake
    void Awake () {

        otherLight = GetComponent<Light>();
        otherLight.intensity = baseIntensity;

        trigger = transform.GetComponent<SphereCollider>();
        trigger.radius = 3.5f;
        trigger.isTrigger = true;

        //Get player collision from "charactercontroller". Did not work with getting player capsule collider.
        playerController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }

    // Update
    void Update() {

        //Dexrease light
        if (playerEnterExit == false && otherLight.intensity > minIntensity) {

            otherLight.intensity -= decreaseIntensity * Time.deltaTime;

            if (otherLight.intensity <= minIntensity) {

                playerEnterExit = false;
                otherLight.enabled = false;
            }
        }
    }

    // OnTriggerEnter
    void OnTriggerEnter(Collider other) {

        playerEnterExit = true;

        if (other == playerController) {
            //Enable light
            otherLight.enabled = true;
        }
    }

    // OnTriggerStay
    void OnTriggerStay(Collider other) {

        //Increase light
        if (other == playerController && PlayerClass.duringNight) {
            
            if (otherLight.intensity < maxIntensity)
                otherLight.intensity += increaseIntensity * Time.deltaTime;
        }

        if (otherLight.intensity == maxIntensity)
            otherLight.intensity = maxIntensity;
    }

    // OnTriggerExit
    void OnTriggerExit(Collider other) {

        if (other == playerController) {
            //Disable light
            playerEnterExit = false;
        }
    }
}
