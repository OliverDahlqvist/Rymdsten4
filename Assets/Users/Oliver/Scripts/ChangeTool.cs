using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTool : MonoBehaviour {
    float mouseX;
    float mouseY;
    Quaternion rotationSpeed;
    public float speed;
    private float offset;
    private float lerpSpeed;

    [HideInInspector]
    public GameObject activeTool;
    private GameObject secondTool;

    [SerializeField]
    private List<ToolVariables> tools = new List<ToolVariables>();
    private int selectedTool;
    private int amountTools;
    public bool buildActive = false;
    private int lastInput = 0;

    public int tool1 = 0;
    public int tool2 = 1;
    public int tool3 = 2;
    public int tool4 = 3;
    
    /*private Vector3 startPosPick;
    private Vector3 startPosLaser;
    private Vector3 endPosPick;
    private Vector3 endPosLaser;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 endPosSunEater;*/

    [SerializeField]
    private GameObject pick;
    private GameObject laser;


	void Start () {
        selectedTool = 0;
        amountTools = transform.childCount;

        for (int i = 0; i < amountTools; i++)
        {
            tools.Add(transform.GetChild(i).GetComponent<ToolVariables>());
        }

        getCurrentTool(0);
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1) && lastInput != tool1)
        {
            getCurrentTool(tool1);
            lastInput = tool1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && lastInput != tool2)
        {
            getCurrentTool(tool2);
            lastInput = tool2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && lastInput != tool3)
        {
            getCurrentTool(tool3);
            lastInput = tool3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && lastInput != tool4)
        {
            getCurrentTool(tool4);
            lastInput = tool4;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            buildActive = !buildActive;
            if(!buildActive)
            {
                getCurrentTool(lastInput);
            }
            else
            {
                getCurrentTool(4);
            }
        }

        if(selectedTool < 4)
        {
            buildActive = false;
            PlayerClass.building = false;
        }

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        rotationSpeed = Quaternion.Euler(-mouseY, offset - mouseX, 0);
        activeTool.transform.localRotation = Quaternion.Slerp(activeTool.transform.localRotation, rotationSpeed, speed * Time.deltaTime);
	}

    void getCurrentTool(int input)
    {
        selectedTool = input;
        activeTool = tools[input].gameObject;
        activeTool.SetActive(true);
        for (int i = 0; i < amountTools; i++)
        {
            if (i != input)
            {
                tools[i].gameObject.SetActive(false);
            }
        }

        PlayerClass.mineLength = tools[selectedTool].mineLength;
        PlayerClass.mineRate = tools[selectedTool].mineRate;

        if (input == 1)
        {
            PlayerClass.laserSelected = true;
        }
        else
        {
            PlayerClass.laserSelected = false;
        }
    }
}
