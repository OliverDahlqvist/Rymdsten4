using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotNotification : MonoBehaviour {
    Animator anim;
    bool isNear = false;
	void Start () {
        anim = GetComponentsInChildren<Animator>()[1];
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isNear = !isNear;
            anim.SetBool("isNear", isNear);
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
