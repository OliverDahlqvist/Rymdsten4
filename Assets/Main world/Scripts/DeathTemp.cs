using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTemp : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		if (PlayerClass.CurrentEnergy <= 0) {
			
			SceneManager.LoadScene ("Main map");
		}
	}
}
