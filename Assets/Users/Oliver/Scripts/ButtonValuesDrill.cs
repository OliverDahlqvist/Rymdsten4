using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValuesDrill : MonoBehaviour {
    public float price;
    public string desc;
    public int currentRank;
    public float currentValue;
    public string prefix;
    public string suffix;
    public bool partsFound;
    public Button button;
    public bool isHover;
    [SerializeField]
    MiningdrillUpgradeMenu upgradeScript;
    private string buttonName;
    public string droneDesc;

    void Start () {
        button = GetComponent<Button>();
        buttonName = transform.name;
        droneDesc = "Purchase a Cargo drone that will retrieve and deliver cargo from your drill to the forge.";
    }
	void Update () {
        if (PlayerClass.currentMenu == 1)
        {
            if (buttonName == "Efficiency")
            {
                price = upgradeScript.selectedDrill.upgradeCost;
                currentRank = upgradeScript.selectedDrill.currentRank;
                currentValue = upgradeScript.selectedDrill.orePerTick;
                prefix = "Rank: ";
                suffix = "Current ore/tick: ";
                desc = "+1 Ore/Tick";
                partsFound = true;
            }

            else if (buttonName == "Cargo")
            {
                price = upgradeScript.selectedDrill.upgradeCostCargo;
                currentRank = upgradeScript.selectedDrill.cargoRank;
                currentValue = upgradeScript.selectedDrill.drillAmountMax;
                prefix = "Rank: ";
                suffix = "Current size: ";
                desc = "Drill cargo size: +100";
                partsFound = true;
            }
        }
        else if(PlayerClass.currentMenu == 2)
        {
            if (buttonName == "Cargo")
            {
                if (upgradeScript.selectedDrill.droneBuilt)
                {
                    price = upgradeScript.selectedDrill.droneCargoCost;
                    currentRank = upgradeScript.selectedDrill.droneCargoRank;
                    currentValue = upgradeScript.selectedDrill.droneCargoScript.inventoryMax;
                    prefix = "Rank: ";
                    suffix = "Current size: ";
                    desc = "Drone cargo size: +100";
                    partsFound = true;
                }
                else
                {
                    price = 0;
                    currentRank = 0;
                    currentValue = 0;
                    prefix = "Rank: ";
                    suffix = "Current size: ";
                    desc = "Build a cargo drone first!";
                    partsFound = false;
                }
            }
            else if(buttonName == "PurchaseDrone")
            {
                if (upgradeScript.selectedDrill.droneBuilt)
                {
                    price = 0;
                    currentRank = 0;
                    prefix = "Drone already built!";
                    suffix = "";
                    desc = "";
                    partsFound = false;
                }
                else
                {
                    price = 1000;
                    currentRank = 0;
                    prefix = "Purchase a Cargo drone that will retrieve cargo from your drill and deliver it to a forge.";
                    suffix = "";
                    desc = "";
                    partsFound = true;
                }
            }
        }
    }
    public void enableHover()
    {
        isHover = true;
    }
    public void disableHover()
    {
        isHover = false;
    }
}
