using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{

    private float Oxygen;
    //private float Inventory;
    private float displayTime = 3;
    private string save = "Game Saved";
    private string load = "Game Loaded";
    private bool displaySave = false;
    private bool displayLoad = false;
    //private float Material; 



    // Use this for initialization
    void Start()
    {
        if (GlobalControl.Instance.IsSceneBeingLoaded)
        {
            PlayerState.Instance.localPlayerData = GlobalControl.Instance.LocalCopyOfData;

            transform.position = new Vector3(
                            GlobalControl.Instance.LocalCopyOfData.PositionX,
                            GlobalControl.Instance.LocalCopyOfData.PositionY,
                            GlobalControl.Instance.LocalCopyOfData.PositionZ + 0.1f);

            GlobalControl.Instance.IsSceneBeingLoaded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (displayTime >= 0)
            displayTime -= Time.deltaTime;
        if (displayTime <= 0.0)
        {
            displaySave = false;
            displayLoad = false;
        }

        
        //PlayerState.Instance.localPlayerData.amountStones = GameObject.Find("rock_01").GetComponent<Stone>().amountStones;
		//Oxygen = GameObject.FindWithTag("Player").GetComponent<EnergyUi>().CurrentEnergy;
		Oxygen = 100; //PlayerClass.CurrentEnergy;
		//Debug.Log ("Oxygen level at " + Oxygen);
        //Inventory = GameObject.Find("MainCamera").GetComponent<MineStone>().stones;
        //Material = GameObject.Find("MainCamera").GetComponent<MineStone>().credits;

        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKey(KeyCode.F5))
        {
            displaySave = true;
            displayTime = 3;
            PlayerState.Instance.localPlayerData.SceneID = SceneManager.GetActiveScene().buildIndex;
            PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
            PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
            PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;
            PlayerState.Instance.localPlayerData.inventory = GameObject.FindWithTag("MainCamera").GetComponent<MineStone>().stones;
			PlayerState.Instance.localPlayerData.Material = PlayerClass.credits; //GameObject.FindWithTag("MainCamera").GetComponent<MineStone>().credits;


            //PlayerState.Instance.localPlayerData.inventory = Inventory;
            //PlayerState.Instance.localPlayerData.Material = Material;


            GlobalControl.Instance.SaveData();
        }

        if (Input.GetKey(KeyCode.F9) || Oxygen <= 0)
        {
            displayLoad = true;
            displayTime = 3;
            GlobalControl.Instance.LoadData();
            GlobalControl.Instance.IsSceneBeingLoaded = true;

            int whichScene = GlobalControl.Instance.LocalCopyOfData.SceneID;
            SceneManager.LoadScene(whichScene);
        }

    }
    public void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "SaveZone")
        {
            displaySave = true;
            displayTime = 3;
            PlayerState.Instance.localPlayerData.SceneID = SceneManager.GetActiveScene().buildIndex;
            PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
            PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
            PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;
            //PlayerState.Instance.localPlayerData.inventory = Inventory;
            //PlayerState.Instance.localPlayerData.Material = Material;


            GlobalControl.Instance.SaveData();
        }

    }
    private void OnGUI()
    {
        if (displaySave)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), save);
        }
        if (displayLoad)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), load);
        }
    }
}

