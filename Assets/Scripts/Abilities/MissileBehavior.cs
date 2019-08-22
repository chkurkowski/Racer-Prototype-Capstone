using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float missileSpeed = 400f;
    public GameObject explosionPrefab;
    public float fuseTime = 5f;
    private GameObject immunePlayer;
    // Start is called before the first frame update

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(0, 0, missileSpeed);
    }

    public void ExplodeMissile()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void SetImmunePlayer(GameObject newPlayer)
    {
        immunePlayer = newPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != immunePlayer)
        {
            ExplodeMissile();
        }
    }



}
