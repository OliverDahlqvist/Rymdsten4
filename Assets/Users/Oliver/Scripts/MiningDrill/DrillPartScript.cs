using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPartScript : MonoBehaviour {
    Animator anim;
    ParticleSystem ps;

    [SerializeField]
    GameObject stoneParticle;

    public CargoScript droneCargoScript;

    [SerializeField]
    private int drillMultiplier;
    private int drillBaseValue;
    private float drillUpdateTime = 1f;
    private float timestamp = 0f;
    public float drillAmount;
    public float drillAmountMax;
    public float drillTickRate;

    public bool drillOn;
    public bool cargoParticles;
    public Transform targetDrill;

    public string drillName;
    public string droneName;

    //Upgrades
    public int orePerTick;
    public float upEfficiency;


    public int currentRank;
    public float upgradeCost;


    public float droneCost;

    public int cargoRank;
    public float upgradeCostCargo;

    public float droneCargoCost;
    public int droneCargoRank;

    public bool droneBuilt;



    //public Transform cargoEmitter;


    void Start () {
        anim = GetComponent<Animator>();
        ps = GetComponentInParent<ParticleSystem>();
        drillBaseValue = 10;
        drillOn = true;
        drillName = gameObject.transform.parent.name;
        droneName = "Cargo Drone 01";
        drillAmount = 0;
        drillAmountMax = 100;
        drillTickRate = 1;

        if (droneCargoScript == null)
        {
            droneBuilt = false;
        }
        else
        {
            droneBuilt = true;
        }

        cargoParticles = false;

        //Upgrades & Prices
        orePerTick = 1;
        upEfficiency = 150;

        currentRank = 1;
        upgradeCost = 100;

        droneCost = 20;

        cargoRank = 1;
        upgradeCostCargo = 100;

        droneCargoRank = 1;
        droneCargoCost = 100;

    }

	void Update () {

        anim.SetBool("drillOn", drillOn);

        if (Time.time >= timestamp)
        {
            timestamp = Time.time + drillTickRate;
            if (drillOn && drillAmount + orePerTick < drillAmountMax)
            {
                drillAmount += orePerTick;
            }
            else if(drillOn)
            {
                drillAmount = drillAmountMax;
            }
            if (cargoParticles)
            {
                Instantiate(stoneParticle, transform.position, Quaternion.Euler(-90, 0, 0)).GetComponent<FollowObject>().target = targetDrill.GetChild(0).GetChild(0);
            }
           
        }

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