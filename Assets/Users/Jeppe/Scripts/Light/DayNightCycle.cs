using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;
    [SerializeField] [Range(0, 1)] private float currentTimeOfDay = 0f;
    [SerializeField] private float timeMultiplier = 1f;

	private Light dr_Light;
    
	public float exposureIntensity = 0;

    public DeepSky.Haze.DS_HazeCore deepSky;

    // Update
    void Update () {
        
        UpdateSun();
        
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        deepSky.Time = currentTimeOfDay;
        if (currentTimeOfDay >= 1)
            currentTimeOfDay = 0;
    }

    // UpdateSun
    void UpdateSun() {

        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

		// night
        if (currentTimeOfDay <= 0.25f || currentTimeOfDay >= 0.8f) {
			sun.enabled = false;
        }// day
        else
        {
            sun.enabled = true;
        }
    }
}
