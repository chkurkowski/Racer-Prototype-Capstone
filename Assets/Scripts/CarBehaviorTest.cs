using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviorTest : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngleInput;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle;
    public float motorForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {

        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    public void GetInput() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    public void Steer() {

        steeringAngleInput = maxSteerAngle * horizontalInput;
        frontDriverW.steerAngle = steeringAngleInput;
        frontPassengerW.steerAngle = steeringAngleInput;
    }

    public void Accelerate() {

        frontDriverW.motorTorque = verticalInput * motorForce;
        frontPassengerW.motorTorque = verticalInput * motorForce;
    }

    public void UpdateWheelPoses() {

        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    public void UpdateWheelPose(WheelCollider collider, Transform transform) {

        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }
}
