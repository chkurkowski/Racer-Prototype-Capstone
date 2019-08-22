using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosionBehavior : MonoBehaviour
{
    public float growthRate = 0.01f;
    public float growthAmount = 0.1f;
    public float explosionLifeLength = 0.5f;
    private float explosionDamage;
    private GameObject immunePlayer;

    void Start()
    {
        InvokeRepeating("Grow", 0, growthRate);
        Destroy(gameObject, explosionLifeLength);
    }

    public void Grow()
    {
        transform.localScale += new Vector3(growthAmount, growthAmount, growthAmount);
    }

    public void SetImmunePlayer(GameObject newImmunePlayer)
    {
        immunePlayer = newImmunePlayer;
    }

    public void SetExplosionDamage(float damage)
    {
        explosionDamage = damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject != immunePlayer)
        {
            //place check to see if the "other" gameobject has a heat/health code
            Debug.Log(explosionDamage + " damage was dealt to: " + collision.gameObject.name + "!");
        }
        else if(collision.gameObject == immunePlayer)
        {
            Debug.Log("Player Detected by explosion!");

        }
    }
}
