using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] private GameObject PickUpObj;

	Vector3 rotation;
	Vector3 impRotation;
	Vector3 getMinScale;
	Vector3 getMaxScale;
	Vector3 currentScale;
	Vector3 getScaleImplode;

	[SerializeField] private float rotX = 0, rotY = 1, rotZ = 0;
	[SerializeField] private float impRotX = 1, impRotY = 1, impRotZ = 1;
	[SerializeField] private float rotSpeed = 50;
	[SerializeField] private float scaleMin = 0.3f, scaleMax = 0.5f;
	[SerializeField] private float scaleSpeed = 15;
	[SerializeField] private float foundRotSpeed = 1000;
	[SerializeField] private float impRotSpeed = 2000;
	[SerializeField] private float impScaleMax = 0.6f;
	[SerializeField] private float impScaleUpSpeed = 250;
	[SerializeField] private float impScaleDownSpeed = 1000;
	[SerializeField] private float impCountDown = 2f;

	private float countDown = 1;
	private bool scaleUpDown;
    private bool itemFound;

	// Awake
	void Awake () {

		rotation = new Vector3 (rotX, rotY, rotZ);
		impRotation = new Vector3 (impRotX, impRotY, impRotZ);

		getMinScale = new Vector3 (scaleMin, scaleMin, scaleMin);
		getMaxScale = new Vector3 (scaleMax, scaleMax, scaleMax);
		getScaleImplode = new Vector3 (impScaleMax, impScaleMax, impScaleMax);
        
        PickUpObj.transform.localScale = getMaxScale;
		currentScale = PickUpObj.transform.localScale;

		//false = scale smaller, true = scale bigger
		scaleUpDown = false;
        itemFound = false;
	}

	// Update
	void Update () {

		//down
		if (!scaleUpDown && currentScale.magnitude > getMinScale.magnitude) {
			
			currentScale -= new Vector3(0.01f, 0.01f, 0.01f) * scaleSpeed * Time.deltaTime;

			if (currentScale.magnitude <= getMinScale.magnitude)
				scaleUpDown = true;
		}

		//up
		if (scaleUpDown && currentScale.magnitude < getMaxScale.magnitude) {
			
			currentScale += new Vector3(0.01f, 0.01f, 0.01f) * scaleSpeed * Time.deltaTime;

			if (currentScale.magnitude >= getMaxScale.magnitude)
				scaleUpDown = false;
		}

		//implode
		if (itemFound) {

			if (currentScale.magnitude < getScaleImplode.magnitude)
				currentScale += new Vector3(0.01f, 0.01f, 0.01f) * impScaleUpSpeed * Time.deltaTime;

			if (impCountDown <= 0) {

				currentScale -= new Vector3(0.01f, 0.01f, 0.01f) * impScaleDownSpeed * Time.deltaTime;
                PickUpObj.transform.Rotate (impRotation * impRotSpeed * Time.deltaTime);

				if (currentScale.x <= 0.001f) {

                    Destroy(this.gameObject);
                    itemFound = false;
                }
			}

			impCountDown -= countDown * Time.deltaTime;
            PickUpObj.transform.Rotate (impRotation * foundRotSpeed * Time.deltaTime);
		}

        //set new scale & rotate
        PickUpObj.transform.localScale = currentScale;
        PickUpObj.transform.Rotate (rotation * rotSpeed * Time.deltaTime);
    }

    //TriggerStay
    void OnTriggerStay(Collider other) {

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)) {

            itemFound = true;
            GetComponentInParent<SphereCollider>().enabled = false;

            if (transform.CompareTag("JetPack") && !PlayerClass.jetPackFound)
                PlayerClass.jetPackFound = true;

            if (transform.CompareTag("SonicForceLP") && !PlayerClass.sonicForceFound)
                PlayerClass.sonicForceFound = true;

            if (transform.CompareTag("LaserDrill") && !PlayerClass.laserDrillFound)
                PlayerClass.laserDrillFound = true;

            if (transform.CompareTag("Warmsuit") && !PlayerClass.WarmSuitUpgrade)
                PlayerClass.WarmSuitUpgrade = true;

            if (transform.CompareTag("CarBattery") && !PlayerClass.CarRepairPartFound)
                PlayerClass.CarRepairPartFound = true;

            if (transform.CompareTag("ComRepairPart") && PlayerClass.ComRepairPart <= 4)
                PlayerClass.ComRepairPart++;

                // other.gameObject.GetComponent<EnergyUi>().ActivationButton.text = "You've found a com array repair part. Only "+ (4-PlayerClass.ComRepairPart) + " to go";
               
                

            if (transform.CompareTag ("ElevatorBattery") && !PlayerClass.elevatorBatteryFound)
                PlayerClass.elevatorBatteryFound = true;
        }
    }
}
