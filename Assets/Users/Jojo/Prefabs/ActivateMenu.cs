using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ActivateMenu : MonoBehaviour
{

    public GameObject Player;
    public Camera MainCamera;
    public Camera UppgradeCamera;
    public Camera MapCamera;
    public GameObject PlayerUi;
    public Text UpgradeFeedback;
    public GameObject Fudge;
    public GameObject SpaceShip;
    public GameObject Ui;
    public Text credits;
    bool showText;
    float textValue;
    Color feedbackColor;
    //public string OxygenCostString = PlayerClass.UpgradeCostOxygen.ToString();
    //public string PickaxeCostString = PlayerClass.UpgradeCostPick.ToString();
    //public string BackpackCostString = PlayerClass.UpgradeCostInventory.ToString();



    private bool MapActive = false;




    // Use this for initialization
    void Start()
    {
        feedbackColor = new Color(0, 1, 0, 1);
        Player = GameObject.FindGameObjectWithTag("Player");
        Fudge = GameObject.FindGameObjectWithTag("MainCamera");
        Ui = GameObject.FindGameObjectWithTag("UICamera");


    }

    // Update is called once per frame
    void Update()
    {
        if (showText)
        {
            textValue += Time.deltaTime;
            if (textValue >= 1)
            {
                showText = false;
            }
        }
        if (!showText && textValue > 0)
        {
            textValue -= Time.deltaTime;
        }
        if (textValue < 0)
        {
            textValue = 0;
        }

        if (PlayerClass.textValue >= 0 || PlayerClass.textValue <= 0)
        {
            feedbackColor.a = Mathf.Lerp(0, 1, textValue);

            UpgradeFeedback.color = feedbackColor;
        }

        credits.text = Mathf.Floor(PlayerClass.credits).ToString();


        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {

            if (hit.collider.gameObject.tag == "Upgrade" && Input.GetKeyDown(KeyCode.E))
            {
                UppgradeCamera.enabled = true;
                Player.gameObject.SetActive(false);
                PlayerUi.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UppgradeCamera.GetComponent<AudioListener>().enabled = true;

            }
        }

        //if (Input.GetKeyDown(KeyCode.M) && !MapActive)
        //{
        //    MainCamera.enabled = false;
        //    MapCamera.enabled = true;
        //    PlayerUi.SetActive(false);
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    MapActive = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.M) && MapActive)
        //{
        //    MapCamera.enabled = false;
        //    MainCamera.enabled = true;
        //    Player.gameObject.SetActive(true);
        //    PlayerUi.SetActive(true);
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //    MapActive = false;
        //    Debug.Log("Fail");
        //}



    }

    public void ExitGame()
    {
        UppgradeCamera.enabled = false;
        MainCamera.enabled = true;
        Player.gameObject.SetActive(true);
        PlayerUi.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Fail");
        UppgradeCamera.GetComponent<AudioListener>().enabled = false;

    }

    public void UpgradePickAxe()
    {
        if (PlayerClass.credits >= PlayerClass.UpgradeCostPick)
        {
            PlayerClass.stonesPerHitPick += 10;
            PlayerClass.credits -= PlayerClass.UpgradeCostPick;
            PlayerClass.UpgradeCostPick *= 1.2f;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Upgrade successful!";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "Not enough credits";
            showText = true;
        }
    }
    public void UpgradeLaser()
    {

        if (PlayerClass.credits >= PlayerClass.UpgradeCostLaserDrill && PlayerClass.laserDrillBuilt)
        {
            PlayerClass.stonesPerHitLaser += 10;
            PlayerClass.credits -= PlayerClass.UpgradeCostLaserDrill;
            PlayerClass.UpgradeCostLaserDrill *= 1.4f;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Upgrade successful!";
            showText = true;
        }
        else if (!PlayerClass.laserDrillBuilt)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You don't have the drill yet";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You can't afford that!";
            showText = true;
        }

    }
    public void UpgradeBackpack()
    {
        if (PlayerClass.credits >= PlayerClass.UpgradeCostInventory)
        {
            PlayerClass.inventorySize += 200;
            PlayerClass.credits -= PlayerClass.UpgradeCostInventory;
            PlayerClass.UpgradeCostInventory *= 1.2f;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Upgrade successful!";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "Not enough credits";
            showText = true;
        }

    }

    public void UpgradeOxygenTank()
    {
        if (PlayerClass.credits >= PlayerClass.UpgradeCostOxygen)
        {
            PlayerClass.EnergyMax += 50;
            PlayerClass.credits -= PlayerClass.UpgradeCostOxygen;
            PlayerClass.UpgradeCostOxygen *= 1.5f;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Upgrade successful!";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "Not enough credits";
            showText = true;
        }
    }
    public void CraftJetpack()
    {
        if (PlayerClass.JetpackBuilt)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You've already bought that";
            showText = true;
        }
        else if (PlayerClass.jetPackFound && PlayerClass.credits > 10)
        {
            PlayerClass.JetpackBuilt = true;
            PlayerClass.credits -= 500;
            UpgradeFeedback.text = "You have repaired the jetpack, press J to enable it ";
            showText = true;
            feedbackColor = Color.green;
        }
        else if (PlayerClass.jetPackFound && PlayerClass.credits < 10)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "Not enough cash ";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You need a repair part for that";
            showText = true;
        }

    }
    public void CraftWarmsuit()
    {
        if (PlayerClass.WarmSuit)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You've already bought that";
            showText = true;
        }
        else if (PlayerClass.WarmSuitUpgrade && PlayerClass.credits > 10)
        {
            PlayerClass.WarmSuit = true;
            PlayerClass.credits -= 10;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Your suit has now been upgraded to handle extreme temperatures ";
            showText = true;

        }
        else if (PlayerClass.WarmSuit && PlayerClass.credits < 10)
        {
            UpgradeFeedback.text = "Not enough cash ";
            showText = true;
        }

        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You need a repair part for that!";
            showText = true;
        }
    }

    public void SuperOxygen()
    {
        if (PlayerClass.credits >= 100000f)
        {
            PlayerClass.EnergyMax += 5000;
            PlayerClass.credits -= 100000f;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Your oxygen supply is a bit too large";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "Like you could afford that";
            showText = true;
        }

    }
    public void SunEater()
    {
        if (PlayerClass.SunEaterBought)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You've already bought that";
            showText = true;
        }
        else if (PlayerClass.credits >= 100000 && !PlayerClass.SunEaterBought)
        {
            PlayerClass.SunEaterBought = true;
            PlayerClass.credits -= 100000;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "The mightiest pickaxe is yours, press '3' to equip it";
            showText = true;
        }
        else if (PlayerClass.credits < 100000)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You can't afford that, you're too poor";
            showText = true;
        }

    }

    public void BottomlessBag()
    {
        if (PlayerClass.credits >= 100000)
        {
            PlayerClass.inventorySize += 100000;
            PlayerClass.credits -= 100000;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Your backpack can swallow suns";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "Get some more cash and come back later";
            showText = true;
        }

    }
    public void BuildLaserDrill()
    {
        if (PlayerClass.credits >= 500 && PlayerClass.laserDrillFound && !PlayerClass.laserDrillBuilt)
        {

            PlayerClass.credits -= 500;
            PlayerClass.laserDrillBuilt = true;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "Laserdrill built, press '2' to equip it";
            showText = true;
        }
        else if (!PlayerClass.laserDrillFound)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You need a repair part for that";
            showText = true;
        }
        else if (PlayerClass.credits < 500)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You can't afford that";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You've already built that";
            showText = true;

        }

    }
    public void RepairArray()
    {
        if (PlayerClass.ComRepairPart >= 4 && PlayerClass.credits >= 0)
        {
            SceneManager.LoadScene("RescueEndScene");
        }
        else if (PlayerClass.ComRepairPart < 4)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You need " + (4 - PlayerClass.ComRepairPart) + " more repair parts ";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You need more cash";
            showText = true;
        }
    }

    public void UpgradeSpeed()
    {
        if (PlayerClass.credits >= PlayerClass.UpgradeCostSpeed)
        {
            Player.GetComponent<Movement>().runSpeed += 5;
            PlayerClass.credits -= PlayerClass.UpgradeCostSpeed; 
            PlayerClass.UpgradeCostSpeed *= 100;
            
            feedbackColor = Color.green;
            UpgradeFeedback.text = "You can now run faster ";
            showText = true;
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You don't have enough cash";
            showText = true;
        }
    }

    public void BuildSpaceShip()
    {
        if (PlayerClass.credits >= 10000)
        {
            SceneManager.LoadScene("RescueEndScene");
        }
        else
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You don't have enough money!";
            showText = true;
        }
    }
    public void RepairCar()
    {
        if (PlayerClass.CarRepaired)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You've already bought that";
            showText = true;
        }
        else if (PlayerClass.credits >= 1000 && PlayerClass.CarRepairPartFound)
        {
            PlayerClass.credits -= 1000;
            PlayerClass.CarRepaired = true;
            feedbackColor = Color.green;
            UpgradeFeedback.text = "You can now use the car by walking up to it and pressing 'E'";
            showText = true;
        }
        else if (!PlayerClass.CarRepairPartFound)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You need to find a repair part first";
            showText = true;
        }
        else if (PlayerClass.CarRepairPartFound && PlayerClass.credits < 1000)
        {
            feedbackColor = Color.red;
            UpgradeFeedback.text = "You don't have enough cash";
            showText = true;
        }

    }


    public void UpgradeForge()
    {
        if (PlayerClass.credits >= 10)
        {
            PlayerClass.credits -= 10;
            PlayerClass.forgeEfficency = 2;
        }
    }

}
