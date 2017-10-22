using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDrill : MonoBehaviour {
    private Button button;
    DrillPartScript selectedDrill;
    MiningdrillUpgradeMenu menu;
    [TextArea(1, 3)]
    public string description;
    private float total = 11;

    public enum ButtonType
    {
        Speed, Yield, Tier
    };
    public ButtonType buttonIndex;

    void Start () {
        menu = GetComponentInParent<MiningdrillUpgradeMenu>();
        button = GetComponent<Button>();
    }
	void Update () {
        if (menu.selectedDrill != null)
        {
            selectedDrill = menu.selectedDrill;
        }
        if (selectedDrill != null)
        {
            if ((int)buttonIndex < 2)
            {
                if (PlayerClass.credits > selectedDrill.price[(int)buttonIndex] && selectedDrill.rank[(int)buttonIndex] < selectedDrill.upgrade[2])
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else if((int)buttonIndex == 2)
            {
                if (PlayerClass.credits > selectedDrill.price[(int)buttonIndex] && selectedDrill.rank[(int)buttonIndex] < PlayerClass.drillTierMax)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
        }
    }

    public void PushButton()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            int amountPresses = 0;
            float totalValue = selectedDrill.price[(int)buttonIndex];
            while (PlayerClass.credits > totalValue && amountPresses < 10 && amountPresses < selectedDrill.upgrade[selectedDrill.upgrade.Length - 1] - 1)
            {
                amountPresses++;
                totalValue += selectedDrill.price[(int)buttonIndex] * Mathf.Pow(1.10f, amountPresses);
            }
            for (int i = 0; i < amountPresses; i++)
            {
                selectedDrill.PurchaseUpgrade((int)buttonIndex);
            }
        }
        else
        {
            selectedDrill.PurchaseUpgrade((int)buttonIndex);
        }
    }
}
