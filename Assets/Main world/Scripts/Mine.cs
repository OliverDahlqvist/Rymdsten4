using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour {

    
    //public GameObject Camera;
    MineStone mineScript;
    CameraShake cameraShake;

    [SerializeField]
    GameObject PS_Sparks;
    [SerializeField]
    Text notificationText;


    public GameObject PS_Impact;
    Animator animator;
    public AudioClip[] hitSound;
    AudioSource audio;
    private int stonesToAdd;


    void Start () {
        animator = GetComponent<Animator>();
        mineScript = Camera.main.GetComponent<MineStone>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audio = GetComponent<AudioSource>();
	}
	void Update () {
        if (Input.GetMouseButton(0) && PlayerClass.menuActive < 1)
        {
            animator.SetBool("Mining", true);
        }
        else
        {
            animator.SetBool("Mining", false);
        }
        

    }
    public void Spark()
    {
        Debug.Log("Hej");
        if (mineScript.hitStone == true && PlayerClass.stones < PlayerClass.inventorySize)
        {
            if (mineScript.hit.collider.GetComponentInParent<Stone>().CompareTag("laserRock") && !PlayerClass.blackHoleSelected)
            {
                PlayerClass.notificationText = "NEED LASER";
                PlayerClass.displayNotification = true;
            }
            else
            {
                if (transform.name == "Pickaxe")
                {
                    audio.clip = hitSound[Random.Range(0, hitSound.Length)];
                }
                audio.Play();
               
                mineScript.addStones = true;
                Stone stoneHit = mineScript.rayHit;
                if (stoneHit.destroyObject == false)
                {
                    if (PlayerClass.stonesPerHitPick + PlayerClass.stones > PlayerClass.inventorySize)
                    {
                        PlayerClass.stones = PlayerClass.inventorySize;
                    }
                    else
                    {
                        if (PlayerClass.blackHoleSelected)
                        {
                            PlayerClass.stones += stoneHit.amountStones * PlayerClass.stonesPerHitPick;
                        }
                        else
                        {
                            PlayerClass.stones += PlayerClass.stonesPerHitPick;
                        }
                    }

                    
                    if (PlayerClass.blackHoleSelected)
                    {
                        stoneHit.amountStones = 0;
                        stoneHit.changeColor = true;
                    }
                    else
                    {
                        stoneHit.amountStones -= 1;
                    }

                    Instantiate(PS_Impact, mineScript.hit.point, Quaternion.LookRotation(mineScript.hit.normal));
                    Instantiate(PS_Sparks, mineScript.hit.point, Quaternion.LookRotation(mineScript.hit.normal));
                    if (PlayerClass.blackHoleSelected)
                    {
                        cameraShake.shakeDuration += 1f;
                    }
                    else
                    {
                        cameraShake.shakeDuration += 0.2f;
                    }
                }

                if (Mathf.Floor(stoneHit.amountStones) <= 0)
                {
                    stoneHit.destroyObject = true;
                }
            }
        }
        else
        {
            mineScript.addStones = false;
        }
    }
}
