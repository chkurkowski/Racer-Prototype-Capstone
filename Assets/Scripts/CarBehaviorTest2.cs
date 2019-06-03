using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviorTest2 : MonoBehaviour
{
    
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strengthCoefficient;
    public float maxTurn;
    private float throttle;
    private float steer;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Force();
        Steering();
    }

    protected virtual void Force()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = strengthCoefficient * throttle;
        }
    }

    protected virtual void Steering()
    {
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * steer;
        }
    }
}
