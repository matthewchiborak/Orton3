using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerFinalCutsceneScript : MonoBehaviour {

    public int triggerValue;
    public string sceneToLoadOn;

	
	
	// Update is called once per frame
	void Update ()
    {
	    if(StoredInfoScript.persistantInfo.getProgressLevel() == triggerValue)
        {
            StoredInfoScript.persistantInfo.IncreaseProgress();
            Destroy(StoredInfoScript.persistantInfo.getGameObject());
            SceneManager.LoadScene(sceneToLoadOn, LoadSceneMode.Single);
        }
            
	}
}
