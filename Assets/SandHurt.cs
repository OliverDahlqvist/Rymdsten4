using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHurt : MonoBehaviour {

	// Use this for initialization
	void Start () {
       }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerClass.CurrentEnergy -= (PlayerClass.EnergyMax /10) * Time.deltaTime;
        }
    }
    // Update is called once per frame
    
		
	}
