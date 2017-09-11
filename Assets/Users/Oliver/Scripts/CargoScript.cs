using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CargoScript : MonoBehaviour {
    NavMeshAgent agent;
    public GameObject dropOff;
    public GameObject drill;
    ForgeAnimationScript forgeAnimation;
    DrillPartScript drillScript;
    Animator anim;
    [SerializeField]
    Transform player;
    Text notif;
    Transform canvas;


    float inventoryMax;
    float inventory;

    public bool droppingOff = false;
    bool isNear = false;

    float distanceToTarget;


    void Start () {
        agent = GetComponent<NavMeshAgent>();
        inventoryMax = 100;
        inventory = 0;
        forgeAnimation = dropOff.GetComponent<ForgeAnimationScript>();
        drillScript = drill.GetComponentInChildren<DrillPartScript>();
        anim = GetComponentsInChildren<Animator>()[1];
        agent.destination = drill.transform.position;
        canvas = transform.GetChild(0).transform.GetChild(0).transform;
        notif = GetComponentInChildren<Text>();
    }
	
	void Update () {
		if(inventory >= inventoryMax && droppingOff == false)
        {
            agent.destination = dropOff.transform.position;
            droppingOff = true;
        }

        if (droppingOff)
        {
            distanceToTarget = Vector3.Distance(dropOff.transform.position, transform.position);
        }
        else
        {
            distanceToTarget = Vector3.Distance(drill.transform.position, transform.position);
        }

        if(distanceToTarget <= 15 && !PlayerClass.usingForge && droppingOff)
        {
            forgeAnimation.openDoor = true;
            PlayerClass.credits += inventory;
            inventory = 0;
            droppingOff = false;
            agent.destination = drill.transform.position;
        }
        else if(distanceToTarget <= 15 && !droppingOff)
        {
            inventory += drillScript.drillAmount;
            drillScript.drillAmount = 0;
        }

        if (isNear)
        {
            notif.text = PlayerClass.formatValue(inventory) + "/" + PlayerClass.formatValue(inventoryMax);
            canvas.rotation = Quaternion.LookRotation(transform.position - player.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            anim.SetBool("isNear", isNear);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            anim.SetBool("isNear", isNear);
        }
    }
}
