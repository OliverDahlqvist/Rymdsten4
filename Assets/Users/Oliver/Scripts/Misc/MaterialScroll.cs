using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScroll : MonoBehaviour {
    Renderer mainMat;
    [Range(0, 100)]
    public float scrollSpeed;

	void Start () {
        mainMat = GetComponent<Renderer>();
	}
	
	void Update () {
        float offset = Time.time * scrollSpeed;
        mainMat.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
