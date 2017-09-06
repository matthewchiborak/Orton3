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

            string data = ((int)sceneToLoad).ToString() + "~0";

            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);
            
            SceneManager.LoadScene(((SceneNames)((int)sceneToLoad)).ToString(), LoadSceneMode.Single);
        }
    }
}
