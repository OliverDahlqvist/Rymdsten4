using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Scanner : MonoBehaviour {
    [SerializeField]
    private Animator projectionAnimator;
    [SerializeField]
    private Animator mainAnimator;
    [SerializeField]
    CameraShake cameraShake;
    [SerializeField]
    private AnimationCurve scanValueCurve;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private AudioSource scannerHitAudio;
    private Vector3 parentStart;

    public Text[] text;
    public Slider slider;

    ScienceObject objectHit;

    private bool display;
    private float scanValue;

    List<char> scanValueCurrent = new List<char>();
    List<char> scanValueTarget = new List<char>();

    private float timestamp = 0;

    void Start () {
        projectionAnimator = GetComponent<Animator>();
        mainAnimator = transform.parent.GetComponentInChildren<Animator>();
        parentStart = parentTransform.localPosition;

        display = false;
        scanValue = 0;

        text = new Text[transform.parent.GetComponentsInChildren<Text>().Length];
        text = transform.parent.GetComponentsInChildren<Text>();
        slider = transform.parent.GetComponentInChildren<Slider>();
        scanValueCurrent.AddRange("00".ToCharArray());
    }
	
	void Update () {
        ScrollingText();

        if (Input.GetMouseButton(0))
        {
            if (objectHit != null)//Hit something
            {
                if(scanValue < 1)//Sets science points value
                scanValueTarget = objectHit.scienceValue.ToString().ToCharArray().ToList();
                else
                {
                    scanValueTarget = "00".ToList();
                }

                if (!objectHit.exhausted)//SCAN; if object hit is not exhausted
                {
                    
                    SetVolume(scannerHitAudio, true, 0.225f, 1);

                    scanValue += Time.deltaTime / 2;
                    objectHit.m_light.intensity = Mathf.Lerp(1.47f, 0, scanValueCurve.Evaluate(scanValue));

                    if(scanValue >= 1)//Scan complete
                    {

                        PlayerClass.sciencePoints += objectHit.scienceValue;
                        objectHit.exhausted = true;
                    }
                }
                else//If stone is hit but exhausted
                {
                    if(scanValue > 0)
                        scanValue -= Time.deltaTime / 0.1f;

                    SetVolume(scannerHitAudio, false, 0, 2);
                }
            }
            else//Nothing hit
            {
                SetVolume(scannerHitAudio, false, 0, 2);
                text[0].text = "IDLE";
                scanValueTarget = "00".ToList();
            }
            display = true;
        }
        else
        {
            if (scannerHitAudio.volume > 0)
                scannerHitAudio.volume -= Time.deltaTime;

            if (objectHit != null)
            {
                if (!objectHit.exhausted)
                    objectHit.m_light.intensity = 1.47f;
            }

            if (scanValue > 0)
                scanValue -= Time.deltaTime / 0.1f;

            scanValueTarget = "00".ToList();
            text[0].text = "IDLE";
            display = false;
        }

        parentTransform.localPosition = Vector3.Lerp(parentStart, new Vector3(0.67f, -0.526f, 1.671f), scanValue);

        slider.value = scanValueCurve.Evaluate(scanValue);
        if (display != projectionAnimator.GetBool("display"))
        {
            projectionAnimator.SetBool("display", display);
            mainAnimator.SetBool("scanning", display);
        }
    }

    private void SetVolume(AudioSource source, bool increase, float volume, float time)
    {
        if (increase)
        {
            if (source.volume < volume)
                source.volume += Time.deltaTime / time;
        }
        else
        {
            if (source.volume > volume)
                source.volume -= Time.deltaTime / time;
        }
    }

    private void ScrollingText()
    {
        if (Time.time >= timestamp)
        {
            timestamp = Time.time + 0.1f;

            while (scanValueCurrent.Count < scanValueTarget.Count)//Add to list
            {
                scanValueCurrent.Add('0');
            }
            while (scanValueCurrent.Count > scanValueTarget.Count)//Remove from list
            {
                scanValueCurrent.RemoveAt(scanValueCurrent.Count - 1);
            }

            for (int i = 0; i < scanValueCurrent.Count; i++)//Changes current char to lower or higher
            {
                if (scanValueCurrent[i] < scanValueTarget[i])
                {
                    scanValueCurrent[i] += (char)1;
                }
                else if (scanValueCurrent[i] > scanValueTarget[i])
                {
                    scanValueCurrent[i] -= (char)1;
                }
            }

            string debugString = new string(scanValueCurrent.ToArray());
            text[1].text = debugString;
            if (objectHit != null)
            {
                text[0].text = objectHit.scienceName;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Science"))
        {
            objectHit = other.gameObject.GetComponent<ScienceObject>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Science"))
        {
            objectHit = null;
            scanValue = 0;
        }
    }
}
