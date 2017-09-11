using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public Transform playerPosition;

    public PlayerStatistics localPlayerData = new PlayerStatistics();

    //private float Oxygen; 
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        //GlobalControl.Instance.Player = gameObject;
    }

    //At start, load data from GlobalControl.
    void Start()
    {
        localPlayerData = GlobalControl.Instance.savedPlayerData;
    }

    void Update()
    {

    }

}