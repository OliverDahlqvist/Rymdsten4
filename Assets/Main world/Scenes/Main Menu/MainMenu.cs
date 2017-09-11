using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public Dropdown ResolutionMenu;
    public Slider MasterVolume;
    public Slider MusicVolume;
    public Slider SfxVolume;
    public Text StartButton;
    public GameObject MainPanel;
    public GameObject Player;
    public Camera MenuCamera;
    public Camera UICam;
    public GameObject rover;
    public AudioMixer Mixer; 
    


    private Resolution[] resolutions;
    private Scene CurrentScene;
    private string SceneName;
    private bool VisibleCursor = false;

    private string ResStrings;



    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {

        Mixer.SetFloat("Music", Mathf.Log10(MusicVolume.value<= 0 ? 0.001f : MusicVolume.value) * 40);
        Mixer.SetFloat("Sfx", Mathf.Log10(SfxVolume.value <= 0 ? 0.001f : SfxVolume.value) * 40);
        AudioListener.volume = MasterVolume.value;

        SceneManager.sceneLoaded += SetPlayer;
        resolutions = Screen.resolutions;
        CurrentScene = SceneManager.GetActiveScene();
        SceneName = CurrentScene.name;

        for (int i = 0; i < resolutions.Length; i++)
        {
            ResolutionMenu.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
        }

    }

    void Update()
    {

		CurrentScene = SceneManager.GetActiveScene();
		SceneName = CurrentScene.name;

        Debug.Log(SceneName);


     


        

		if (PlayerClass.activeMenu == false && Input.GetKeyDown(KeyCode.Tab) )
        {

            MenuCamera.enabled = true;
            MainPanel.SetActive(true);
            if (PlayerClass.inVehicle)
            {
                rover.SetActive(false);
            }
            rover.GetComponentInChildren<Camera>().enabled = false;
            UICam.enabled = false;

            VisibleCursor = true;
            Player.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
			PlayerClass.activeMenu = true; 


        }
		else if (PlayerClass.activeMenu == true && Input.GetKeyDown(KeyCode.Tab))
		{
			Cursor.lockState = CursorLockMode.Locked; 
			MainPanel.SetActive(false);

            
			VisibleCursor = false;
			PlayerClass.activeMenu = false;
            UICam.enabled = true;
            rover.GetComponentInChildren<Camera>().enabled = true;
            if (PlayerClass.inVehicle)
            {
                rover.SetActive(true);
            }
            else
            {
                Player.SetActive(true);
            }
        }


        if (SceneName == "MainMenu")
        {
            StartButton.text = "Start Game";
        }
        else
        {
            StartButton.text = "Resume Game";
        }
    }

    public string ResToString(Resolution res)
    {
        return res.width + " X " + res.height;
    }

    public void startGame()
    {
        if (SceneName == "MainMenu")
        {
            SceneManager.LoadScene("Main map");
            Player = GameObject.FindGameObjectWithTag("Player");
            MainPanel.SetActive(false);



        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            MainPanel.SetActive(false);
            Player.SetActive(true);
            VisibleCursor = false;
            PlayerClass.activeMenu = false;
            UICam.enabled = true;
        }
    }


    public void SetPlayer(Scene scene, LoadSceneMode loadscenemode)
    {

        SceneName = scene.name;
        if (SceneName == "Main map")
        {
			Player = GameObject.FindGameObjectWithTag("Player");
            MenuCamera = GameObject.Find("CameraHolder").GetComponent<Camera>();
            UICam = GameObject.Find("UiCamera").GetComponent<Camera>();
            rover = GameObject.FindGameObjectWithTag("Vehicle");
        }


    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void VolumeChange()
    {
        AudioListener.volume = MasterVolume.value;

    }

    public void MusicVolumeChange(float MusicVol)
    {
        Mixer.SetFloat("Music", Mathf.Log10(MusicVol <= 0 ? 0.001f : MusicVol) * 40);
    }
    public void SFXVolumeChange(float SfxVol)
    {
        Mixer.SetFloat("Sfx", Mathf.Log10(SfxVol <= 0 ? 0.001f : SfxVol) * 40);
        
    }
    public void SfxVolumeChangeSound()
    {
        SfxVolume.GetComponent<AudioSource>().Play();
    }

    public void ChangeResolution()
    {

        Screen.SetResolution(resolutions[ResolutionMenu.value].width, resolutions[ResolutionMenu.value].height, true);

    }


}