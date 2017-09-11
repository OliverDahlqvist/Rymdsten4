using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tech12 : MonoBehaviour {
    
    public Transform techArm;
    public Transform techPlate;
    public Transform vinyl;
    public Light spotLight;

    private bool possiblePlay;
    private bool playStart;
    private bool isPlaying;
    private bool armRotBack;

    [SerializeField] private float rpm;
    [SerializeField] private float armRotSpd;
    private float armStartRot;
    private float armCurrentRot;
    private float armTargetRot;

    AudioSource bambataAudio;
    private bool playBambata = false;

    // Awake
    void Awake () {

        spotLight.enabled = false;
        possiblePlay = false;
        playStart = false;
        isPlaying = false;
        vinyl.gameObject.SetActive(false);
        rpm = 120;
        armRotSpd = 25;
        armStartRot = 0;
        armCurrentRot = 0;
        armTargetRot = 1.3f;

        bambataAudio = GetComponent<AudioSource>();
    }

    // Update
    void Update () {

        if (possiblePlay && Input.GetKeyDown(KeyCode.E)) {
            
            playStart = true;
            spotLight.enabled = true;

            if (isPlaying) {

                playStart = false;
                isPlaying = false;
                armRotBack = true;
                spotLight.enabled = false;
                vinyl.gameObject.SetActive(false);
            }
        }  

        if (playStart) {

            isPlaying = true;
            techPlate.Rotate(Vector3.forward * rpm * Time.deltaTime);

            //arm activate
            armRotBack = false;
            if (armCurrentRot < armTargetRot) {

                techArm.Rotate(Vector3.forward * armRotSpd * Time.deltaTime);
                armCurrentRot += 1 * Time.deltaTime;
                //spawn record
                if (armCurrentRot >= armTargetRot && PlayerClass.sonicForceFound) {

                    vinyl.gameObject.SetActive(true);
                    playBambata = true; //activate sonic force
                }
            }
        }

        //PlayMusic
        if (playBambata) {
            bambataAudio.Play(); //execute sonic force
            playBambata = false;
        }

        //arm deactivate
        if (armRotBack && armCurrentRot > armStartRot) {

            techArm.Rotate(-Vector3.forward * armRotSpd * Time.deltaTime);
            armCurrentRot -= 1 * Time.deltaTime;
            bambataAudio.Stop();
        }
    }

    //OnTriggerEnter
    void OnTriggerEnter (Collider other) {

        if(other.CompareTag("Player"))
            possiblePlay = true;
    }

    //OnTriggerExit
    void OnTriggerExit(Collider other) {

        if (other.CompareTag("Player"))
            possiblePlay = false;
    }
}
