using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	CharacterController playerCollider;
	GameObject playerOnElevator;

	[SerializeField] float minHeight = 0;
	[SerializeField] float maxHeight = 10;
	[SerializeField] float moveSpeed = 1;
	float currentHeight;

	bool moveElevator, min, max;
	[HideInInspector]
	public bool onElevator;

    // Start
    void Awake () {

		playerCollider = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterController> ();
		playerOnElevator = GameObject.FindGameObjectWithTag ("PlayerElevator");

		moveElevator = false;
		onElevator = false;
		currentHeight = 0;
	}

	// Update
	void Update () {

		if (onElevator && Input.GetKeyDown (KeyCode.E) && PlayerClass.elevatorBatteryPlaced) {

            moveElevator = true;

            if (currentHeight >= maxHeight) {
                max = true;
            }
            else if (currentHeight <= minHeight) {
                min = true;
            }
		}
			
		//up
		if (moveElevator && min && (currentHeight < maxHeight)) {

			gameObject.transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			currentHeight += 0.1f;

			if (currentHeight >= maxHeight) 
				min = false;
		}

		//down
		if (moveElevator && max && (currentHeight > minHeight)) {

			gameObject.transform.Translate (-Vector3.forward * moveSpeed * Time.deltaTime);
			currentHeight -= 0.1f;

			if (currentHeight <= minHeight)
				max = false;
		}
	}

	// TriggerEnter
	void OnTriggerEnter (Collider other) {

		if (other.CompareTag ("Player")) {
			
			playerOnElevator.transform.parent = gameObject.transform;
		}
	}

	// TriggerExit
	void OnTriggerExit (Collider other) {

		if (other.CompareTag("Player")) {
			
			playerOnElevator.transform.parent = null;
		}
	}
}
