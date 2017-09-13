using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticleScript : MonoBehaviour {
    private float vel;
    private float startPos;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        startPos = transform.position.y;
        vel = Random.Range(0.1f, 2f);
        float randVal = Random.Range(10f, 20f);
        transform.localScale = new Vector3(randVal, randVal, randVal);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(transform.localScale.x < 120)
        {
            transform.localScale += transform.localScale * 0.1f;
        }
        vel += 0.01f;
        transform.position += new Vector3(0, vel * Time.deltaTime, 0);

        if (transform.position.y > startPos + 7)
            transform.localScale += transform.localScale * 0.0075f;

        if (vel > 4)
        {
            anim.SetBool("fadeBool", true);
        }
	}
    void destroySmoke()
    {
        Destroy(gameObject);
    }
}
