using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandstormScript : MonoBehaviour {
    private float spawnDelay = 1f;

    [SerializeField]
    private List<GameObject> dustObjects;
    private Dictionary<GameObject, float> sandstormSpeed = new Dictionary<GameObject, float>();

    [SerializeField]
    private GameObject dustObj;

	void Start () {
		
	}
	
	void Update () {
        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0)
        {
            spawnDelay = 1f;
            for(int i = 0; i < 8; i++)
            {
                GameObject newObject = Instantiate(dustObj, new Vector3(transform.localPosition.x + i * Random.Range(5, 10), transform.localPosition.y, transform.localPosition.z), Random.rotation);
                float randVal = Random.Range(0.5f, 2f);
                sandstormSpeed.Add(newObject, Random.Range(0.1f, 0.2f));
                newObject.transform.localScale += new Vector3(randVal, randVal, randVal);
                dustObjects.Add(newObject);
            }
        }

        for(int i = 0; i < dustObjects.Count; i++)
        {

            dustObjects[i].transform.position += new Vector3(0, 0, Random.Range(0.05f, 0.1f));
        }
	}
}
