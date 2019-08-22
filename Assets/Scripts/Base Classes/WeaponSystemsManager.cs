using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemsManager : MonoBehaviour
{

    [Tooltip("Length of ability cooldown in seconds.")] public float abilityRecharge = 5f;
    [Tooltip("Ability Prefab Slot")] public GameObject abilityObject;
    [Tooltip("Missle Spawn Location")] public Transform missileSpawnLocation;

    private bool canUseAbility = true;

    [Tooltip("The pickup slot.")] public GameObject pickupObject;
    [Tooltip("The Axis for using Abilities")] public string abilityAxis;
    [Tooltip("The Axis for using Pickups")] public string pickupAxis;

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
       // if (Input.GetButtonDown(pickupAxis))
       // {
         //   pickupObject.SendMessage("UsePickup");
      //  }
    }


    private IEnumerator AbilityCooldown()
    {
        yield return new WaitForSecondsRealtime(abilityRecharge);
        canUseAbility = true;

    }
}
