using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private TrackManager trackManager;
    // Start is called before the first frame update
    void Start()
    {
        trackManager = GameObject.Find("Manager").GetComponent<TrackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            if (trackManager.checkIncrimate() == true)
            {
                trackManager.lap++;
            }
        }
    }
}
