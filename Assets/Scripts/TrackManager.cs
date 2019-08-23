using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public int lap = 0;
    private bool canIncrimate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIncrimate()
    {
        return canIncrimate;
    }

    public void AssignIncrimate(bool IncrimateValue)
    {
        canIncrimate = IncrimateValue;
    }
}
