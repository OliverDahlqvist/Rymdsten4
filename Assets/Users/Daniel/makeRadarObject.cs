﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class makeRadarObject : MonoBehaviour {

    public Image image;

	// Use this for initialization
	void Start () {
        Radar.RegisterRadarObject( this.gameObject, image);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
    }
}
