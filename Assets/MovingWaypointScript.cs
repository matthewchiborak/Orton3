using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWaypointScript : MonoBehaviour {

    public Transform[] positions;
    public float timeBeforeSwitch;
    private float timeOfLastSwitch;
    private int currentPosition;

	// Use this for initialization
	void Start ()
    {
        timeOfLastSwitch = Time.time;
        currentPosition = 0;
        transform.position = positions[0].position;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if((Time.time) - timeOfLastSwitch > timeBeforeSwitch)
        {
            timeOfLastSwitch = Time.time;
            currentPosition++;

            if(currentPosition >= positions.Length)
            {
                currentPosition = 0;
            }

            transform.position = positions[currentPosition].position;
        }
	}
}
