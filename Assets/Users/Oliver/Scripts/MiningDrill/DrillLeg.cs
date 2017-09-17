using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillLeg : MonoBehaviour {
    private Vector3 startPoint;
    private RaycastHit hit;
    private AudioSource impactSound;

    [SerializeField]
    GameObject dustParticle;

    private bool locked = false;
    private float speed;
    private float acc;
    private bool damp;

    void Start () {
        startPoint = transform.GetChild(0).transform.position;
        speed = 0;
        acc = 80;
        impactSound = GetComponentInChildren<AudioSource>();
        impactSound.clip = GetComponentInParent<MiningDrillVariables>().impactSound;
	}

	void Update () {
        if (Physics.Raycast(startPoint, Vector3.down, out hit, 0.7f))
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                if (!damp)
                {
                    damp = true;
                    speed = 0;
                    acc = -6;
                    emitDust(hit);
                }
            }
        }
        if (!locked)
        {
            if (damp && speed <= -3)
            {

                locked = true;

                GetComponent<DrillLeg>().enabled = false;
            }
            startPoint = transform.GetChild(0).transform.position;
            speed += acc * Time.deltaTime;
            transform.parent.transform.Rotate(transform.parent.transform.right * (Time.deltaTime * speed), Space.World);
        }
	}
    void emitDust(RaycastHit spawnLocation)
    {
        Instantiate(dustParticle, spawnLocation.point, Random.rotation);
        impactSound.Play();
    }
}
