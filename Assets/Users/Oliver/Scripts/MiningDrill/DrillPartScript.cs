using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPartScript : MonoBehaviour {
    Animator anim;
    ParticleSystem ps;


    private float timestamp = 0f;
    private bool drillOn;
    public string drillName;

    public float speed;
    public float yield;

    //Upgrades
    public float[] price;
    public float[] upgrade;
    public int[] rank;

    void Start () {
        anim = GetComponent<Animator>();
        ps = GetComponentInParent<ParticleSystem>();
        drillOn = true;
        drillName = gameObject.transform.parent.name;

        price = new float[3] { 100, 10, 1000};
        upgrade = new float[3] { 1, 1, 10};
        rank = new int[3] { 1, 1, 1};
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
        float priceIncrement = 1.10f;

        switch (i)
        {
            case 0:
                upgrade[i] -= 0.1f;
                rank[i] += 1;
                break;
            case 1:
                upgrade[i] += 1;
                rank[i] += 1;
                break;
            case 2:
                upgrade[i] += 10;
                rank[i] += 1;
                priceIncrement = 2;
                break;
            default:
                Debug.Log("Error");
                break;
        }
        price[i] *= priceIncrement;
    }
}