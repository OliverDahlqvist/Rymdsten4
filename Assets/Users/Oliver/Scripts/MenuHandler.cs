using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MenuHandler : MonoBehaviour {
    BlurOptimized blurEffect;
    Movement movement;
    [SerializeField]
    GameObject upgradeCamera;
    [SerializeField]
    GameObject miningDrillCam;


    void Start () {
        blurEffect = GameObject.FindGameObjectWithTag("UICamera").GetComponentInChildren<BlurOptimized>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

	void Update () {
        if(PlayerClass.menuActive > 1 && Input.GetKey(KeyCode.Escape))
        {
            PlayerClass.menuActive = 0;
        }
        
        if (PlayerClass.menuActive == 1)
        {
            upgradeCamera.SetActive(true);
            blurEffect.enabled = true;
            movement.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(PlayerClass.menuActive == 2)
        {
            blurEffect.enabled = true;
            movement.enabled = false;
            miningDrillCam.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if(PlayerClass.menuActive < 1)
        {
            upgradeCamera.SetActive(false);
            miningDrillCam.SetActive(false);
            blurEffect.enabled = false;
            movement.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
