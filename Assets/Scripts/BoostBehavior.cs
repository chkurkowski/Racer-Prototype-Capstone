using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBehavior : MonoBehaviour
{
    public int boostTime;
    public float boostAmount;
    public float accelerationAdd;
    public float decelerationSub;
    private float boostCharge;
    private float iniSpeed;
    private bool canBoost;
    private CarPhysicsBehavior carScript;

    // Start is called before the first frame update
    void Start()
    {
        canBoost = true;
        carScript = GetComponent<CarPhysicsBehavior>();
        iniSpeed = carScript.driveForce;
        boostCharge = iniSpeed + boostAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && canBoost)
        {
            canBoost = false;
            StartCoroutine(Boost());
        }

        if (boostCharge < boostAmount && canBoost == true)
        {
            //boostCharge = (boostCharge + 0.05f) + Time.fixedDeltaTime;
        }
    }
    
    private IEnumerator Boost()
    {
        while (carScript.driveForce < boostCharge)
        {
            carScript.driveForce += accelerationAdd;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(boostTime);
        while (carScript.driveForce > iniSpeed)
        {
            carScript.driveForce -= decelerationSub;
            yield return new WaitForSeconds(0.05f);
        }
        canBoost = true;
    }
}
