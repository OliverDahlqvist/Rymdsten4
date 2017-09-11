using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour {
    
	public eNew eNewScr;

	// TriggerEnter
	void OnTriggerEnter (Collider other) {

		if (other.CompareTag ("Player")) {
            
;			eNewScr.onButton = true;
        }
	}

	// TriggerExit
	void OnTriggerExit (Collider col) {

		if (col.CompareTag ("Player")) {
            
			eNewScr.onButton = false;
		}
	}
}