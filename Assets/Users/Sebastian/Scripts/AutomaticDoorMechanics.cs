using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorMechanics : MonoBehaviour {

    public Animator animator;
    public string onTriggerOpen;
    public string onTriggerClose;

    // Use this for initialization
    void Start() {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (onTriggerOpen != null) {
            animator.SetTrigger(onTriggerOpen);
        }
   }

    void OnTriggerExit(Collider other) {
        if (onTriggerClose != null) {
            animator.SetTrigger(onTriggerClose);
        }
    }

}