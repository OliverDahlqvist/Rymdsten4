using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;
    [SerializeField] [Range(0, 1)] private float currentTimeOfDay = 0f;
    [SerializeField] private float timeMultiplier = 1f;

	private Light dr_Light;

    private float sunInitialIntensity;
	public float exposureIntensity = 0;

    // Start
    void Start () {

        sunInitialIntensity = sun.intensity;
		dr_Light = GameObject.FindWithTag("DirectionalLight").GetComponent<Light>();
    }

    // Update
    void Update () {

        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
            currentTimeOfDay = 0;

        //print("Day : " + (PlayerClass.duringDay));
    }

    // UpdateSun
    void UpdateSun() {

        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;

		// night
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f) {

            intensityMultiplier = 0;
			dr_Light.enabled = true;

            if (!PlayerClass.duringNight) {

                PlayerClass.duringNight = true;
                PlayerClass.duringDay = false;
            }
                
        }// day
        else if (currentTimeOfDay <= 0.25f) {
			
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
			dr_Light.enabled = true;

            if (!PlayerClass.duringDay) {

                PlayerClass.duringDay = true;
                PlayerClass.duringNight = false;
            }
                
        }// transition
        else if (currentTimeOfDay >= 0.73f) {
			
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
			dr_Light.enabled = true;
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
