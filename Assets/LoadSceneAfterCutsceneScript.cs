using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneAfterCutsceneScript : MonoBehaviour {

    public int progressRequired;
    public string sceneToLoad;
    public Vector3 locationForShawn;

	
	
	// Update is called once per frame
	void Update ()
    {
	    if(StoredInfoScript.persistantInfo.getProgressLevel() == progressRequired)
        {
            StoredInfoScript.persistantInfo.getPlayerTransform().position = locationForShawn;

            StoredInfoScript.persistantInfo.lastPosition = StoredInfoScript.persistantInfo.resetPosition;

            StoredInfoScript.persistantInfo.currentScene = sceneToLoad;
            StoredInfoScript.persistantInfo.lastLoadLocation = locationForShawn;

            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
	}
}
