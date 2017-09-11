using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {
    [SerializeField]
    Text headerText;
    Text headerTextS;

    [SerializeField]
    GameObject LaserDrill;
    [SerializeField]
    GameObject Pick;
    [SerializeField]
    GameObject Inventory;
    [SerializeField]
    GameObject Oxygen;
    [SerializeField]
    GameObject Info;
    [SerializeField]
    Text Mats;

    RectTransform InfoRect;
    Text[] InfoText = new Text[3];
    Image InfoImage;

    [SerializeField]
    Color inactiveColor;
    [SerializeField]
    Color activeColor;
    [SerializeField]
    Color shadowColor;
    [SerializeField]
    Color infoActiveColor;
    [SerializeField]
    Color infoInactiveColor;



    GameObject currentHover;
    float fadeT;
    float positionT;
    float colorT;
    float heightT;

    bool hover;
    float infoPos;
    Button currentButton;

    List<GameObject> items = new List<GameObject>();
    List<ButtonValues> button = new List<ButtonValues>();
    List<Image> images = new List<Image>();
    List<Text> texts = new List<Text>();


    void Start () {
        fadeT = 0;
        positionT = 0;
        headerTextS = headerText.transform.parent.GetChild(0).GetComponent<Text>();

        InfoRect = Info.GetComponent<RectTransform>();

        for (int t = 0; t < InfoText.Length; t++)   // Info Texts //
        {
            InfoText[t] = Info.transform.GetChild(t).GetComponent<Text>();
        }
        InfoImage = Info.GetComponent<Image>();

        for (int i = 0; i < GetComponentsInChildren<ItemRotate>(true).Length; i++) // Items //
        {
            items.Add(GetComponentsInChildren<ItemRotate>(true)[i].gameObject);
        }
        for (int i = 0; i < GetComponentsInChildren<ButtonValues>().Length; i++) // Buttons //
        {
            button.Add(GetComponentsInChildren<ButtonValues>()[i]);
        }
        for (int i = 0; i < 4; i++) // Button Images //
        {
            if(GetComponentsInChildren<Image>()[i].transform.name != "Header")
            {
                images.Add(GetComponentsInChildren<Image>()[i]);
            }
        }
        for (int i = 0; i < 8; i++) //Button Texts //
        {
            if (GetComponentsInChildren<Text>()[i].transform.name != "Header")
            {
                texts.Add(GetComponentsInChildren<Text>()[i]);
            }
        }
    }
	
	void Update () {
        updateButtons();
        updateInfoPanel();
        Mats.text = "Material : " + PlayerClass.formatValue(PlayerClass.credits);

        if (PlayerClass.currentMenu == 2)
        {
            Inventory.SetActive(false);
            Oxygen.SetActive(false);
            Pick.SetActive(false);
        }
        else if (PlayerClass.currentMenu == 1)
        {
            Inventory.SetActive(true);
            Oxygen.SetActive(true);
            Pick.SetActive(true);
        }

        if (Info.transform.localPosition.x != infoPos)   // Change Position //
        {
            if (positionT < 1)
            {
                positionT += Time.deltaTime * 10;
            }

            Info.transform.localPosition = Vector3.Lerp(Info.transform.localPosition, new Vector3(infoPos, Info.transform.localPosition.y, Info.transform.localPosition.z), positionT);
        }
        
        if (InfoImage.color != Color.Lerp(infoActiveColor, infoInactiveColor, colorT))   // Change Color //
        {
            InfoImage.color = Color.Lerp(infoActiveColor, infoInactiveColor, colorT);
            for (int i = 0; i < InfoText.Length; i++)
            {
                InfoText[i].color = Color.Lerp(activeColor, inactiveColor, colorT);
            }
        }
    }

    public void HeaderButton()
    {
        if(PlayerClass.currentMenu == 1)
        {
            PlayerClass.currentMenu = 2;
            headerText.text = "Craft";
        }
        else
        {
            PlayerClass.currentMenu = 1;
            headerText.text = "Upgrade";
        }
        headerTextS.text = headerText.text;
    }
    void updateInfoPanel()
    {
        if (hover)
        {
            Info.SetActive(true);

            if (!currentButton.interactable && colorT < 1)
            {
                colorT += Time.deltaTime * 4;
            }
            else if (currentButton.interactable && colorT > 0)
            {
                colorT -= Time.deltaTime * 4;
            }

            if (heightT >= 1 && fadeT < 1)  // Fade In //
            {
                fadeT += Time.deltaTime * 2;
                for (int i = 0; i < InfoText.Length; i++)
                {
                    InfoText[i].GetComponent<CanvasRenderer>().SetAlpha(fadeT);
                }


            }
            if (heightT < 1)
            {
                heightT += Time.deltaTime * 6;
                InfoRect.sizeDelta = new Vector2(236.9f, Mathf.Lerp(0, 100, heightT));
            }
        }
        else
        {
            for (int i = 0; i < InfoText.Length; i++)
            {
                InfoText[i].GetComponent<CanvasRenderer>().SetAlpha(fadeT);
            }

            if (fadeT > 0)   // Fade Out //
            {
                fadeT -= Time.deltaTime * 10;
            }
            if (fadeT <= 0 && heightT > 0)
            {
                heightT -= Time.deltaTime * 6;
                InfoRect.sizeDelta = new Vector2(236.9f, Mathf.Lerp(0, 100, heightT));
            }
            else if (heightT <= 0)

            {
                Info.SetActive(false);
            }
        }
        hover = false;
    }

    void updateButtons()
    {
        for (int i = 0; i < button.Count; i++)
        {
            
            if (button[i].price > PlayerClass.credits || !button[i].partsFound)
            {
                button[i].button.interactable = false;
            }
            else
            {
                button[i].button.interactable = true;
            }

            if (!button[i].button.interactable)
            {
                Text buttonText = button[i].GetComponentInChildren<Text>();
                buttonText.color = inactiveColor;
            }
            else
            {
                Text buttonText = button[i].GetComponentInChildren<Text>();
                buttonText.color = activeColor;
            }

            if (button[i].isHover)
            {
                hover = true;
                currentButton = button[i].button;
                infoPos = button[i].transform.localPosition.x;
                positionT = 0;


                if (Input.GetKey(KeyCode.LeftShift))
                {
                    float totalValue = button[i].price;
                    int amountTimes = 0;
                    while (PlayerClass.credits > button[i].price * Mathf.Pow(1.15f, amountTimes) + totalValue && amountTimes < 9)
                    {
                        amountTimes++;
                        totalValue += button[i].price * Mathf.Pow(1.15f, amountTimes);
                    }
                    InfoText[0].text = "(" + (amountTimes + 1) + ") " + PlayerClass.formatValue(totalValue) + " material";
                }
                else
                {
                    InfoText[0].text = PlayerClass.formatValue(button[i].price) + " material";
                }
                if (PlayerClass.currentMenu == 1)
                {
                    InfoText[1].text = "Rank: " + button[i].currentRank.ToString() + "\nCurrent: " + PlayerClass.formatValue(button[i].currentValue);
                    InfoText[2].text = button[i].desc;
                }
                else if(PlayerClass.currentMenu == 2)
                {
                    InfoText[1].text = button[i].desc;
                    InfoText[2].text = "";
                }
                items[i].SetActive(true);
            }
            else
            {
                items[i].SetActive(false);
            }
        }
    }

    public void LaserDrillPress()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = PlayerClass.UpgradeCostLaserDrill;
            while (PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += PlayerClass.UpgradeCostLaserDrill * Mathf.Pow(1.15f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                purchaseUpgrade(PlayerClass.UpgradeCostLaserDrill, "Laser");
            }
        }
        else
        {
            purchaseUpgrade(PlayerClass.UpgradeCostLaserDrill, "Laser");
        }
    }
    public void PickPress()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = PlayerClass.UpgradeCostPick;
            while(PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += PlayerClass.UpgradeCostPick * Mathf.Pow(1.15f, amountPresses);
            }
                for (int i = 0; i < amountPresses; i++)
                {
                    purchaseUpgrade(PlayerClass.UpgradeCostPick, "Pick");
                }
        }else
        {
            purchaseUpgrade(PlayerClass.UpgradeCostPick, "Pick");
        }
    }
    public void OxygenPress()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = PlayerClass.UpgradeCostOxygen;
            while (PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += PlayerClass.UpgradeCostOxygen * Mathf.Pow(1.15f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                purchaseUpgrade(PlayerClass.UpgradeCostOxygen, "Oxygen");
            }
        }
        else
        {
            purchaseUpgrade(PlayerClass.UpgradeCostOxygen, "Oxygen");
        }
    }
    public void InventoryPress()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = PlayerClass.UpgradeCostInventory;
            while (PlayerClass.credits > totalValue && amountPresses < 10)
            {
                amountPresses++;
                totalValue += PlayerClass.UpgradeCostInventory * Mathf.Pow(1.15f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                purchaseUpgrade(PlayerClass.UpgradeCostInventory, "Inventory");
            }
        }
        else
        {
            purchaseUpgrade(PlayerClass.UpgradeCostInventory, "Inventory");
        }
    }

    public void ExitPress()
    {
        PlayerClass.menuActive = 0;
    }

    void purchaseUpgrade(float price, string upgradeItem)
    {


        if (PlayerClass.currentMenu == 1)
        {
            if (upgradeItem == "Pick")
            {
                PlayerClass.stonesPerHitPick += 10;
                PlayerClass.UpgradeCostPick *= 1.15f;
                PlayerClass.CurrentRankPick++;
            }
            else if (upgradeItem == "Laser")
            {
                PlayerClass.stonesPerHitLaser += 10;
                PlayerClass.UpgradeCostLaserDrill *= 1.15f;
                PlayerClass.CurrentRankLaserDrill++;
            }
            else if (upgradeItem == "Oxygen")
            {
                PlayerClass.EnergyMax += 50;
                PlayerClass.UpgradeCostOxygen *= 1.15f;
                PlayerClass.CurrentRankOxygen++;
            }
            else if (upgradeItem == "Inventory")
            {
                PlayerClass.inventorySize += 150;
                PlayerClass.UpgradeCostInventory *= 1.15f;
                PlayerClass.CurrentRankInventory++;
            }
        }
        else if(PlayerClass.currentMenu == 2)
        {
            if (upgradeItem == "Laser")
            {
                PlayerClass.laserDrillBuilt = true;
                price = 1000;
            }
        }
        PlayerClass.credits -= price;
    }
}