using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsBehavior : MonoBehaviour
{
    //list of suspension points and driving points, not currently used, but could be useful for checking if the car is making a jump
    //or for making frontwheel vs rearwheel drive vehicles later
    //public List<SuspensionPoint> suspension;
    //public List<SuspensionPoint> drivingPoints;

    //Downward force applied to vehicle to keep it on the ground
    public float downForce = 100;

    //Call for the rigidbody of the car
    private Rigidbody carRB;

    public bool isOverheated = false; // Borissov - Whether or not the car is overheated. (7-15-2019)

    //The transform position where driving and braking forces are applies
    public Transform drivePos;

    //forces applied by each action
    public float driveForce, brakeForce, turnForce;

    float deadZone = .1f;

    public GameObject[] hoverPoints;

    public float groundedDrag = 3.0f;

    [Range(0, 1)]
    public float reverseSpeed = 0.25f;

    //the input fields for each action
    private float driveInput, brakeInput, turnInput;

    //the grip value that controls how much the car slides when turning
    public float carGrip;

    //current speed
    float currSpeed;


    //checks how "compressed" each point is, 0 for fully extended, 1 for fully compressed
    private float compression;


    GameObject suspensionPoint;

    //value for how long each suspension point should be
    public float suspensionLength = 3;

    //how much force is applied to keep vehicle lifted
    public float suspensionForce = 50;


    public bool grounded;


    public float maxSpeed = 500.0f;

    //The forward value without the upward component
    Vector3 flatFwd;

    //Input Axis for controller support
    public string horizontalAxis; //left stick
    //public string verticalAxis;// W and S
    public string verticalForwardAxis;//right trigger
    public string verticalBackwardAxis;//left trigger
    public float forwardInput;
    public float backwardInput;

    private CarHeatBehaviour carHeatManager;


    private void Awake()
    {
        //stores the rigidbody value of the car
        carRB = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        //lowers the center of mass of the vehicle to limit flipping
        carHeatManager = gameObject.GetComponent<CarHeatBehaviour>();
        carRB.centerOfMass = new Vector3(0, -1, 0);
    }

    private void FixedUpdate()
    {
       // driveInput = brakeInput = Input.GetAxis(verticalAxis);
        turnInput = Input.GetAxis(horizontalAxis);

        forwardInput = Input.GetAxis(verticalForwardAxis);
        backwardInput = Input.GetAxis(verticalBackwardAxis);


        //clamps braking and throttle inputs to needed values
        driveInput = Mathf.Clamp(driveInput, -reverseSpeed, 1);
        brakeInput = Mathf.Clamp(brakeInput, -1, 0);
        //clamping for controller triggers, probably isn't needed.
        forwardInput = Mathf.Clamp(forwardInput, -reverseSpeed, 1);
       // backwardInput = Mathf.Clamp(backwardInput, 0, 1);

        //Testing method, launches the car into the air on button press to test suspension
        if (Input.GetKeyDown(KeyCode.Space))
        {
            carRB.AddForceAtPosition(Vector3.up * 15,
                new Vector3(transform.position.x + (Random.value * 5), transform.position.y + (Random.value * 5), transform.position.z + (Random.value * 5)),
                ForceMode.Impulse);
        }


        //Checks if the car is touching the ground at all positions
        grounded = false;

        //Consolidated suspension system into this script. Draws a downward raycast at each point to check for collisions and applies an upward force if one is found.
        for (int i = 0; i < hoverPoints.Length; i++)
        {
            suspensionPoint = hoverPoints[i];

            Ray ray = new Ray(suspensionPoint.transform.position, -transform.up);
            RaycastHit hit;

            Debug.DrawRay(suspensionPoint.transform.position, -transform.up, Color.red);



            if (Physics.Raycast(ray, out hit, suspensionLength))
            {
                compression = (suspensionLength - hit.distance) / suspensionLength;
                grounded = true;
            }
            else
            {
                compression = 0;
            }

            if (compression > 0)
            {
                Vector3 force = Vector3.up * compression * suspensionForce;
                carRB.AddForceAtPosition(force, transform.position, ForceMode.Acceleration);
            }
        }

        //adjusts drag value based on if car is in the air or on the ground
        if (grounded)
        {
            carRB.drag = groundedDrag;
        }
        else
        {
            carRB.drag = 0.1f;
        }



        carRB.AddForce(-Vector3.up * downForce, ForceMode.Acceleration);

        //calculates out the forward vector of the vehicle so it won't fly upward when throttle is applied
        Vector3 carFwd = transform.TransformDirection(Vector3.forward);
        Vector3 tempFwd = new Vector3(carFwd.x, 0, carFwd.z);

        flatFwd = Vector3.Normalize(tempFwd);

        throttle();
        brake();
        turn();
        slideControl();


        //Checks if car is above the max speed and reduces it's velocity if so
        if (carRB.velocity.sqrMagnitude > (carRB.velocity.normalized * maxSpeed).sqrMagnitude)
        {
            carRB.velocity = carRB.velocity.normalized * maxSpeed;
        }
    }

    //applies forward force based on inputs
    public void throttle()
    {
        /*if(forwardInput > 0f)
        {
            Debug.Log("Right Trigger was pressed!");
            carRB.AddForceAtPosition(flatFwd * driveForce * forwardInput * Time.deltaTime, drivePos.position); // Right Trigger
        }
        else if(backwardInput > 0f)
        {
            Debug.Log("Left Trigger was pressed!");
            carRB.AddForceAtPosition(flatFwd * driveForce * (-backwardInput * reverseSpeed) * Time.deltaTime, drivePos.position); // Left Trigger

        }*/
        // carRB.AddForceAtPosition(flatFwd * driveForce * driveInput * Time.deltaTime, drivePos.position); //used for W and S

        if(!carHeatManager.isOverheated) // Borissov - 7/15/2019
        {
            carRB.AddForceAtPosition(flatFwd * driveForce * forwardInput * Time.deltaTime, drivePos.position); //used for W and S and arrow keys
        }

        //if (forwardInput > deadZone)
      //  {
          //  carRB.AddForce(flatFwd * driveForce * forwardInput); //used for W and S and arrow keys
        //}
    }

    //applies backward force based on inputs
    public void brake()
    {
        if (carRB.velocity.z > 0)
        {
            carRB.AddForceAtPosition(flatFwd * brakeForce * brakeInput, drivePos.position);
            //carRB.AddRelativeForce(Vector3.down * brakeForce * brakeInput * Time.deltaTime);
        }
    }

    //applies a torque to rotate the vehicle the appropriate amount
    public void turn()
    {

        //Vector3 turnVec = ((transform.up * turnForce) * turnInput) * 800.0f;

        //carRB.AddTorque(turnVec);
        if (turnInput != 0)
        {
            carRB.AddRelativeTorque(Vector3.up * turnInput * turnForce);
        }
    }


    //applies an inverse sideways force to limit vehicle sliding when turning
    public void slideControl()
    {

        Vector3 tempVel = new Vector3(carRB.velocity.x, 0, carRB.velocity.z);
        Vector3 flatVel = Vector3.Normalize(tempVel);

        currSpeed = flatVel.magnitude;

        float sliding = Vector3.Dot(transform.right, flatVel);

        float slideAmount = Mathf.Lerp(100, carGrip, currSpeed * .02f);

        Vector3 slideControl = transform.right * (-sliding * slideAmount);
    }

}
