using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateNotification : MonoBehaviour {
    [SerializeField]
    Text notification;
    [SerializeField]
    Text notification_shadow;

    Color notificationColor;
    Color notification_shadowColor;

    void Start () {
        notificationColor = notification.color;
        notification_shadowColor = notification_shadow.color;
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
        if (PlayerClass.displayNotification)
        {
            PlayerClass.textValue += Time.deltaTime;
            if (PlayerClass.textValue >= 1)
            {
                PlayerClass.displayNotification = false;
            }
        }
        if (!PlayerClass.displayNotification && PlayerClass.textValue > 0)
        {
            PlayerClass.textValue -= Time.deltaTime * 2f;
        }
        if (PlayerClass.textValue < 0)
        {
            PlayerClass.textValue = 0;
        }

        if (PlayerClass.textValue >= 0 || PlayerClass.textValue <= 0)
        {
            notificationColor.a = Mathf.Lerp(0, 1, PlayerClass.textValue);
            notification_shadowColor.a = Mathf.Lerp(0, 1, PlayerClass.textValue);
            notification_shadow.color = notification_shadowColor;
            notification.color = notificationColor;
        }

        if(notification.text != PlayerClass.notificationText)
        {
            notification.text = PlayerClass.notificationText;
            notification_shadow.text = PlayerClass.notificationText;
        }
    }
}
