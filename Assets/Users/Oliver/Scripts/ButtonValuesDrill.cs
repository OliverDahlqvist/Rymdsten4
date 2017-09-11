using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValuesDrill : MonoBehaviour {
    public float price;
    public string desc;
    public int currentRank;
    public float currentValue;
    public bool partsFound;
    public Button button;
    public bool isHover;
    [SerializeField]
    MiningdrillUpgradeMenu upgradeScript;
    private string buttonName;

    void Start () {
        button = GetComponent<Button>();
        buttonName = transform.name;
    }
	void Update () {
        if (buttonName == "Efficiency")
        {
            price = upgradeScript.selectedDrill.upgradeCost;
            currentRank = upgradeScript.selectedDrill.currentRank;
            desc = "+1 Ore/Tick";
            partsFound = true;
        }
        else if (buttonName == "Tier")
        {
            price = PlayerClass.UpgradeCostInventory;
            currentRank = PlayerClass.CurrentRankInventory;
            currentValue = PlayerClass.inventorySize;
            desc = "Not Implemented Yet";
            partsFound = true;
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
