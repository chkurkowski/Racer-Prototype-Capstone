using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHeatBehaviour : MonoBehaviour
{

    public float heatMaxConstant = 150; // Borissov - Heat cannot go past this point.
    public float heatLimit = 100; // Borissov - Once heat goes past this, car is considered "overheated".
    public const float heatMinConstant = 0; // Borissov - Heat cannot go below this.
    public float heatCurrent = 0; // Borissov - Current Heat.
    public float cooldownAmount = 1;
    public bool isOverheated = false; // Borissov - Tells other scripts if the car is overheated.

    private CarPhysicsBehavior carPhysicsController;
    private BoostBehavior carBoostController;

    void Start()
    {
        carPhysicsController = gameObject.GetComponent<CarPhysicsBehavior>();
        carBoostController = gameObject.GetComponent<BoostBehavior>();
    }

    void Update()
    {

    }

    public void UpdateHeat()
    {
        heatCurrent -= cooldownAmount;
    }

    public void CheckHeat()
    {
       isOverheated = heatCurrent >= heatMaxConstant ? true : false;
    }


}
