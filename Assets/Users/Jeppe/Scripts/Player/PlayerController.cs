using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour {
    
    Transform cameraTransform;
    Camera camera;
    Rigidbody rigidBody;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    [SerializeField] private float lookSensiX = 250f;
    [SerializeField] private float lookSensiY = 250f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

    private float verticalLook;

    // Awake
    void Awake() {
        //Camera
        camera = GameObject.FindWithTag("PlayerCamera").GetComponent<Camera>();
        cameraTransform = camera.transform;
        //Avatar
        rigidBody = GetComponent<Rigidbody>();
        //Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update
    void Update() {
        
        //X look
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * lookSensiX * Time.deltaTime);
        //Y look
        verticalLook += Input.GetAxis("Mouse Y") * lookSensiY * Time.deltaTime;
        verticalLook = Mathf.Clamp(verticalLook, -90, 90);
        cameraTransform.localEulerAngles = Vector3.left * verticalLook;

        //Walk
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 playerMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, playerMoveAmount, ref smoothMoveVelocity, 0.15f);

        //Show cursor
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    // LateUpdate
    void LateUpdate () {

        rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveAmount) * Time.deltaTime);
    }
}
