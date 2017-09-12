using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class LoadSceneTriggerScript : MonoBehaviour {

    public Image blackScreen;
    public Text loadingText;

    public LevelID sceneToLoad;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackScreen.enabled = true;
            loadingText.enabled = true;

            /*if (other.GetComponentInParent<NewStoredInfoScriptOrton>())
            {
                data = ((int)levelID).ToString() + "~" + checkPointID.ToString() + "~" + other.GetComponentInParent<NewStoredInfoScriptOrton>().getStateString() + "~" + cordsOfDestination.x.ToString() + "~" + cordsOfDestination.y.ToString();
                rankData = other.GetComponentInParent<NewStoredInfoScriptOrton>().getRankStrin();
            }
            else
            {
                data = ((int)levelID).ToString() + "~" + checkPointID.ToString() + "~" + other.GetComponentInParent<NewStoredInfoScript>().getStateString() + "~" + cordsOfDestination.x.ToString() + "~" + cordsOfDestination.y.ToString();
                rankData = other.GetComponentInParent<NewStoredInfoScript>().getRankStrin();
            }*/

            string data;

            if (other.GetComponentInParent<NewStoredInfoScriptOrton>())
            {
                data = ((int)sceneToLoad).ToString() + "~0~" + other.GetComponentInParent<NewStoredInfoScriptOrton>().getStateString() + "~0~0";
            }
            else
            {
                data = ((int)sceneToLoad).ToString() + "~0~" + other.GetComponentInParent<NewStoredInfoScript>().getStateString() + "~0~0";
            }
            
            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);
            
            SceneManager.LoadScene(((SceneNames)((int)sceneToLoad)).ToString(), LoadSceneMode.Single);
        }
    }
}
