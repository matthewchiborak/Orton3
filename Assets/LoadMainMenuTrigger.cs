using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class LoadMainMenuTrigger : MonoBehaviour {

    public Image blackScreen;
    public Text loadingText;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackScreen.enabled = true;
            loadingText.enabled = true;

            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
