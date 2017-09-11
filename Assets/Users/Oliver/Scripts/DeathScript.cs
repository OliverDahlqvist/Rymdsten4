using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour {
    [SerializeField]
    Image overlay;
    Vector3 respawnPosition;

    float t;
    bool respawnSequence;

	void Start () {
        respawnPosition = GameObject.FindGameObjectWithTag("RespawnPosition").transform.position;
        t = 0;
        respawnSequence = false;
	}
	
	void Update () {
        if (respawnSequence)
        {
            if (t > 0)
            {
                t -= Time.deltaTime;
            }

            overlay.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), t);

            if (t < 0)
            {
                t = 0;
                respawnSequence = false;
            }
        }

        if (PlayerClass.CurrentEnergy <= 0)
        {
            if(t < 4 && !respawnSequence)
            {
                t += Time.deltaTime / 1;
            }
            overlay.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), t);

            
            if(t >= 1)
            {
                
                PlayerClass.displayNotification = true;
                PlayerClass.notificationText = "\n\n\n\nYou died \n\nPress any key to respawn";
                if (Input.anyKey && t > 3)
                {
                    t = 1;

                    transform.position = respawnPosition;

                    PlayerClass.CurrentEnergy = PlayerClass.EnergyMax;
                    PlayerClass.stones = 0;

                    respawnSequence = true;
                    //GameObject.FindGameObjectWithTag("Vehicle").transform.position = GameObject.FindGameObjectWithTag("RoverRespawn").transform.position;
                }
            }
        }
        

        
	}
}
