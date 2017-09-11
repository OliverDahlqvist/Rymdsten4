using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour {
    AudioSource laserAudio;
    [SerializeField]
    GameObject laserBeam;
    MineStone mineStoneScript;
    float maxVolume;
	// Use this for initialization
	void Start () {
        laserAudio = GetComponent<AudioSource>();
        mineStoneScript = Camera.main.GetComponent<MineStone>();
        laserAudio.volume = 0;
        maxVolume = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        if (laserBeam.gameObject.activeSelf)
        {
            if (mineStoneScript.hitStone)
                laserAudio.volume = 0.3f;
            else
                laserAudio.volume = 0.3f;

            if(laserAudio.volume < maxVolume)
            {
                laserAudio.volume += Time.deltaTime * 2f;
            }
            //laserAudio.volume = 0.3f;
        }
        else
        {
            if(laserAudio.volume > 0)
            {
                laserAudio.volume -= Time.deltaTime * 2f;
            }
            //laserAudio.volume = 0;
        }
	}
}
