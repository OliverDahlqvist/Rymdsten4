using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeScript : MonoBehaviour {
    public Material lavaMaterial;
    
    private Color lavaColor = new Color32(178, 178, 178, 255);

    [HideInInspector]
    public bool openDoor;

	void Start () {
        lavaMaterial.SetColor("_Emission", lavaColor);
        openDoor = false;
    }

	void Update () {
        //lavaMaterial.SetColor("_Emission", lavaColor);
        //DynamicGI.SetEmissive(GetComponent<Renderer>(), lavaColor);
        //DynamicGI.UpdateMaterials(GetComponent<Renderer>());
        //DynamicGI.UpdateEnvironment();
    }
}
