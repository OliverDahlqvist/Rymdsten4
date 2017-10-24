using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrillVariables : MonoBehaviour {
    public string drillName;


    public Color notFull;
    public Color full;
    private float colorT;

    
    public AudioClip impactSound;

    void Start () {

        drillName = gameObject.name;

    }
}
