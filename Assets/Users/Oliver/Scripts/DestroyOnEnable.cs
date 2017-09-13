using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnable : MonoBehaviour {
	void OnEnable () {
        Destroy(gameObject);
	}
}
