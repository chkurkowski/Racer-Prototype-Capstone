using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgePickupBehavior : MonoBehaviour
{
    public float animateSpeed = 0.01f;
    public float animateUpDownAmount = .01f;
    private int animateUpDownDirection = 1;
    public float animateSpinAmount = .01f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PickupMovement", 0, animateSpeed);
    }


    public void UsePickup()
    {

    }
}
