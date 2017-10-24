using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject SelectedObject;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*if (Input.GetAxisRaw("Vertical") != 0 && ButtonSelected == false)
        {
            eventSystem.SetSelectedGameObject(SelectedObject);
            ButtonSelected = true;
        }*/
		
	}

}
