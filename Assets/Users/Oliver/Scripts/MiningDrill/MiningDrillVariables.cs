using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrillVariables : MonoBehaviour {
    public string drillName;
    private DrillPartScript ds;

    private Renderer rend;

    public Color notFull;
    public Color full;
    private float colorT;

    
    public AudioClip impactSound;

    void Start () {
        ds = GetComponentInChildren<DrillPartScript>();
        drillName = gameObject.name;
        rend = GetComponent<Renderer>();
    }
}
