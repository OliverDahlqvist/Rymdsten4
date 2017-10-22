using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerScript : MonoBehaviour {
    int x = Screen.width / 2;
    int y = Screen.height / 2;
    public RaycastHit hit;

    [SerializeField]
    Text[] textfield;
    [SerializeField]
    GameObject startPosition;


    private string[] text = new string[2];

    private Stone stoneHit;

    void Start () {
	}
	
	void Update () {

        Ray ray = new Ray(startPosition.transform.position, startPosition.transform.forward);

        for(int i = 0; i < text.Length; i++)
        {
            text[i] = "--";
        }

        if (Physics.Raycast(ray, out hit, 30))
        {
            if (hit.collider.GetComponent<Stone>())
            {
                stoneHit = hit.collider.GetComponent<Stone>();
                text[0] = stoneHit.objectName;
                if(text.Length > 1)
                text[1] = PlayerClass.formatValue((stoneHit.amountStones * PlayerClass.stonesPerHitLaser));
            }
            else if (hit.collider.CompareTag("MiningDrill"))
            {
                DrillPartScript hitDrill = hit.collider.transform.root.GetComponentInChildren<DrillPartScript>();
                text[0] = hitDrill.drillName;
            }
        }
        for(int i = 0; i < textfield.Length; i++)
        {
            textfield[i].text = text[i];
        }

    }
}