using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningdrillUpgradeMenu : MonoBehaviour {
    [HideInInspector]
    public string drillName;
    [SerializeField]
    InputField nameText;
    [SerializeField]
    Text stats;
    [SerializeField]
    Text mats;
    [SerializeField]
    Text cargoText;
    [SerializeField]
    Text cargoDroneText;
    [SerializeField]
    GameObject Info;
    [SerializeField]
    GameObject Efficiency;
    [SerializeField]
    ButtonValuesDrill PurchaseDrone;
    [SerializeField]
    GameObject cargoDrone;
    [SerializeField]
    GameObject cargo;
    [SerializeField]
    AudioSource sound;
    [SerializeField]
    Image switchIcon;
    [SerializeField]
    Sprite droneIcon;
    [SerializeField]
    Sprite drillIcon;


    Text[] InfoText = new Text[3];
    RectTransform InfoRect;

    public DrillPartScript selectedDrill;

    GameObject currentHover;
    float fadeT;
    float positionT;
    float colorT;
    float heightT;

    bool hover;
    float infoPos;
    Button currentButton;
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

    List<GameObject> items = new List<GameObject>();
    public List<ButtonValuesDrill> button = new List<ButtonValuesDrill>();
    List<Image> images = new List<Image>();
    List<Text> texts = new List<Text>();

    void Start () {
        fadeT = 0;
        positionT = 0;
        colorT = 0;

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
        for (int i = 0; i < GetComponentsInChildren<ButtonValuesDrill>().Length; i++) // Buttons //
        {
            button.Add(GetComponentsInChildren<ButtonValuesDrill>()[i]);
        }
    }
	
	void Update () {
        if (PlayerClass.currentMenu == 2)
        {
            switchIcon.sprite = drillIcon;
            Efficiency.SetActive(false);
            if (!selectedDrill.droneBuilt)
            {
                PurchaseDrone.transform.gameObject.SetActive(true);
                nameText.transform.gameObject.SetActive(false);
                cargo.SetActive(false);
            }
            else
            {
                nameText.transform.gameObject.SetActive(true);

                //PurchaseDrone.transform.gameObject.SetActive(false);

                cargo.SetActive(true);
            }
        }
        else if (PlayerClass.currentMenu == 1)
        {
            switchIcon.sprite = droneIcon;
            Efficiency.SetActive(true);
            PurchaseDrone.transform.gameObject.SetActive(false);
            nameText.transform.gameObject.SetActive(true);
            cargo.SetActive(true);
        }

        updateButtons();
        updateInfoPanel();

        stats.text = "Tick Rate: " + PlayerClass.globalTickRate * 60 + "\nOre/Tick: " + selectedDrill.orePerTick + "\nOre/Sec: " 
            + selectedDrill.orePerTick / PlayerClass.globalTickRate + "\nOre/Min: " 
            + selectedDrill.orePerTick / PlayerClass.globalTickRate * 60 + "\nOre/Hour: " 
            + (selectedDrill.orePerTick / PlayerClass.globalTickRate) * 60 * 60;

        mats.text = "Material : " + PlayerClass.formatValue(PlayerClass.credits);

        cargoText.text = selectedDrill.drillAmount + "/" + selectedDrill.drillAmountMax;

        if (selectedDrill.droneBuilt)
        {
            cargoDroneText.text = selectedDrill.droneCargoScript.inventory + "/" + selectedDrill.droneCargoScript.inventoryMax;
        }
        else if(cargoDroneText.gameObject.activeSelf)
        {
            cargoDroneText.gameObject.SetActive(false);
        }

        if(selectedDrill.droneBuilt && !cargoDroneText.gameObject.activeSelf)
        {
            cargoDroneText.gameObject.SetActive(true);
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
                if (button[i].GetComponentInChildren<Text>())
                {
                    Text buttonText = button[i].GetComponentInChildren<Text>();
                    buttonText.color = inactiveColor;
                }
                else
                {
                    Image buttonImage = button[i].GetComponentInChildren<Image>();
                    buttonImage.color = inactiveColor;
                }
            }
            else
            {
                if (button[i].GetComponentInChildren<Text>())
                {
                    Text buttonText = button[i].GetComponentInChildren<Text>();
                    buttonText.color = activeColor;
                }
                else
                {
                    Image buttonImage = button[i].GetComponentInChildren<Image>();
                    buttonImage.color = activeColor;
                }
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
                else if(button[i].price > 0)
                {
                    InfoText[0].text = PlayerClass.formatValue(button[i].price) + " material";
                }
                else
                {
                    InfoText[0].text = "Already purchased";
                }

                if (button[i].prefix != "" || button[i].suffix != "")
                {
                    if(button[i].currentRank > 0)
                        InfoText[1].text = button[i].prefix + button[i].currentRank.ToString() + "\n" + button[i].suffix + PlayerClass.formatValue(button[i].currentValue);
                    else
                        InfoText[1].text = button[i].prefix + "\n" + button[i].suffix;
                }
                else
                {
                    InfoText[1].text = "Rank: " + button[i].currentRank.ToString();
                }
                InfoText[2].text = button[i].desc;
            }
        }
    }

    public void setSelectedName()
    {
        if (PlayerClass.currentMenu == 1)
        {
            nameText.text = selectedDrill.drillName;
        }
        else
        {
            nameText.text = selectedDrill.droneName;
        }
    }

    public void updateName()
    {
        if (PlayerClass.currentMenu == 1)
        {
            selectedDrill.drillName = nameText.text;
        }
        else
        {
            selectedDrill.droneName = nameText.text;
        }
        sound.Play();
    }

    public void ExitPress()
    {
        PlayerClass.menuActive = 0;
    }

    public void BuildDronePress()
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
    }

    public void EfficiencyPress()
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
    }
    public void droneCargoPress()
    {
        if(PlayerClass.currentMenu == 1)
        {
            pressPurchase(selectedDrill.upgradeCostCargo, "Cargo");
        }
        else if (PlayerClass.currentMenu == 2)
        {
            pressPurchase(selectedDrill.droneCargoCost, "Cargo");
        }
    }
    public void purchaseDronePress()
    {
        purchaseUpgrade(1000, "PurchaseDrone");
    }

    void pressPurchase(float price, string item)
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
    }

    //Functionality
    void purchaseUpgrade(float price, string upgradeItem)
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
        else if(PlayerClass.currentMenu == 2)
        {
            if(upgradeItem == "Cargo")
            {
                selectedDrill.droneCargoScript.inventoryMax += 100;
                selectedDrill.droneCargoCost *= 1.15f;
                selectedDrill.droneCargoRank++;
            }
            if(upgradeItem == "PurchaseDrone")
            {
                GameObject builtDrone = Instantiate(cargoDrone, selectedDrill.transform.position, Quaternion.identity);
                selectedDrill.droneCargoScript = builtDrone.GetComponent<CargoScript>();
                selectedDrill.droneCargoScript.drill = selectedDrill.transform.root.gameObject;
                selectedDrill.droneBuilt = true;
                PlayerClass.amountCargoDrones++;
                selectedDrill.droneName = "Cargo Drone " + PlayerClass.amountCargoDrones;
                setSelectedName();
                button[button.Count - 1].isHover = false;
                PurchaseDrone.transform.gameObject.SetActive(false);
                //PlayerClass.currentMenu = 1;
            }
        }
        PlayerClass.credits -= price;
    }

    public void HeaderButton()
    {
        if (PlayerClass.currentMenu == 1)
        {
            PlayerClass.currentMenu = 2;
            nameText.text = selectedDrill.droneName;
        }
        else if(PlayerClass.currentMenu == 2)
        {
            PlayerClass.currentMenu = 1;
            nameText.text = selectedDrill.drillName;
        }
    }
}