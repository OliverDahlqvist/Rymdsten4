using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour {
    Vector3 startScale;
    float scaleT;

	void Awake () {
        startScale = transform.localScale;
        scaleT = 0;
	}

    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        scaleT = 0;
    }

    void Update () {
        transform.Rotate(Vector3.up * (Time.deltaTime * 10), Space.World);
        if(scaleT < 1)
        {
            scaleT += Time.deltaTime * 2;
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), startScale, scaleT);
        }

        
	}
    
}
