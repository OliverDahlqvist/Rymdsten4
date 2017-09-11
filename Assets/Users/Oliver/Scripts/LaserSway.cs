using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSway : MonoBehaviour {
    float mouseX;
    float mouseY;
    Quaternion rotationSpeed;
    public float speed;
    private float offset = -4.985f;

	void Update () {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        rotationSpeed = Quaternion.Euler(-mouseY, offset - mouseX, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotationSpeed, speed * Time.deltaTime);
    }
}
