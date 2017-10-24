using UnityEngine;


public class UserInput : MonoBehaviour
{
    private CarControl m_Car; 

    float h;
    float accel;
    float brake;
    float handbrake;
    float revears;
    bool toggleDriftSystem;

   const string Horizontal_KEY = "Horizontal";
    const string Vertical_KEY = "Vertical";
    const string NITRO_KEY = "Fire2";
    const string HANDBRAKE_KEY = "Fire1";
    //const string DRIFT_SYSTEM_KEY = KeyCode.A;

    
    void OnEnable()
    {
        m_Car = GetComponent<CarControl>();
    }

    void Update()
    {
        toggleDriftSystem = Input.GetKeyUp(KeyCode.A);
        if (toggleDriftSystem)
            m_Car.toggleDriftSystem();
    }

 

    void FixedUpdate()
    {

        if (PlayerClass.inVehicle)
        {
            h = Input.GetAxis(Horizontal_KEY);
            
            accel = brake = Input.GetAxis(Vertical_KEY);
            handbrake = Input.GetAxis(HANDBRAKE_KEY);

        }
        else
        {
            h = 0;
            accel = 0;
        }
        m_Car.Move(h, accel, brake, handbrake); // pass the input to the car!

       
    }
   
}

