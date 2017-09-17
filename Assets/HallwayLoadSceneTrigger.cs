using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class HallwayLoadSceneTrigger : MonoBehaviour {

    public Image blackScreen;
    public Text loadingText;

    private List<string> parsedLine;

    public LevelID sceneToLoad;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string assetText;
            parsedLine = new List<string>();

            using (var streamReader = new StreamReader("Assets/Resources/Save.txt", Encoding.UTF8))
            {
                assetText = streamReader.ReadToEnd();
            }

            //Parse
            parsedLine.AddRange(assetText.Split("~"[0]));
            
            blackScreen.enabled = true;
            loadingText.enabled = true;
            
            string data;

            
            data = ((int)sceneToLoad).ToString() + "~0~" + parsedLine[2] + "~" + parsedLine[3] + "~" + parsedLine[4] + "~" + parsedLine[5] + "~" + parsedLine[6] + "~" + parsedLine[7] + "~" + parsedLine[8];
            
            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);

            SceneManager.LoadScene(((SceneNames)((int)sceneToLoad)).ToString(), LoadSceneMode.Single);
        }
    }
}
