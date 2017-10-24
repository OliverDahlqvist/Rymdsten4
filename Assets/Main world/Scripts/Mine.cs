using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour {

    MineStone mineScript;
    CameraShake cameraShake;

    public GameObject[] particleSystems;
    /*public GameObject PS_Sparks;
    public GameObject PS_Impact;
    public GameObject PS_Light;*/

    [Header("Sounds")]
    [SerializeField]
    Text notificationText;
    Animator animator;
    public AudioClip[] hitSound;
    AudioSource audioSource;

    private int stonesToAdd;


    void Start () {
        animator = GetComponent<Animator>();
        mineScript = Camera.main.GetComponent<MineStone>();
        cameraShake = Camera.main.GetComponentInChildren<CameraShake>();
        audioSource = GetComponent<AudioSource>();
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
        if (mineScript.hitStone == true && PlayerClass.stones < PlayerClass.inventorySize)
        {
            if (mineScript.hit.collider.GetComponentInParent<Stone>().CompareTag("laserRock") && !PlayerClass.blackHoleSelected)
            {
                PlayerClass.notificationText = "NEED LASER";
                PlayerClass.displayNotification = true;
            }
            else
            {
                if (transform.name == "PickaxeModel")
                {
                    audioSource.clip = hitSound[Random.Range(0, hitSound.Length)];
                }
                audioSource.Play();
               
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

                    for (int i = 0; i < particleSystems.Length; i++)
                    {
                        Instantiate(particleSystems[i], mineScript.hit.point, Quaternion.LookRotation(mineScript.hit.normal));
                    }
                    
                    if (PlayerClass.blackHoleSelected)
                    {
                        cameraShake.AddShake(1);
                    }
                    else
                    {
                        cameraShake.AddShake(0.2f);
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
