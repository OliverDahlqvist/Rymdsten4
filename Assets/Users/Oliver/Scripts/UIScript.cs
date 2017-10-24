﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    //Variables for the character

    //public bool Inbounds = false;
    [Header("UI Elements")]
    public Slider oxygenSlider;
    public Text oxygenText;
    public Image oxygenTexture;
    public Image iconOre;
    public Text amountOre;
    public Text amountMaterial;
    public Image Visor;
    public Text scienceText;

    [Header("UI Colors")]
    public Color defaultColor;
    public Color defaultTextColor;
    public Color warningColor;
    public Color defaultColorOxygen;
    public Color ScreenColor;

    [Header("Light Colors")]
    public Color[] lightColor;

    [Header("Objects")]
    public Light mainLight;
    Color iconColor;
    Color oxygenColor;
    Color textColor;
    public RaycastHit hit;

    bool inCave = false;
    float lightT = 1;
    float rockT = 0;
    float oxygenT = 0;

    private int x = Screen.width / 2;
    private int y = Screen.height / 2;

    void Start()
    {
        oxygenSlider.maxValue = PlayerClass.EnergyMax;
        oxygenSlider.value = PlayerClass.EnergyMax;
        //EnergyPercentage.text = Energy.value + "%";
    }


    void OnTriggerEnter(Collider other) //Checks for collisions with other objects 
    {
        if (other.tag == "AirZoneEnter") //Checks for collisions with objects with the tag "AirZone"
        {
            PlayerClass.Inbounds = true; //If the character is inside an Airzone the bool Inbounds is set to true
        }
        else if(other.tag == "AirZoneExit")
        {
            PlayerClass.Inbounds = false;
        }
        else if(other.tag == "CaveEnter")
        {
            inCave = true;
        }
        else if(other.tag == "CaveExit")
        {
            inCave = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "AirZone")
        {
            PlayerClass.Inbounds = false;
        }
        ScreenColor = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        if (Physics.Raycast(ray, out hit, 2))
        {
            if (PlayerClass.menuActive < 1)
            {
                if (hit.collider.gameObject.tag == "CanBeActivated" || hit.collider.gameObject.tag == "DropOff" ||
                    hit.collider.gameObject.tag == "Upgrade" || hit.collider.gameObject.tag == "AudioLog" || hit.collider.gameObject.tag == "Vehicle" || hit.collider.gameObject.tag == "CraftingStation")
                {
                    PlayerClass.notificationText = "Press 'E' to activate";
                    PlayerClass.displayNotification = true;
                }
            }
        }
        if (inCave) // IN CAVE
        {
            if (lightT > 0)
            {
                lightT -= Time.deltaTime;
                mainLight.color = Color.Lerp(lightColor[1], lightColor[0], lightT);
            }
        }
        else if(!inCave)
        {
            if (lightT < 1)
            {
                lightT += Time.deltaTime;
                mainLight.color = Color.Lerp(lightColor[1], lightColor[0], lightT);
            }
        }

        if (!PlayerClass.Inbounds) // INSIDE
        {
            PlayerClass.CurrentEnergy -= Time.deltaTime * (PlayerClass.EnergyDrain * PlayerClass.TemperatureMultipier + PlayerClass.flashLightDrain + PlayerClass.jetPackDrain + PlayerClass.fallDmgDrain); //Energy is drained depending on where the player is. 
        }
        else if (PlayerClass.Inbounds && oxygenSlider.value <= PlayerClass.EnergyMax)
        {
            PlayerClass.CurrentEnergy += Time.deltaTime * PlayerClass.EnergyGain;
        }

        if (PlayerClass.CurrentEnergy > PlayerClass.EnergyMax)
        {
            PlayerClass.CurrentEnergy = PlayerClass.EnergyMax;
        }

        iconColor = Color.Lerp(defaultColor, warningColor, rockT);
        textColor = Color.Lerp(defaultTextColor, warningColor, rockT);
        oxygenColor = Color.Lerp(defaultColorOxygen, warningColor, oxygenT);
        if(PlayerClass.stones == PlayerClass.inventorySize && rockT < 1)
        {
            rockT += Time.deltaTime;
            amountOre.color = textColor;
            iconOre.color = iconColor;
        }
        else if(PlayerClass.stones != PlayerClass.inventorySize && rockT > 0)
        {
            rockT -= Time.deltaTime;
            amountOre.color = textColor;
            iconOre.color = iconColor;
        }
        if(oxygenSlider.value <= 20 && oxygenT < 1)
        {
            oxygenT += Time.deltaTime;
            oxygenTexture.color = oxygenColor;
        }
        else if(oxygenSlider.value > 20 && oxygenT > 0)
        {
            oxygenT -= Time.deltaTime;
            oxygenTexture.color = oxygenColor;
        }

        amountOre.text = PlayerClass.formatValue(PlayerClass.stones) + "/" + PlayerClass.formatValue(PlayerClass.inventorySize);
        oxygenSlider.value = PlayerClass.CurrentEnergy;
        amountMaterial.text = PlayerClass.formatValue(PlayerClass.credits);


        oxygenSlider.maxValue = PlayerClass.EnergyMax;

        oxygenText.text = oxygenSlider.value.ToString("F1") + "%"; //Updates the text by the energy slider
        scienceText.text = PlayerClass.sciencePoints.ToString("F0");
    }
}

