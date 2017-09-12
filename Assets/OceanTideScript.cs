using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanTideScript : MonoBehaviour {

    public Vector3 startPoint;
    public Vector3 endPoint;
    public float durationOfPhase;
    public float durationOfPause;

    private int phase;
    private float timePhaseStarted;

	// Use this for initialization
	void Start ()
    {
        phase = 0;
        timePhaseStarted = Time.time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(phase == 0)
        {
            transform.position = Vector3.Slerp(startPoint, endPoint, (Time.time - timePhaseStarted) / durationOfPhase);
            if((Time.time - timePhaseStarted) > durationOfPhase)
            {
                timePhaseStarted = Time.time;
                phase++;
            }
        }
        else if (phase == 1)
        {
            if ((Time.time - timePhaseStarted) > durationOfPause)
            {
                timePhaseStarted = Time.time;
                phase++;
            }
        }
        else if(phase == 2)
        {
            transform.position = Vector3.Slerp(endPoint, startPoint, (Time.time - timePhaseStarted) / durationOfPhase);
            if ((Time.time - timePhaseStarted) > durationOfPhase)
            {
                timePhaseStarted = Time.time;
                phase++;
            }
        }
        else if (phase == 3)
        {
            if ((Time.time - timePhaseStarted) > durationOfPause)
            {
                timePhaseStarted = Time.time;
                phase = 0;
            }
        }
    }
}
