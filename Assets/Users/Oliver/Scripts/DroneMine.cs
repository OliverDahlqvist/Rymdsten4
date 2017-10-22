using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class DroneMine : MonoBehaviour {
    public GameObject target;
    public GameObject dropOff;
    ForgeAnimationScript forgeAnimation;
    NavMeshAgent agent;
    Stone targetStone;
    GameObject laserBeam;
    Animator anim;
    Text notif;
    Transform canvas;
    [SerializeField]
    Transform player;

    bool isNear = false;
    public bool hasTarget = false;
    public bool droppingOff = false;
    float distanceToTarget;
    float timeStamp = 0f;
    float inventorySize = 100;
    float amountStones = -1;
    float amountStonesPerHit = 1;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        forgeAnimation = dropOff.GetComponent<ForgeAnimationScript>();
        anim = GetComponentsInChildren<Animator>()[1];
        laserBeam = transform.GetChild(0).transform.GetChild(0).gameObject;
        canvas = transform.GetChild(0).transform.GetChild(1).transform;
        notif = GetComponentInChildren<Text>();
    }

	void Update () {

        if (!hasTarget && findClosest() != null)
        {
            target = findClosest();
            targetStone = target.GetComponent<Stone>();
            agent.destination = target.transform.position;
            hasTarget = true;
        }
        else
        {
            if (droppingOff)
            {
                distanceToTarget = Vector3.Distance(dropOff.transform.position, transform.position);
            }
            else
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        }
        
        if (distanceToTarget <= 15)
        {
            if (amountStones < inventorySize && !droppingOff && !targetStone.destroyObject)
            {
                laserBeam.SetActive(true);

                if (Time.time >= timeStamp)
                {
                    timeStamp = Time.time + 0.1f;
                    if (Mathf.Floor(targetStone.amountStones) <= 0)
                    {
                        targetStone.destroyObject = true;
                    }
                    else
                    {
                        targetStone.amountStones--;
                        amountStones += amountStonesPerHit;
                        if(amountStones > inventorySize)
                        {
                            amountStones = inventorySize;
                        }
                    }
                }

                laserBeam.transform.LookAt(target.transform);
                laserBeam.transform.localScale = new Vector3(1, 1, distanceToTarget);
            }
            else if(!PlayerClass.usingForge && droppingOff)
            {
                forgeAnimation.openDoor = true;
                PlayerClass.credits += amountStones;
                amountStones = 0;
            }
        }
        else
        {
            laserBeam.SetActive(false);
        }

        if (hasTarget && !droppingOff)
        {
            if (targetStone.destroyObject)
            {
                hasTarget = false;
                target = null;
            }
        }

        // Dropoff rocks //
        if(amountStones >= inventorySize && !droppingOff)
        {
            agent.destination = dropOff.transform.position;
            droppingOff = true;
        }
        else if(amountStones == 0 && droppingOff)
        {
            agent.destination = target.transform.position;
            droppingOff = false;
        }
        // Display Inventory //
        if (isNear)
        {
            notif.text = PlayerClass.formatValue(amountStones) + "/" + PlayerClass.formatValue(inventorySize);
            canvas.rotation = Quaternion.LookRotation(transform.position - player.position);
        }

    }

    GameObject findClosest()
    {
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach(GameObject rock in rocks)
        {
            if(rock.GetComponent<Stone>().destroyObject == false)
            {
                Vector3 diff = rock.transform.position - position;
                float curDist = diff.sqrMagnitude;
                if (curDist < distance)
                {
                    closest = rock;
                    distance = curDist;
                }
            }
        }
        return closest;
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
