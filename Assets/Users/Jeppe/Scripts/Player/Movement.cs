using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    CharacterController controller;
    Camera playerCam;
    Light flashLight;

    Vector3 inputDir;
    Vector3 moveDir;

    private float moveX;
    private float moveY;

    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    private float moveSpeed;

    public float speedTransitionTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    private float yVelocity;
    public float thirtyeightProcentOfEarthGravity = 6.076f;
    public float jumpForce = 2f;
    public float jetForce = 0.25f;

    bool run;
    bool jetPackOnOff;

    Vector2 inputLook;
    Vector2 mouseLook;
    Vector2 smoothLook;

    public float mouseSens = 0.5f;
    public float mouseSmooth = 4.0f;
    public float xLookMin = -75.0f;
    public float yLookMin = 90.0f;

    public float playerFlashLightDrain = 1f;
    public float playerJetPackDrain = 5;

    private float groundSlopeAngle = 0f;
    private Vector3 groundSlopeDir;

    //_____________________________________________________
    /*enum MOVEMENT
    {
        Idle, Walk, Run, Jump
    }
    MOVEMENT currentMovement = Idle;*/


    //Awake
    void Awake() {

        controller = GetComponent<CharacterController>();
        playerCam = gameObject.GetComponentInChildren<Camera>();
        flashLight = gameObject.GetComponentInChildren<Light>();

        jetPackOnOff = false;
        flashLight.enabled = false;

        //Hide mousecursor in game
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Update
    void Update() {

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        inputDir = new Vector3(moveX, yVelocity, moveY);
        currentSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedTransitionTime);
        moveDir = transform.rotation * inputDir * currentSpeed;

        //moveDir.y -= thirtyeightProcentOfEarthGravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);

        //Gravity
        if (!controller.isGrounded) {
            yVelocity -= thirtyeightProcentOfEarthGravity * Time.deltaTime;
        }
        else {
            yVelocity = 0;
        }

        //Walk/Run
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = runSpeed;
        }
        else{
            moveSpeed = walkSpeed;
        }

        //Jump
        if (controller.isGrounded && !jetPackOnOff && groundSlopeAngle < 40 && Input.GetKey(KeyCode.Space))
        {
            yVelocity += jumpForce;
        }

        //Jetpack
        /*if (PlayerClass.JetpackBuilt && Input.GetKeyDown(KeyCode.J)) {
            jetPackOnOff = !jetPackOnOff;
        }
        if (jetPackOnOff && Input.GetKey(KeyCode.Space)) {
            yVelocity += jetForce;
            PlayerClass.jetPackDrain = playerJetPackDrain;
        }
        else {
            PlayerClass.jetPackDrain = 0;
        }*/

        //Flashlight
        if (Input.GetKeyDown(KeyCode.F)) {
            flashLight.enabled = !flashLight.enabled;
        }
        if (flashLight.enabled) {
            PlayerClass.flashLightDrain = -1;
        }
        else {
            PlayerClass.flashLightDrain = 0;
        }
        /*
        //Falldamage
        if (!controller.isGrounded) {

            fallDist = transform.position.y - latestYPos;
            if (fallDist < 0)
                fallCounter += Time.deltaTime;
        }
        if (controller.isGrounded) {

            PlayerClass.fallDmgDrain = 0;

            if (fallCounter >= fallDmgAfterSec) {
                PlayerClass.fallDmgDrain = -fallCounter * dmgMulti;
            }

            fallCounter = 0;
            //PlayerClass.fallDmgDrain = 0;
            latestYPos = transform.position.y;
        }
        if (PlayerClass.fallDmgDrain < 0) {
            //Debug.Log(PlayerClass.fallDmgDrain);
        }
        */
        //Look
        MouseLook();

        //Show mousecursor
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }

    // MouseLook
    void MouseLook() {

        inputLook = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputLook = Vector2.Scale(inputLook, new Vector2(mouseSens * mouseSmooth, mouseSens * mouseSmooth));

        smoothLook.x = Mathf.Lerp(smoothLook.x, inputLook.x, 1f / mouseSmooth);
        smoothLook.y = Mathf.Lerp(smoothLook.y, inputLook.y, 1f / mouseSmooth);
        mouseLook += smoothLook;

        mouseLook.y = Mathf.Clamp(mouseLook.y, xLookMin, yLookMin);

        playerCam.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
    }
    /*
    void OnControllerColliderHit(ControllerColliderHit hit) {

        Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
        groundSlopeDir = Vector3.Cross(temp, hit.normal);
        groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    }*/
}
