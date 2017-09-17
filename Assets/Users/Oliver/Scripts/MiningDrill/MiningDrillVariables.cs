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
/*	void Update () {
        if (ds.drillAmount >= ds.drillAmountMax)
        {
            if (colorT < 1) {
                colorT += Time.deltaTime * 6;
            }
            rend.materials[4].SetColor("_EmissionColor", Color.Lerp(notFull, full, colorT));
        }
        else
        {
            if (colorT > 0)
            {
                colorT -= Time.deltaTime * 6;
            }
            rend.materials[4].SetColor("_EmissionColor", Color.Lerp(notFull, full, colorT));
        }
    }*/
}
