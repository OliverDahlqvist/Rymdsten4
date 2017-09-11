using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eNew : MonoBehaviour {

    [HideInInspector]
    public bool onButton = false;

    [SerializeField] private float eSpeed = 0;

    bool eTop = false;
    bool eBottom = true;

    bool moveUpDown = false;

    //Update
    private void Update() {
        
        if (onButton) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (PlayerClass.elevatorBatteryPlaced) {

                    if (eTop) {
                        moveUpDown = !moveUpDown;
                    }

                    if (eBottom) {
                        moveUpDown = !moveUpDown;
                    }
                }
            }
        }

        if (!moveUpDown)
            if (!eBottom)
                transform.Translate(-Vector3.forward * eSpeed * Time.deltaTime);

        if (moveUpDown)
            if (!eTop)
                transform.Translate(Vector3.forward * eSpeed * Time.deltaTime);
    }

    //OnTriggerEnter
    void OnTriggerEnter(Collider col) {
        
        if (col.CompareTag("ETopTrigger"))
            eTop = true;

        if (col.CompareTag("EBottomTrigger"))
            eBottom = true;
    }

    //OnTriggerExit
    void OnTriggerExit(Collider col) {

        if (col.CompareTag("ETopTrigger"))
            eTop = false;

        if (col.CompareTag("EBottomTrigger"))
            eBottom = false;
    }
}
