using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CreditsRoll : MonoBehaviour {

    public Text creditsText;
    public float scrollSpeed = 50f;
    public bool scrollGo = false; 
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (scrollGo)
        {
            creditsText.rectTransform.position += new Vector3(0, Time.deltaTime * scrollSpeed, 0);
        }
        
		
	}
}
