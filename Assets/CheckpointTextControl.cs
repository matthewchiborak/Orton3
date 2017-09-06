using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointTextControl : MonoBehaviour {

    public Text checkPointReachedText;

    private bool currentlyDisplaying;
    public float durationOfTextOnScreen;
    private float timeTextAppeared;

    public void reachedCheckPoint()
    {
        timeTextAppeared = Time.time;
        checkPointReachedText.enabled = true;
        currentlyDisplaying = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(currentlyDisplaying)
        {
            if(Time.time - timeTextAppeared > durationOfTextOnScreen)
            {
                currentlyDisplaying = false;
                checkPointReachedText.enabled = false;
            }
        }
	}
}
