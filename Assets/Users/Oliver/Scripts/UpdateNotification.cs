using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateNotification : MonoBehaviour {
    [SerializeField]
    Text notification;
    [SerializeField]
    Animator anim;

    void Start () {
    }

	void Update () {
        // DEBUG //
        if (Input.GetKey(KeyCode.F1))
            PlayerClass.credits += 10;
        else if (Input.GetKey(KeyCode.F2))
            PlayerClass.credits += 1000;
        else if (Input.GetKey(KeyCode.F3))
            PlayerClass.credits += 100000;
        else if (Input.GetKey(KeyCode.F4))
            PlayerClass.credits += 1000000;
        else if (Input.GetKey(KeyCode.F5))
            PlayerClass.credits += 1000000000;
        else if (Input.GetKey(KeyCode.F6))
            PlayerClass.credits += 1000000000000;

        // TEXT //
        /*if (PlayerClass.displayNotification)
        {
            PlayerClass.textValue += Time.deltaTime * 10f;
            if (PlayerClass.textValue >= 1)
            {
                PlayerClass.displayNotification = false;
            }
        }
        if (!PlayerClass.displayNotification && PlayerClass.textValue > 0)
        {
            PlayerClass.textValue -= Time.deltaTime * 10f;
        }
        if (PlayerClass.textValue < 0)
        {
            PlayerClass.textValue = 0;
        }

        if (PlayerClass.textValue >= 0 || PlayerClass.textValue <= 0)
        {
            notificationColor.a = Mathf.Lerp(0, 1, PlayerClass.textValue);
            notPanelColor.a = Mathf.Lerp(0, 0.45f, PlayerClass.textValue);
            notification.color = notificationColor;
            notPanel.color = notPanelColor;
        }*/

        if (PlayerClass.displayNotification)
        {
            PlayerClass.displayNotification = false;
            anim.SetBool("displayNotification", true);
        }
        else
        {
            anim.SetBool("displayNotification", false);
        }

        if(notification.text != PlayerClass.notificationText)
        {
            notification.text = PlayerClass.notificationText;
        }
    }
}
