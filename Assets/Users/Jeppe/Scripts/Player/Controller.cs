using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	CharacterController controller;
	Camera playerCam;
	Vector3 moveDir;

	float moveSpeed;

	[SerializeField] float walkSpeed = 5;
	[SerializeField] float runSpeed = 10;
	[SerializeField] float jumpForce = 5;
	[SerializeField] float playerGravity = 6.076f;

	Vector2 inputLook;
	Vector2 mouseLook;
	Vector2 smoothLook;

	[SerializeField] float mouseSens = 0.5f;
	[SerializeField] float mouseSmooth = 4.0f;
	[SerializeField] float xLookMin = -75.0f;
	[SerializeField] float yLookMin = 90.0f;

	//Awake
	void Awake () {

		controller = GetComponent<CharacterController> ();
		playerCam = gameObject.GetComponentInChildren<Camera> ();

	}

	// Update
	void Update () {

		moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDir = transform.TransformDirection (moveDir);

		//walk/run
		if (Input.GetKey (KeyCode.LeftShift)) {
			moveSpeed = runSpeed;
		} else {
			moveSpeed = walkSpeed;
		}
		//speed
		//moveDir *= moveSpeed;

		//gravity
		if (!controller.isGrounded)
			moveDir.y -= playerGravity;

		//jump
		if (controller.isGrounded && Input.GetKeyDown (KeyCode.Space)) {
			moveDir.y += jumpForce;
			Debug.Log (moveDir.y);
			print ("Hej");
		}
		
		//move
		controller.Move (moveDir * moveSpeed * Time.deltaTime);

		//mouse
		MouseLook ();
	}



	// MouseLook
	void MouseLook () {

		inputLook = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		inputLook = Vector2.Scale(inputLook, new Vector2(mouseSens * mouseSmooth, mouseSens * mouseSmooth));

		smoothLook.x = Mathf.Lerp(smoothLook.x, inputLook.x, 1f / mouseSmooth);
		smoothLook.y = Mathf.Lerp(smoothLook.y, inputLook.y, 1f / mouseSmooth);
		mouseLook += smoothLook;

		mouseLook.y = Mathf.Clamp(mouseLook.y, xLookMin, yLookMin);

		playerCam.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
	}
}