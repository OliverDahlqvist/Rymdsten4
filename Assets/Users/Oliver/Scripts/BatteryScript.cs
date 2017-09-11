using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {
    public float energy;
    public float maxEnergy;
    private string textEnergy;
    private float energyDec;
    private float lerpC;

    public GameObject craftingStation;
    Renderer craftingRenderer;

    TextMesh textMesh;

    [SerializeField]
    private Material screenMaterial;
    
    private Color onColor = new Color32(0, 90, 163, 255);
    private Color offColor = new Color32(158, 49, 49, 255);
   // private Color warningColor = new Color32(100, 0, 0, 255);

    private bool displayOn;

    [SerializeField]
    private float intensity;

	void Start () {
        textMesh = GetComponentInChildren<TextMesh>();
        craftingRenderer = GetComponent<Renderer>();

        screenMaterial.SetColor("_Color", onColor);
        screenMaterial.SetColor("_EmissionColor", onColor);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), onColor * 2);
        DynamicGI.UpdateMaterials(GetComponent<Renderer>());
        DynamicGI.UpdateEnvironment();

        energy = 11;
        maxEnergy = 100;
        displayOn = true;
        intensity = 1;
        lerpC = 0;
	}
	
	void Update () {
        energyDec = Time.deltaTime / 12;

        textEnergy = Mathf.Round(energy) + "/" + maxEnergy;
        textMesh.text = textEnergy;

        if(Mathf.Round(energy) <= maxEnergy / 10 && displayOn)
        {
            if(lerpC < 1)
            {
                lerpC += Time.deltaTime * 2;
            }
            screenMaterial.SetColor("_Color", Color.Lerp(onColor, offColor, lerpC));
            screenMaterial.SetColor("_EmissionColor", Color.Lerp(onColor, offColor, lerpC));
            DynamicGI.SetEmissive(GetComponent<Renderer>(), Color.Lerp(onColor, offColor, lerpC));
            DynamicGI.UpdateMaterials(GetComponent<Renderer>());

            DynamicGI.SetEmissive(craftingRenderer, Color.Lerp(onColor, offColor, lerpC));
            DynamicGI.UpdateMaterials(craftingRenderer);

            DynamicGI.UpdateEnvironment();

        }
        if(Mathf.Round(energy) > maxEnergy / 10 && displayOn)
        {
            if(lerpC == 1)
            {
                lerpC = 0;
            }
            screenMaterial.SetColor("_Color", onColor);
            screenMaterial.SetColor("_EmissionColor", onColor);
            DynamicGI.SetEmissive(GetComponent<Renderer>(), onColor);
            DynamicGI.UpdateMaterials(GetComponent<Renderer>());

            DynamicGI.SetEmissive(craftingRenderer, onColor);
            DynamicGI.UpdateMaterials(craftingRenderer);

            DynamicGI.UpdateEnvironment();
        }

        if (Mathf.Round(energy) <= 0 && displayOn)
        {
            displayOn = false;
        }
        else if (!displayOn && intensity > 0)
        {
            intensity -= 0.01f;

            screenMaterial.SetColor("_Color", offColor * intensity);
            screenMaterial.SetColor("_EmissionColor", offColor * intensity);

            DynamicGI.SetEmissive(GetComponent<Renderer>(), offColor * intensity);
            DynamicGI.UpdateMaterials(GetComponent<Renderer>());
            DynamicGI.UpdateEnvironment();
        }
        else if(displayOn)
        {
            energy -= energyDec;
        }
	}
}
