using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLogPlay : MonoBehaviour
{

    LogAudio logAudio;

    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start()
    {
        logAudio = GetComponent<LogAudio>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2))
            {
                // If hit audio log, the object most have the LogAudion script 
                if (hit.collider.gameObject.tag == "AudioLog")
                {
                    m_AudioSource.Stop();
                    m_AudioSource.clip = hit.collider.gameObject.GetComponent<LogAudio>().Log;
                    m_AudioSource.Play();

                    Destroy(hit.collider.gameObject, 0.1f);

                    Debug.Log("you hit a AudioLog");
                    
                    //AudioSource.PlayClipAtPoint(hit.collider.gameObject.GetComponent<LogAudio>().Log, transform.position);


                }




            }
        }
    }
}