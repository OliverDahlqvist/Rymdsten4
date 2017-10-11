using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningdrillUpgradeMenu : MonoBehaviour {
    [HideInInspector]
    public string drillName;

    [SerializeField]
    private InputField nameText;

    [SerializeField]
    private Text material;
    [SerializeField]
    private Text price;
    [SerializeField]
    private Text description;

    private AudioSource sound;

    public ButtonDrill currentButton;
    

    [HideInInspector]
    public DrillPartScript selectedDrill;

    void Start () {
        
    }
	
	void Update () {
        material.text = "Material_/" + PlayerClass.formatValue(PlayerClass.credits) + "/";

        if (currentButton != null)
        {
            price.text = PlayerClass.formatValue(selectedDrill.price[(int)currentButton.buttonIndex]);
            description.text = currentButton.description;
        }
    }

    public void setSelectedName()
    {
        nameText.text = selectedDrill.drillName;
    }

    public void updateName()
    {
        selectedDrill.drillName = nameText.text;
        sound.Play();
    }

    public void ExitPress()
    {
        PlayerClass.menuActive = 0;
    }

    public void SetCurrentButton(ButtonDrill button)
    {
        currentButton = button;
    }

    /*public void BuildDronePress()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = selectedDrill.upgradeCost;
            while (PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += selectedDrill.upgradeCost * Mathf.Pow(1.15f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                purchaseUpgrade(selectedDrill.upgradeCost, "Efficiency");
            }
        }
        else
        {
            purchaseUpgrade(selectedDrill.upgradeCost, "Efficiency");
        }
    }*/

    /*public void EfficiencyPress()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = selectedDrill.upgradeCost;
            while (PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += selectedDrill.upgradeCost * Mathf.Pow(1.15f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                purchaseUpgrade(selectedDrill.upgradeCost, "Efficiency");
            }
        }
        else
        {
            purchaseUpgrade(selectedDrill.upgradeCost, "Efficiency");
        }
    }*/
    /*public void droneCargoPress()
    {
        if(PlayerClass.currentMenu == 1)
        {
            pressPurchase(selectedDrill.upgradeCostCargo, "Cargo");
        }
        else if (PlayerClass.currentMenu == 2)
        {
            pressPurchase(selectedDrill.droneCargoCost, "Cargo");
        }
    }*/
    /*public void purchaseDronePress()
    {
        purchaseUpgrade(1000, "PurchaseDrone");
    }*/

    /*void pressPurchase(float price, string item)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = price;
            while (PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += price * Mathf.Pow(1.15f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                purchaseUpgrade(price, item);
            }
        }
        else
        {
            purchaseUpgrade(price, item);
        }
    }*/

    //Functionality
    /*void purchaseUpgrade(float price, string upgradeItem)
    {
        if (PlayerClass.currentMenu == 1)
        {
            if (upgradeItem == "Efficiency")
            {
                selectedDrill.orePerTick += 1;
                selectedDrill.upgradeCost *= 1.15f;
                selectedDrill.currentRank++;
            }
            else if(upgradeItem == "Cargo")
            {
                selectedDrill.drillAmountMax += 100;
                selectedDrill.upgradeCostCargo *= 1.15f;
                selectedDrill.cargoRank++;
            }
        }
        PlayerClass.credits -= price;
    }*/
    
    public void OnHover(ButtonDrill buttonValues)
    {
        Debug.Log("Hover");
        //mats.text = "Material : " + PlayerClass.formatValue(PlayerClass.credits);
    }
    public void OnExit()
    {
    }
}