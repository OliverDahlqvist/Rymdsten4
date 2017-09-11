using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    //Variables for the character

    //public bool Inbounds = false;
    public Slider oxygenSlider;
    public Text oxygenText;
    public Image oxygenTexture;
    public Image iconOre;
    public Text amountOre;
    public Text amountMaterial;
    public Text notifications;
    public Text notificationsShadow;
    public Image Visor;

    public Color defaultColor;
    public Color warningColor;
    public Color ScreenColor;
    Color rockColor;
    Color oxygenColor;
    FrostEffect frostScript;
    public RaycastHit hit;

    float rockT = 0;
    float oxygenT = 0;
    string warning = "";

    private int x = Screen.width / 2;
    private int y = Screen.height / 2;

    void Start()
    {
        oxygenSlider.maxValue = PlayerClass.EnergyMax;
        oxygenSlider.value = PlayerClass.EnergyMax;
        //EnergyPercentage.text = Energy.value + "%";
        frostScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FrostEffect>();
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
        if (other.tag == "CanBeActivated")
        {
            PlayerClass.notificationText = "Press 'E' To activate";
        }

    }




    void OnTriggerExit(Collider other)
    {
        if (other.tag == "AirZone")
        {
            PlayerClass.Inbounds = false;
        }
        if (other.tag == "CanBeActivated")
        {
            notifications.text = "";
        }
        ScreenColor = new Color(0, 0, 0, 0);
    }


    // Update is called once per frame
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
        /*ActivationButton_Shadow.text = ActivationButton.text;
        ActivationButton_Shadow.fontSize = ActivationButton.fontSize;*/


        if (!PlayerClass.Inbounds) //If the character isn't inside a house
        {
            PlayerClass.CurrentEnergy -= Time.deltaTime * (PlayerClass.EnergyDrain * PlayerClass.TemperatureMultipier + PlayerClass.flashLightDrain + PlayerClass.jetPackDrain + PlayerClass.fallDmgDrain); //Energy is drained depending on where the player is. 
        }
        else if (PlayerClass.Inbounds && oxygenSlider.value <= PlayerClass.EnergyMax)
        {
            PlayerClass.CurrentEnergy += Time.deltaTime * PlayerClass.EnergyGain; //If the player is inside a house the energy increases the energy gained 
        }

        if (PlayerClass.CurrentEnergy > PlayerClass.EnergyMax) //Prevents energy from going higher than 100 
        {
            PlayerClass.CurrentEnergy = PlayerClass.EnergyMax;
        }

        rockColor = Color.Lerp(defaultColor, warningColor, rockT);
        oxygenColor = Color.Lerp(defaultColor, warningColor, oxygenT);
        if(PlayerClass.stones == PlayerClass.inventorySize && rockT < 1)
        {
            rockT += Time.deltaTime;
            amountOre.color = rockColor;
            iconOre.color = rockColor;
        }
        else if(PlayerClass.stones != PlayerClass.inventorySize && rockT > 0)
        {
            rockT -= Time.deltaTime;
            warning = "";
            amountOre.color = rockColor;
            iconOre.color = rockColor;
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

        oxygenText.text = oxygenSlider.value + "%"; //Updates the text by the energy slider
    }
}

