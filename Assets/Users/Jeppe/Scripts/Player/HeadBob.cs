using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    public Transform head;
    public float headBobFrequenzy = 1.5f;
    public float headBobSwayAngle = 0.5f;
    public float headBobHeight = 0.3f;
    public float headBobSideMovement = 0.05f;
    public float headBobSpeedMulti = 0.3f;
    public float headBobStridSpeedLengthen = 0.3f;
    public float jumpLandMove = 3f;
    public float jumpLandTilt = 60f;
    
    //FPSCharacter fpsCharacter;
    Vector3 originalLocalPosition;
    float nextStepTime = 0.5f;
    float headBobCycle = 0f;
    float headBobFade = 0f;
    float springPosition = 0f;
    float springVelocity = 0f;
    float springElastic = 1.1f;
    float springDampen = 0.8f;
    float springVelocityThreshold = 0.05f;
    float springPositionThreshold = 0.05f;
    Vector3 previousPosition;
    Vector3 previousVelocity = Vector3.zero;
    bool previouslyGrounded;
    


    // Use this for initialization
    void Start() {

        originalLocalPosition = head.localPosition;
        //fpsCharacter = GetComponent<FPSCharacter>();

        previousPosition = GetComponent<Rigidbody>().position;
    }



    // Update is called once per frame
    void FixedUpdate() {

        //Get current frame velocity and dir
        Vector3 velocity = (GetComponent<Rigidbody>().position - previousPosition) / Time.deltaTime;
        //Compare current frame velocity with previous frame velocity
        Vector3 velocityChange = velocity - previousVelocity;

        previousPosition = GetComponent<Rigidbody>().position;
        previousVelocity = velocity;

        //Y bob and spring back force
        springVelocity -= velocityChange.y;
        springVelocity -= springPosition * springElastic;
        springVelocity *= springDampen;

        springPosition += springVelocity * Time.deltaTime;
        springPosition = Mathf.Clamp(springPosition, -0.3f, 0.3f);

        //If less then threshold make zero
        if (Mathf.Abs(springVelocity) < springVelocityThreshold && Mathf.Abs(springPosition) < springPositionThreshold) {
            springVelocity = 0;
            springPosition = 0;
        }

        //Get magnitude
        float flatVelocity = new Vector3(velocity.x, 0.0f, velocity.z).magnitude;
        //Get strides
        float strideLengthen = 1 + (flatVelocity * headBobStridSpeedLengthen);
        //Increase headbob cycle from current velocity div by stride length
        headBobCycle += (flatVelocity / strideLengthen) * (Time.deltaTime / headBobFrequenzy);

        //Sinecurve factor for headbob
        float bobFactor = Mathf.Sin(headBobCycle * Mathf.PI * 2);
        //Sway factor
        float bobSwayFactor = Mathf.Sin(Mathf.PI * (2 * headBobCycle + 0.5f));

        //Fix inversion
        bobFactor = 1 - (bobFactor * 0.5f + 1);
        //Smooth start and end och bob cycle
        bobFactor *= bobFactor;

        //Smoothing fade
        if (new Vector3(velocity.x, 0.0f, velocity.z).magnitude < 0.1f) {
            headBobFade = Mathf.Lerp(headBobFade, 0.0f, Time.deltaTime);
        }
        else {
            headBobFade = Mathf.Lerp(headBobFade, 1.0f, Time.deltaTime);
        }

        //Bob height multiplyer
        float speedHeightFactor = 1 + (flatVelocity * headBobSpeedMulti);

        float xPos = -headBobSideMovement * bobSwayFactor;
        float yPos = springPosition * jumpLandMove + bobFactor * headBobHeight * headBobFade * speedHeightFactor;

        //Calc headtilt
        float xTilt = -springPosition * jumpLandTilt;
        float zTilt = bobSwayFactor * headBobSwayAngle * headBobFade;

        head.localPosition = originalLocalPosition + new Vector3(xPos, yPos, 0.0f);
        head.localRotation = Quaternion.Euler(xTilt, 0.0f, zTilt);

        //if (fpsCharacter.isGrounded) {

        //    if (!previouslyGrounded) {

        //        nextStepTime = headBobCycle + 0.5f;
        //    }
        //    else {

        //        if (headBobCycle > nextStepTime) {

        //            nextStepTime = headBobCycle + 0.5f;
        //        }
        //    }

        //    previouslyGrounded = true;
        //}
        //else {

        //    previouslyGrounded = false;
        //}
    }
}
