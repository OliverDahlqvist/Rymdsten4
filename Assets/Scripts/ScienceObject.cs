using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceObject : MonoBehaviour {
    public string scienceName;
    public bool exhausted = false;
    public Light m_light;

    private float m_scienceValue;

    public float scienceValue
    {
        get
        {
            if(exhausted == true)
            {
                return 0;
            }
            else
            {
                return m_scienceValue;
            }
        }
        set
        {
            m_scienceValue = value;
        }
    }

	void Start () {
        m_light = GetComponentInChildren<Light>();
        m_scienceValue = (int)Random.Range(10, 100);
	}
	
	void Update () {
		
	}
}
