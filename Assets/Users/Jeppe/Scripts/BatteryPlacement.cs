using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPlacement : MonoBehaviour {

    public GameObject elevatorBatteryEnable;

    bool inside = false;

    private void Update() {

        if (inside) {

            if (PlayerClass.elevatorBatteryFound) {

                if (Input.GetKeyDown (KeyCode.E)) {

                    elevatorBatteryEnable.SetActive (true);
                    PlayerClass.elevatorBatteryPlaced = true;
                }
            }
        }
    }

	private void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Player") {

            inside = true;
        }
    }

    private void OnTriggerExit (Collider col) {

        if (col.gameObject.tag == "Player") {

            inside = false;
        }
    }
}
