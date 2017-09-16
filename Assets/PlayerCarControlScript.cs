using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class PlayerCarControlScript : MonoBehaviour {

    public string sceneToLoad;

    private int cooldown;
    public Animator anim;

    public GameObject grenade;
    private bool fireShot;

    public Transform shotPoint;
    public GameObject orton;

    private GameObject tempGren;

    public AudioSource grenadeLauncherSource;

    private float totalMouseX;
    private float totalMouseY;

    public float speed;

    public Transform[] patrolWayPoints;
    private int wayPointIndex;

    public UnityEngine.AI.NavMeshAgent nav;

    public ParticleSystem explosion;
    public float durationBeforeReset;
    private float timeOfReset;
    private bool midReset;

    public AudioSource explosionSource;

    private List<string> parsedLine;

    // Use this for initialization
    void Start ()
    {
        cooldown = 0;
        totalMouseX = 0;
        totalMouseY = 0;
        wayPointIndex = 0;
        nav.speed = speed;
        nav.Resume();
        nav.destination = patrolWayPoints[0].position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(midReset)
        {
            if((Time.time - timeOfReset) > durationBeforeReset)
            {
                //Update the save file
                string assetText;
                parsedLine = new List<string>();

                using (var streamReader = new StreamReader("Assets/Resources/Rank.txt", Encoding.UTF8))
                {
                    assetText = streamReader.ReadToEnd();
                }

                //Parse
                parsedLine.AddRange(assetText.Split("~"[0]));
                
                int deaths = int.Parse(parsedLine[2]);
                deaths++;

                string data = parsedLine[0] + "~" + parsedLine[1] + "~" + deaths.ToString();

                System.IO.File.WriteAllText("Assets/Resources/Rank.txt", data);

                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }

            return;
        }
        
        totalMouseX += Input.GetAxis("Mouse X");
        totalMouseY += Input.GetAxis("Mouse Y");
        totalMouseX = Mathf.Clamp(totalMouseX, -130f / 4, 130f / 4);
        totalMouseY = Mathf.Clamp(totalMouseY, -22f / 2, 22f / 2);

        orton.transform.localRotation = Quaternion.Euler(totalMouseY * 2, totalMouseX * 2 + 180, 0);

        if (cooldown <= 0 && Input.GetButtonDown("Fire1"))
        {
            anim.Play("Armature|Shoot", -1, 0f);
            fireShot = true;
            cooldown = 60;
        }

        if(cooldown < 30 && fireShot)
        {
            grenadeLauncherSource.Play();
            tempGren = Instantiate(grenade, shotPoint.position, Quaternion.Euler(orton.transform.rotation.eulerAngles.x * -1, orton.transform.rotation.eulerAngles.y + 180, orton.transform.rotation.eulerAngles.z));// orton.transform.localRotation);
            fireShot = false;
        }

        if(cooldown > 0)
        {
            cooldown--;
        }

        if (nav.pathPending)
        {
            return;
        }

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            if (wayPointIndex == patrolWayPoints.Length - 1)
            {
                explosion.Play();
                explosionSource.Play();
                anim.Play("Armature|Hit", -1, 0f);
                timeOfReset = Time.time;
                midReset = true;
            }
            else
            {
                wayPointIndex++;
            }
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;
        nav.speed = speed;
        nav.Resume();
    }
}
