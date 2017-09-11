using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPartScript : MonoBehaviour {
    Animator anim;
    ParticleSystem ps;

    [SerializeField]
    private int drillMultiplier;
    private int drillBaseValue;
    private float drillUpdateTime = 1f;
    private float timestamp = 0f;
    private float powerInput;

    public bool drillOn;

    public string drillName;

    //Upgrades
    public int orePerTick;
    public float upEfficiency;
    public int currentRank;
    public float upgradeCost;

    void Start () {
        anim = GetComponent<Animator>();
        ps = GetComponentInParent<ParticleSystem>();
        drillBaseValue = 10;
        drillOn = true;
        powerInput = 100;
        drillName = gameObject.transform.parent.name;

        //Upgrades
        orePerTick = 1;
        upEfficiency = 150;
        currentRank = 1;
        upgradeCost = 100;
    }

	void Update () {

        anim.SetBool("drillOn", drillOn);

        if (drillOn && ps.isStopped)
        {
            ps.Play();
        }
        else if(!drillOn && ps.isPlaying)
        {
            ps.Stop();
        }
	}
}
