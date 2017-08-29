using UnityEngine;
using System.Collections;

public class StopAlertMusicScript : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update ()
    {
        StoredInfoScript.persistantInfo.lastPosition = StoredInfoScript.persistantInfo.resetPosition;
	}
}
