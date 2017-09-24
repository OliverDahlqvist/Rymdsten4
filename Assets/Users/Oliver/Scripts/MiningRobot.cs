using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiningRobot : MonoBehaviour {
    NavMeshAgent movement;
    public GameObject dustParticle;
    float rotT = 0;
    private RaycastHit hit;

    float rotateSpeed = 1f;
    private float timestamp = 0F;
    public float delay = 0.4f;

    void Start () {
        movement = GetComponentInParent<NavMeshAgent>();
	}
	
	void Update () {
        // Tilt when moving
        if (movement.desiredVelocity.magnitude > 1f && rotT < 1)
        {
            rotT += Time.deltaTime;
        }
        else if(movement.desiredVelocity.magnitude < 1f && rotT > 0)
        {
            rotT -= Time.deltaTime;
        }
        transform.localEulerAngles = new Vector3(Mathf.Lerp(-90, -75, rotT), transform.rotation.y, transform.rotation.z);

        if (Time.time >= timestamp)
        {
            timestamp = Time.time + delay;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    Instantiate(dustParticle, hit.point, Random.rotation);
                }
            }
        }
    }
}
