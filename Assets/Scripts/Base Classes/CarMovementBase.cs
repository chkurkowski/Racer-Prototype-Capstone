using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementBase : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed;

    /*
    *These are the main grouping of member variables that will dictate how your car functions in particular.
    */
    [SerializeField]
    protected float topSpeed = 100f;
    [SerializeField]
    protected float acceleration = 10f;
    [SerializeField]
    protected float handling = 10f;
    [SerializeField]
    protected float maxHeat = 100f;
    [SerializeField]
    protected float collisionDamage = 100f;
    [SerializeField]
    protected float carWeight = 100f;

    void FixedUpdate()
    {
    	GetInput();
    }

    public virtual void GetInput()
    {
    	horizontalInput = Input.GetAxis("Horizontal");
    	verticalInput = Input.GetAxis("Vertical");
    }

    protected virtual void Accelerate()
    {
    	if(speed < topSpeed)
    	{
    		speed = verticalInput * acceleration;
    	}
    }

    protected virtual void Braking()
    {

    }

    protected virtual void Drifting()
    {

    }

    protected virtual void Decelerate()
    {

    }


}
