using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiningRobot : MonoBehaviour {
    NavMeshAgent movement;
    float rotT = 0;

    float rotateSpeed = 1f;

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
    }
}
