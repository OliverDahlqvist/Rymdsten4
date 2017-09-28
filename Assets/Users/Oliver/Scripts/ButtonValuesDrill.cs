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

    void Start () {
        button = GetComponent<Button>();
        buttonName = transform.name;
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
