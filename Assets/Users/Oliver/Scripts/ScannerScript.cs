using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerScript : MonoBehaviour {
    int x = Screen.width / 2;
    int y = Screen.height / 2;
    RaycastHit hit;

    [SerializeField]
    Text laserDisplayText;
    [SerializeField]
    Text stoneDisplayText;
    [SerializeField]
    GameObject laserBeam;

    MineStone mineStone;

    private string displayText;
    private string stoneText;
    private Stone stoneHit;

    void Start () {
        if(laserBeam == null)
        {
            Debug.Log("Drag laserBeam reference to script");
        }
        mineStone = Camera.main.GetComponentInChildren<MineStone>();
	}
	
	void Update () {

        Ray ray = new Ray(laserBeam.transform.position, laserBeam.transform.forward);

        displayText = "--";
        stoneText = "--";

        if (Physics.Raycast(ray, out hit, 30))
        {
            if (hit.collider.GetComponent<Stone>())
            {
                stoneHit = hit.collider.GetComponent<Stone>();
                displayText = stoneHit.objectName;
                stoneText = PlayerClass.formatValue((stoneHit.amountStones * PlayerClass.stonesPerHitLaser));
            }
            else if (hit.collider.CompareTag("MiningDrill"))
            {
                DrillPartScript hitDrill = hit.collider.transform.root.GetComponentInChildren<DrillPartScript>();
                displayText = hitDrill.drillName;
                stoneText = hitDrill.drillAmount + "/" + hitDrill.drillAmountMax;
            }
            else if (hit.collider.CompareTag("CargoDrone"))
            {
                CargoScript hitDrone = hit.collider.transform.root.GetComponent<CargoScript>();
                displayText = hitDrone.name;
                stoneText = hitDrone.inventory + "/" + hitDrone.inventoryMax;
            }
        }
            
        laserDisplayText.text = displayText;
        stoneDisplayText.text = stoneText;
    }
}