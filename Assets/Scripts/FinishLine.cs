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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            if (trackManager.CheckIncrimate() == true)
            {
                trackManager.AssignIncrimate(false);
                if(trackManager.lap < 3)
                {
                   trackManager.lap++; 
                }
                else if (trackManager.lap >= 3)
                {
                    Debug.Log("Race Done");
                }
            }
        }
    }
}
