﻿using System.Collections;
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
    private SimpleCameraFollow mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Sets variables for gameplay
        canBoost = true;
        carScript = GetComponent<CarPhysicsBehavior>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<SimpleCameraFollow>();
        iniSpeed = carScript.driveForce;
        boostCharge = iniSpeed + boostAmount;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks for Boost Input and calls corutine if possible
        if (Input.GetButtonDown("Fire2") && canBoost)
        {
            canBoost = false;
            StartCoroutine(Boost());
        }
    }
    
    private IEnumerator Boost()
    {
        while (carScript.driveForce < boostCharge)
        {
            // Lerps the camera follow speed to be slower
            mainCamera.GetComponent<SimpleCameraFollow>().followSpeed =
            Mathf.Lerp(10, mainCamera.followSpeed, Time.deltaTime);

            // Adds speed to vehicles drive force over time
            carScript.driveForce += accelerationAdd;
            yield return new WaitForSeconds(0.05f);
        }

        // Time while max boost is active
        yield return new WaitForSeconds(boostTime);

        while (carScript.driveForce > iniSpeed)
        {
            // Lerps the camera follow speed to be faster
            mainCamera.GetComponent<SimpleCameraFollow>().followSpeed =
            Mathf.Lerp(mainCamera.followSpeed, 34, 6f * Time.deltaTime);

            // Subtracts the vehicles drive force over time
            carScript.driveForce -= decelerationSub;
            yield return new WaitForSeconds(0.05f);
        }
        canBoost = true;
    }
}
