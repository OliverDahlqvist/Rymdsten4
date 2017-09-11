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
    GameObject Info;

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
    List<ButtonValuesDrill> button = new List<ButtonValuesDrill>();
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
        for (int i = 0; i < 4; i++) // Button Images //
        {
            if (GetComponentsInChildren<Image>()[i].transform.name != "Header")
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
        stats.text = "Tick Rate: " + PlayerClass.globalTickRate * 60 + "\nOre/Tick: " + selectedDrill.orePerTick + "\nOre/Sec: " 
            + selectedDrill.orePerTick / PlayerClass.globalTickRate + "\nOre/Min: " 
            + selectedDrill.orePerTick / PlayerClass.globalTickRate * 60 + "\nOre/Hour: " 
            + (selectedDrill.orePerTick / PlayerClass.globalTickRate) * 60 * 60;
        mats.text = "Material : " + PlayerClass.formatValue(PlayerClass.credits);

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
                    InfoText[1].text = "Rank: " + button[i].currentRank.ToString() + "\nCurrent: " + PlayerClass.formatValue(selectedDrill.orePerTick);
                    InfoText[2].text = button[i].desc;
                }
                else if (PlayerClass.currentMenu == 2)
                {
                    InfoText[1].text = button[i].desc;
                    InfoText[2].text = "";
                }
            }
        }
    }

    public void setSelectedName()
    {
        nameText.text = selectedDrill.drillName;
    }

    public void updateName()
    {
        selectedDrill.drillName = nameText.text;
    }

    public void ExitPress()
    {
        PlayerClass.menuActive = 0;
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

    void purchaseUpgrade(float price, string upgradeItem)
    {

        if (upgradeItem == "Efficiency")
        {
            selectedDrill.orePerTick += 1;
            selectedDrill.upgradeCost *= 1.15f;
            selectedDrill.currentRank++;
        }
        PlayerClass.credits -= price;
    }
}

