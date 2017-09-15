using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevatationScript : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 endPos;
    public float durationOfCycle;

    private float timeLastCycleStarted;

    private bool movingUp;

	// Use this for initialization
	void Start ()
    {
        timeLastCycleStarted = Time.time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(movingUp)
        {
            transform.position = Vector3.Lerp(endPos, startPos, (Time.time - timeLastCycleStarted) / durationOfCycle);

            if ((Time.time - timeLastCycleStarted) > durationOfCycle)
            {
                timeLastCycleStarted = Time.time;
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - timeLastCycleStarted) / durationOfCycle);

            if((Time.time - timeLastCycleStarted) > durationOfCycle)
            {
                timeLastCycleStarted = Time.time;
                movingUp = true;
            }
        }
	}
}
