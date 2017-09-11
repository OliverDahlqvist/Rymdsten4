using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : MonoBehaviour {
    float outputMulti;
    float outputPower;
    bool isOn;


    void Start () {
        outputMulti = 1;

        isOn = false;
	}
	void Update () {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            isOn = !isOn;
        }
        if (isOn)
        {
            outputPower = 1000 * outputMulti * Time.deltaTime;
            if (outputPower + PlayerClass.power < PlayerClass.maxPower)
            {
                PlayerClass.power += outputPower;
            }
        }
	}
}
