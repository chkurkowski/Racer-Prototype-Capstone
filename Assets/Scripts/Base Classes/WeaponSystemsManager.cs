using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemsManager : MonoBehaviour
{

    [Tooltip("Length of ability cooldown in seconds.")] public float abilityRecharge = 5f;
    [Tooltip("Ability Prefab Slot")] public GameObject abilityObject;
    [Tooltip("Missle Spawn Location")] public Transform missileSpawnLocation;

    public bool canUseAbility = true;

    [Tooltip("The Axis for using Abilities")] public string abilityAxis;
    [Tooltip("The Axis for using Pickups")] public string pickupAxis;

    [Header("Sludge Stuff")]
    public GameObject lavaDropObject;
    public float lavaDropRate = .5f;
    public float lavaDropDuration = 3f;
    private float lavaCurrentDuration = 0;
    public bool lavaSludgePickup = false;
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

        if(Input.GetButtonDown(pickupAxis) && lavaSludgePickup)
        {
            isUsingPickup = true;
            lavaSludgePickup = false;
            InvokeRepeating("DropLava", 0, lavaDropRate);
        }

        if(isUsingPickup)
        {
            lavaCurrentDuration += Time.deltaTime;
            if(lavaCurrentDuration >= lavaDropDuration)
            {
                CancelInvoke("DropLava");
                isUsingPickup = false;
                lavaCurrentDuration = 0f;
            }
        }   
    }

    public void DropLava()
    {
       GameObject spawnedLavaDrop =  Instantiate(lavaDropObject, transform.position, lavaDropObject.transform.rotation);
        spawnedLavaDrop.GetComponent<LavaDropBehavior>().immunePlayer = gameObject;
    }

    private IEnumerator AbilityCooldown()
    {
        yield return new WaitForSecondsRealtime(abilityRecharge);
        canUseAbility = true;

    }
}
