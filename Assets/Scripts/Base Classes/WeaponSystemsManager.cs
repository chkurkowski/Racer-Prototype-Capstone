using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemsManager : MonoBehaviour
{

    [Tooltip("Length of ability cooldown in seconds.")] public float abilityRecharge = 5f;
    [Tooltip("Ability Prefab Slot")] public GameObject abilityObject;
    [Tooltip("Missle Spawn Location")] public Transform missileSpawnLocation;

    public bool canUseAbility = true;
    

    [Tooltip("The pickup slot.")] public GameObject pickupObject;
    [Tooltip("The Axis for using Abilities")] public string abilityAxis;
    [Tooltip("The Axis for using Pickups")] public string pickupAxis;

    [Header("Sludge Stuff")]
    public GameObject sludgeDropObject;
    public float sludgeDropRate = .5f;
    public float sludgeDropDuration = 3f;
    private float sludgeCurrentDuration = 0;
    public bool hasSludgePickup = false;
    public bool isUsingPickup = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown(abilityAxis) && canUseAbility)
        {
            GameObject spawnedMissile = Instantiate(abilityObject, missileSpawnLocation.transform.position, missileSpawnLocation.transform.rotation);

            spawnedMissile.GetComponent<MissileBehavior>().SetImmunePlayer(gameObject);

            canUseAbility = false;
            StartCoroutine(AbilityCooldown());
        }

        if(Input.GetButtonDown(pickupAxis) && hasSludgePickup)
        {
            isUsingPickup = true;
            hasSludgePickup = false;
            InvokeRepeating("DropSludge", 0, sludgeDropRate);

        }

        if(isUsingPickup)
        {
            sludgeCurrentDuration += Time.deltaTime;
            if(sludgeCurrentDuration >= sludgeDropDuration)
            {
                CancelInvoke("DropSludge");
                isUsingPickup = false;
                sludgeCurrentDuration = 0f;

            }
        }
      
    }

    public void DropSludge()
    {
        Instantiate(sludgeDropObject, transform.position, sludgeDropObject.transform.rotation);
    }

    private IEnumerator AbilityCooldown()
    {
        yield return new WaitForSecondsRealtime(abilityRecharge);
        canUseAbility = true;

    }
}
