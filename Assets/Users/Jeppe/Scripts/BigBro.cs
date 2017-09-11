using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBro : MonoBehaviour {

    [SerializeField] private Transform targetTransform;
    private bool target;
	
    // Awake
    void Awake () {

        target = false;
    }

	// Update
	void Update () {

        if (target) {

            Vector3 relativePos = targetTransform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos);
        }
        
	}

    // OnTriggerEnter
    void OnTriggerEnter (Collider other) {

        if (other.CompareTag("Player"))
            target = true;
    }

    // OnTriggerExit
    void OnTriggerExit(Collider other) {

        if (other.CompareTag("Player"))
            target = false;
    }
}
