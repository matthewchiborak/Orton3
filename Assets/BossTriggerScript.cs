using UnityEngine;
using System.Collections;

public class BossTriggerScript : MonoBehaviour {

    public GameObject bossFightController;
    public int progressLevelRequired;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (progressLevelRequired == StoredInfoScript.persistantInfo.getProgressLevel())
        {
            
            StoredInfoScript.persistantInfo.IncreaseProgress();
            bossFightController.SetActive(true);
        }
    }
}
