using UnityEngine;
using System.Collections;

public class FrontLight : CarLight
{

	// Use this for initialization
	void Start () {
        lightOn();
        m_car.frontLightsTrigger += lightsHandler;
    }
	
	void lightsHandler(bool isOn)
    {
        if (isOn)
            lightOn();
        else lightOff();
    }
}
