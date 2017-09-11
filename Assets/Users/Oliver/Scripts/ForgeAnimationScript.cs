using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeAnimationScript : MonoBehaviour {
    public bool openDoor;
    Animator animator;
    private float spawnDelay;

    [SerializeField]
    private GameObject smokeParticlePrefab;

	void Start () {
        animator = GetComponent<Animator>();
        spawnDelay = 0.75f;
	}

	void Update () {
        if (openDoor)
        {
            animator.SetBool("isOpen", true);
            if (spawnDelay <= 0)
            {
                smokeEmitter();
                spawnDelay = 0.75f;
            }
            spawnDelay -= Time.deltaTime * 4;
        }
        else
        {
            animator.SetBool("isOpen", false);
        }
    }
    void smokeEmitter()
    {
        Instantiate(smokeParticlePrefab, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Random.rotation);
    }
}
