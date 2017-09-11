using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerScript : MonoBehaviour {
    public Text amountPowerText;
    public Slider powerSlider;
    [SerializeField]
    List<DrillPartScript> drills = new List<DrillPartScript>();
    private float timestamp = 0f;

	void Start () {
        powerSlider.maxValue = PlayerClass.maxPower;


        for(int i = 0; i < GameObject.FindGameObjectsWithTag("MiningDrill").Length; i++)
        {
            drills.Add(GameObject.FindGameObjectsWithTag("MiningDrill")[i].GetComponentInChildren<DrillPartScript>());
        }

	}

	void Update () {
        amountPowerText.text = PlayerClass.formatValue(PlayerClass.power) + "/" + PlayerClass.formatValue(PlayerClass.maxPower);
        powerSlider.value = PlayerClass.power;

        if (Time.time >= timestamp)
        {
            timestamp = Time.time + PlayerClass.globalTickRate;
            for (int i = 0; i < drills.Count; i++)
            {
                if (drills[i].drillOn)
                {
                    PlayerClass.credits += drills[i].orePerTick;
                }
            }
        }
	}
    public void addObject(DrillPartScript obj)
    {
        drills.Add(obj);
    }

}
