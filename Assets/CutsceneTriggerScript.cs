using UnityEngine;
using System.Collections;

public class CutsceneTriggerScript : MonoBehaviour {

    public int cutsceneToTrigger;
    public int progressLevelRequired;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(progressLevelRequired == StoredInfoScript.persistantInfo.getProgressLevel())
        {
            StoredInfoScript.persistantInfo.PlayCutscene(cutsceneToTrigger);
        }
	}
}
