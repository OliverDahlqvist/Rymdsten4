using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CargoScript : MonoBehaviour {
    NavMeshAgent agent;
    public GameObject dropOff;
    public GameObject drill;


    Animator anim;
    [SerializeField]
    Transform player;
    Text notif;
    Transform canvas;


    public float inventoryMax;
    public float inventory;

    public bool droppingOff;
    bool isNear = false;

    float distanceToTarget;


    void Start () {
        agent = GetComponent<NavMeshAgent>();
        inventoryMax = 100;
        inventory = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        dropOff = GameObject.FindGameObjectWithTag("Forge");
        //forgeAnimation = dropOff.GetComponent<ForgeAnimationScript>();
        //drillScript = drill.GetComponentInChildren<DrillPartScript>();
        anim = GetComponentsInChildren<Animator>()[1];
        agent.destination = drill.transform.position;
        canvas = transform.GetChild(0).transform.GetChild(0).transform;
        notif = GetComponentInChildren<Text>();
        droppingOff = false;
    }
	
	void Update () {
        if (inventory >= inventoryMax && !droppingOff)
        {
            agent.SetDestination(dropOff.transform.position);
            droppingOff = true;
        }
        else if (inventory == 0 && droppingOff)
        {
            agent.SetDestination(drill.transform.position);
            droppingOff = false;
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
