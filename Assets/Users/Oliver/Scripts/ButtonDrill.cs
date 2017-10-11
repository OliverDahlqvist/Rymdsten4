using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDrill : MonoBehaviour {
    private Button button;
    DrillPartScript selectedDrill;
    MiningdrillUpgradeMenu menu;
    public string description;

    public enum ButtonType
    {
        Speed, Yield
    };
    public ButtonType buttonIndex;

    void Start () {
        menu = GetComponentInParent<MiningdrillUpgradeMenu>();
        button = GetComponent<Button>();
    }
	void Update () {
        selectedDrill = menu.selectedDrill;
        if (selectedDrill != null)
        {
            if (selectedDrill.price[(int)buttonIndex] > PlayerClass.credits)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }

    public void PushButton()
    {
        selectedDrill.PurchaseUpgrade((int)buttonIndex);
    }
}
