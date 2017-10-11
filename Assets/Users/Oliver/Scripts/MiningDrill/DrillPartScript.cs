using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPartScript : MonoBehaviour {
    Animator anim;
    ParticleSystem ps;

    private float drillUpdateTime = 1f;
    private float timestamp = 0f;

    public bool drillOn;

    public string drillName;

    public float speed;
    public float yield;
    //Upgrades
    public float[] price;
    public float[] upgrade;

    public int rankSpeed;
    public int rankYield;


    //public Transform cargoEmitter;


    void Start () {
        anim = GetComponent<Animator>();
        ps = GetComponentInParent<ParticleSystem>();
        drillOn = true;
        drillName = gameObject.transform.parent.name;

        price = new float[2];
        upgrade = new float[2];

        price[0] = 100;
        price[1] = 10;
        upgrade[0] = 1;
        upgrade[1] = 1;

        speed = 1f;
        rankSpeed = 1;
        yield = 1;

        //Upgrades & Prices
    }

	void Update () {

        anim.SetBool("drillOn", drillOn);
        if (drillOn)
        {
            if (Time.time >= timestamp)
            {
                timestamp = Time.time + upgrade[0];

                PlayerClass.credits += upgrade[1];
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

    public void PurchaseUpgrade(int i)
    {
        PlayerClass.credits -= price[i];
        price[i] *= 1.10f;

        switch (i)
        {
            case 0:
                upgrade[i] -= 0.1f;
                break;
            case 1:
                upgrade[i] += 1;
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}