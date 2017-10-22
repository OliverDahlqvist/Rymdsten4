using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValues : MonoBehaviour {
    public float price;
    public string desc;
    public int currentRank;
    public float currentValue;
    public bool partsFound;
    public Button button;
    public bool isHover;
    private UpgradeMenu upgradeScript;
    private string buttonName;

    void Start () {
        button = GetComponent<Button>();
        buttonName = transform.name;
	}

	void Update () {
        if (PlayerClass.currentMenu == 1)
        {
            if (buttonName == "Pick")
            {
                price = PlayerClass.UpgradeCostPick;
                currentRank = PlayerClass.CurrentRankPick;
                currentValue = PlayerClass.stonesPerHitPick;
                desc = "+10 stones/hit";
                partsFound = true;
            }
            else if (buttonName == "Inventory")
            {
                price = PlayerClass.UpgradeCostInventory;
                currentRank = PlayerClass.CurrentRankInventory;
                currentValue = PlayerClass.inventorySize;
                desc = "+100 inventory";
                partsFound = true;
            }
            else if (buttonName == "Oxygen")
            {
                price = PlayerClass.UpgradeCostOxygen;
                currentRank = PlayerClass.CurrentRankOxygen;
                currentValue = PlayerClass.EnergyMax;
                desc = "+100 oxygen";
                partsFound = true;
            }
            else if (buttonName == "LaserDrill")
            {
                price = PlayerClass.UpgradeCostLaserDrill;
                currentRank = PlayerClass.CurrentRankLaserDrill;
                currentValue = PlayerClass.stonesPerHitLaser;
                desc = "+10 stones/hit";
                partsFound = PlayerClass.laserDrillBuilt;
            }
        }
        else if(PlayerClass.currentMenu == 2)
        {
            if(transform.name == "LaserDrill")
            {
                if (!PlayerClass.laserDrillBuilt)
                {
                    price = 1000;
                    desc = "Mining laser used for more\nefficient mining";
                    //partsFound = PlayerClass.laserDrillFound;
                }
                else
                {
                    price = 0;
                    desc = "Mining laser already built!";
                    partsFound = false;
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
