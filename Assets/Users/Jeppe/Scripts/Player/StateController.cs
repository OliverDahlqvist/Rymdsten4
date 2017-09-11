using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

	CharacterController controller;
	Camera playerCam;

	private float moveX;
	private float moveY;
	private float yVelocity;
	public float gravity = 6.076f;

	public float walkSpeed = 5f;
	public float runSpeed = 10f;
	private float moveSpeed;
	public float jumpForce = 2f;
	public float jetForce = 0.3f;

	private float speedSmoothVelocity;
	private float speedTransitionTime;

	Vector3 inputDir;
	Vector3 moveDir;
	private float currentSpeed;

	Vector2 inputLook;
	Vector2 mouseLook;
	Vector2 smoothLook;

	public float mouseSens = 0.5f;
	public float mouseSmooth = 4.0f;
	public float xLookMin = -75.0f;
	public float yLookMin = 90.0f;

	[SerializeField]
	KeyCode[] inputs;

	//Enum
	enum PlayerState
	{
		IDLE,
		WALK,
		RUN,
		JUMP,
		JET
	};
	PlayerState currentState = PlayerState.IDLE;

	Vector3 lastPos = Vector3.zero;

	// Awake
	void Awake () {

		controller = GetComponent<CharacterController> ();
		playerCam = GetComponentInChildren<Camera> ();
	}
	
	// Update
	void Update () {

		moveX = Input.GetAxis ("Horizontal");
		moveY = Input.GetAxis ("Vertical");

		inputDir = new Vector3 (moveX, yVelocity, moveY);
		currentSpeed = Mathf.SmoothDamp (currentSpeed, moveSpeed, ref speedSmoothVelocity, speedTransitionTime);
		moveDir = transform.rotation * inputDir * currentSpeed;

		moveDir.y -= gravity * Time.deltaTime;



		switch (currentState)
		{
		//IDLE
		case PlayerState.IDLE:
			
			for (int i = 0; i < inputs.Length; i++) {
				if (Input.GetKeyDown (inputs [i]))
					currentState = PlayerState.WALK;
			}

			if (Input.GetKeyDown (KeyCode.Space)) {

				yVelocity += jumpForce;
				currentState = PlayerState.JUMP;
			}
				

			break;



		//WALK
		case PlayerState.WALK:
			
			moveSpeed = walkSpeed;
			controller.Move (moveDir * Time.deltaTime);

			//run
			if (Input.GetKey (KeyCode.LeftShift))
				currentState = PlayerState.RUN;
			//idle
			if (lastPos == transform.position)
				currentState = PlayerState.IDLE;

			lastPos = transform.position;

			break;



		//RUN
		case PlayerState.RUN:
			
			moveSpeed = runSpeed;
			controller.Move (moveDir * Time.deltaTime);

			//walk
			if (Input.GetKeyUp (KeyCode.LeftShift))
				currentState = PlayerState.WALK;
			
			break;



		//JUMP
		case PlayerState.JUMP:

			//walk
			if (controller.isGrounded)
				currentState = PlayerState.WALK;
			
			break;
		}


		MouseLook ();
		Debug.Log (currentState);
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



////Jetpack
//if (Input.GetKeyDown (KeyCode.J)) {
//	jetPack= !jetPack;
//	RandomTest.battery += 1;
//}
//if (jetPack && Input.GetKey (KeyCode.Space))
//	Jump ();



//// Jump
//void Jump() {
//
//	yVelocity += jetForce;
//
//	if (controller.isGrounded == false)
//		yVelocity -= thirtyeightProcentOfEarthGravity * Time.deltaTime;
//}