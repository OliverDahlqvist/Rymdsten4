using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
    public Transform target;
    public float vel;

	void Start () {
	}
	
	void Update () {
        vel += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * vel);

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1f);
    }
}

