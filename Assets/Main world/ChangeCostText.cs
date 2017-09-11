using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeCostText : MonoBehaviour {

    public Text Cost;

    public void ChangeCostOxy()
    {
        Cost.text = PlayerClass.UpgradeCostOxygen.ToString() + "$";
        
    }
    public void ChangeCostPick()
    {
        Cost.text = PlayerClass.UpgradeCostPick.ToString() + "$";
    }
    public void ChangeCostInv()
    {
        Cost.text = PlayerClass.UpgradeCostInventory.ToString() + "$";
    }
    public void ChangeCostSPeed()
    {
        Cost.text = PlayerClass.UpgradeCostSpeed.ToString() + "$";
    }
    public void ChangeCostLaser()
    {
        Cost.text = PlayerClass.UpgradeCostLaserDrill.ToString() + "$";
    }
}

