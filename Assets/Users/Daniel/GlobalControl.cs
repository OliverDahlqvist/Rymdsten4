using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    public GameObject Player;

    public Transform TransitionTarget;

    public List<SavedList> SavedLists; // = new List<SavedList>();

    public delegate void SaveDelegate(object sender, EventArgs args);

    public static event SaveDelegate SaveEvent;

    void Awake()
    {
        Application.targetFrameRate = 144;

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (TransitionTarget == null)
            TransitionTarget = gameObject.transform;

    }

    public void InitializeSceneList()
    {
        if (SavedLists == null)
        {
            print("Saved lists was null");
            SavedLists = new List<SavedList>();
        }

        bool found = false;

        //We need to find if we already have a list of saved items for this level:
        for (int i = 0; i < SavedLists.Count; i++)
        {
            if (SavedLists[i].SceneID == SceneManager.GetActiveScene().buildIndex)
            {
                found = true;
                print("Scene was found in saved lists!");
            }
        }

        //If not, we need to create it:
        if (!found)
        {
            SavedList newList = new SavedList(SceneManager.GetActiveScene().buildIndex);
            SavedLists.Add(newList);

            print("Created new list!");
        }
    }

    public SavedList GetListForScene()
    {
        for (int i = 0; i < SavedLists.Count; i++)
        {
            if (SavedLists[i].SceneID == SceneManager.GetActiveScene().buildIndex)
                return SavedLists[i];
        }
        return null;
    }

    public PlayerStatistics LocalCopyOfData;
    public bool IsSceneBeingLoaded = false;
    public bool IsSceneBeingTransitioned = false;

    public void FireSaveEvent()
    {
        GetListForScene().SavedRocks = new List<SavedDestuctrableRocks>();
        //If we have any functions in the event:
        if (SaveEvent != null)
            SaveEvent(null, null);
    }

    public void SaveData()
    {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        FireSaveEvent();

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");
        FileStream SaveObjects = File.Create("saves/saveObjects.binary");

        LocalCopyOfData = PlayerState.Instance.localPlayerData;

        formatter.Serialize(saveFile, LocalCopyOfData);
        formatter.Serialize(SaveObjects, SavedLists);

        saveFile.Close();
        SaveObjects.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
        FileStream saveObjects = File.Open("Saves/saveObjects.binary", FileMode.Open);

        LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(saveFile);
        SavedLists = (List<SavedList>)formatter.Deserialize(saveObjects);

        saveFile.Close();
        saveObjects.Close();
    }
}