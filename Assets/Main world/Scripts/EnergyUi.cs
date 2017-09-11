using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUi : MonoBehaviour
{

    //Variables for the character

    //public bool Inbounds = false;
    public Slider Energy;
    public Text EnergyPercentage;
    public Text Inventory;
    public Text Material;
    public Text ActivationButton;
    public Slider Temperature;
    public Image Visor;

    private bool TempChangeHot = false;
    private bool TempChangeCold = false;
    private Color ScreenColor;

    /* public float InventoryMax;
     public float CurrentInventory = 0; 
     public int EnergyMax;
     public float EnergyDrain;
     public float EnergyGain; 
     public float CurrentEnergy;
     public float TemperatureMultipier = 1;
     public int SprintMultiplier;
      private float Credits; */



    // Use this for initialization
    void Start()
    {
        //Sets up the starting values and Initializes the ui. 
        Energy.maxValue = PlayerClass.EnergyMax;
        Energy.value = PlayerClass.EnergyMax;
        EnergyPercentage.text = Energy.value + "%";
    }


    void OnTriggerEnter(Collider other) //Checks for collisions with other objects 
    {
        if (other.tag == "AirZone") //Checks for collisions with objects with the tag "AirZone"
        {
            PlayerClass.Inbounds = true; //If the character is inside an Airzone the bool Inbounds is set to true
        }
        else if (other.tag == "TempZoneCold") //Checks for collisions with objects with the tag "Temperature"
        {
            TempChangeCold = true;
            ScreenColor = new Color(0f, 1, 1f, 0f);

            if (!PlayerClass.WarmSuitUpgrade)
            {
                PlayerClass.TemperatureMultipier = 3;    //Increases the oxygen drain to 3 per second
            }
            else
            {
                PlayerClass.TemperatureMultipier = 1.5f;
            }


        }
        else if (other.tag == "TempZoneHot") //Checks for collisions with objects with the tag "Temperature"
        {

            TempChangeHot = true;
            Debug.Log("Entering Hotzone");
            ScreenColor = new Color(0.6f, 0, 0, 0);

            if (!PlayerClass.WarmSuit)
            {
                PlayerClass.TemperatureMultipier = 3;    //Increases the oxygen drain to 3 per second
            }
            else
            {
                PlayerClass.TemperatureMultipier = 1.5f;
            }
        }



        if (other.tag == "CanBeActivated")
        {
            ActivationButton.text = "Press 'E' To activate";
        }

    }




    void OnTriggerExit(Collider other) //Resets the variables 
    {
        if (other.tag == "AirZone") //Upon exiting an airzone, the bool inbounds i set to false
        {
            PlayerClass.Inbounds = false;
        }
        else if (other.tag == "TempZoneCold") //Resets the oxygen drain to 1
        {
            PlayerClass.TemperatureMultipier = 1;
            TempChangeCold = false;

        }
        else if (other.tag == "TempZoneHot") //Resets the oxygen drain to 1
        {
            PlayerClass.TemperatureMultipier = 1;
            TempChangeHot = false;
        }
        if (other.tag == "CanBeActivated")
        {
            ActivationButton.text = "";
        }
    }


    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "CanBeActivated" || hit.collider.gameObject.tag == "DropOff" || hit.collider.gameObject.tag == "Upgrade" || hit.collider.gameObject.tag == "AudioLog" || hit.collider.gameObject.tag == "Vehicle")
            {
                ActivationButton.text = "Press 'E' To activate";
            }


            else if (hit.collider.gameObject.tag == "Warmsuit")
            {

                ActivationButton.text = "Press 'E' To Pickup Broken Warmsuit, repair at the hub upgrade menu";
            }
            else if (hit.collider.gameObject.tag == "Jetpack")
            {

                ActivationButton.text = "Press 'E' To Pickup Jetpack repair part, repair it at the hub upgrade menu";
            }

            else if (hit.collider.gameObject.tag == "ComRepairPart")
            {
                //ActivationButton.text = "This is a repair part for the communications array";
               
            }
            else if (hit.collider.gameObject.tag == "CarBattery")
            {

                ActivationButton.text = "Press 'E' to pick up the Car Battery, repair it at the base upgrade menu";
            }
            else if (hit.collider.gameObject.tag == "CarBattery")
            {

                ActivationButton.text = "Press 'E' to pick up the Car Battery, repair it at the base upgrade menu";
            }
            else if (hit.collider.gameObject.tag == "LaserDrill")
            {

                ActivationButton.text = "Press 'E' to pick up a repair part for the laser drill, build it in the upgrade menu";
            }
           
        }
        else
        {
            ActivationButton.text = "";
        }


            if (!PlayerClass.Inbounds) //If the character isn't inside a house
            {
                PlayerClass.CurrentEnergy -= Time.deltaTime * (PlayerClass.EnergyDrain * PlayerClass.TemperatureMultipier + PlayerClass.flashLightDrain + PlayerClass.jetPackDrain + PlayerClass.fallDmgDrain); //Energy is drained depending on where the player is. 
            }
            else if (PlayerClass.Inbounds && Energy.value <= PlayerClass.EnergyMax)
            {
                PlayerClass.CurrentEnergy += Time.deltaTime * PlayerClass.EnergyGain; //If the player is inside a house the energy increases the energy gained 
            }

            if (PlayerClass.CurrentEnergy > PlayerClass.EnergyMax) //Prevents energy from going higher than 100 
            {
                PlayerClass.CurrentEnergy = PlayerClass.EnergyMax;
            }



            PlayerClass.CurrentInventory = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MineStone>().stones; //Changes the inventoryText depending on how many rocks has been collected
            //PlayerClass.Credits = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MineStone>().PlayerClass.credits;

            Inventory.text = Mathf.Floor(PlayerClass.CurrentInventory) + "/" + Mathf.Floor(PlayerClass.inventorySize); //Changes the inventory text
            Energy.value = PlayerClass.CurrentEnergy;  //Updates the energy value on the slider 
            Material.text = Mathf.Floor(PlayerClass.credits) + "$";
            Energy.maxValue = PlayerClass.EnergyMax;

            if (TempChangeCold)
            {

                if (Temperature.value > -150)
                {
                    Temperature.value--;
                    //ScreenColor.a += 0.001f;
                    //Visor.color = ScreenColor;
                }

                if (Camera.main.GetComponent<FrostEffect>().FrostAmount < 0.3)
                {
                    Camera.main.GetComponent<FrostEffect>().FrostAmount += 0.001f;
                }
             }

            else if (!TempChangeCold)
            {

                if (Temperature.value < 0)
                {
                    Temperature.value++;
                    //ScreenColor.a -= 0.001f;
                    //Visor.color = ScreenColor;
                }


                if (Camera.main.GetComponent<FrostEffect>().FrostAmount > 0)
                {
                    Camera.main.GetComponent<FrostEffect>().FrostAmount -= 0.001f;
                }

            }
            if (TempChangeHot)
            {

            if (Temperature.value < 150)
            {
                Temperature.value++;
                //ScreenColor.a += 0.001f;
                //Visor.color = ScreenColor;
            }
               
            }
            else if (!TempChangeHot && Temperature.value > 0)
            {
                Temperature.value--;
                //ScreenColor.a -= 0.001f;
                //Visor.color = ScreenColor;
            }

            EnergyPercentage.text = Energy.value + "%"; //Updates the text by the energy slider 

        }

    }

