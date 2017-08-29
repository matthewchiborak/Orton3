using UnityEngine;
using System.Collections;

public class FinishLoadSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StoredInfoScript.persistantInfo.lastPosition = StoredInfoScript.persistantInfo.resetPosition;
        //StoredInfoScript.persistantInfo.resetMusic();
        StoredInfoScript.persistantInfo.showScreen();
        StoredInfoScript.persistantInfo.switchTracks();
        StoredInfoScript.persistantInfo.ignorePlayer = false;
	}
	
}
