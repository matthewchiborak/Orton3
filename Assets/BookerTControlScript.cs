using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class BookerTControlScript : MonoBehaviour {

    public string sceneToLoad;

    public float speed;

    public float maxHealth;
    private float currentHealth;

    public Transform[] patrolWayPoints;
    private int wayPointIndex;

    public UnityEngine.AI.NavMeshAgent nav;

    private List<string> parsedLine;

    public AudioSource bookerHurtSource;

    public Image healthBarFront;
    public GameObject blackScreen;

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
        wayPointIndex = 1;
        nav.speed = speed;
        nav.Resume();
        nav.destination = patrolWayPoints[1].position;
    }
	
	public void damageBooker(float damage)
    {
        currentHealth -= damage;
        bookerHurtSource.Play();

        healthBarFront.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            blackScreen.SetActive(true);

            //Update the save file
            string assetText;
            parsedLine = new List<string>();

            using (var streamReader = new StreamReader("Assets/Resources/Save.txt", Encoding.UTF8))
            {
                assetText = streamReader.ReadToEnd();
            }

            //Parse
            parsedLine.AddRange(assetText.Split("~"[0]));

            parsedLine[1] = "6";

            string data = parsedLine[0] + "~" + parsedLine[1] + "~" + parsedLine[2] + "~" + parsedLine[3] + "~" + parsedLine[4] + "~" + parsedLine[5] + "~" + parsedLine[6] + "~" + parsedLine[7] + "~" + parsedLine[8];

            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);

            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}
