using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float missileSpeed;
    private GameObject immunePlayer;
    // Start is called before the first frame update

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(0, 0, missileSpeed);

    }

    public void IgniteMissile()
    {
       
    }

    public void SetImmunePlayer(GameObject newPlayer)
    {
        immunePlayer = newPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != immunePlayer)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("Boom!");
        Destroy(gameObject);
    }

}
