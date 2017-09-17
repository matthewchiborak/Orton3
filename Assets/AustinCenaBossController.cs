using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class AustinCenaBossController : MonoBehaviour {

    public Image healthBarFront;
    public float maxHealth;
    private float currentHealth;
    public float damageAmount;

    public GameObject[] phases;
    private int currentPhase;
    public AudioSource hurtSource;
    public GameObject smokeCloud;

    //Fire
    public GameObject austin;
    private bool fireLit;
    public GameObject fireElements;
    private float timeFireElementsTurnedOn;
    public float durationBeforeFall;
    public float durationOfFall;
    public AudioSource coughSource;
    public Vector3 startPosFire;
    public Vector3 endPosFire;
    public AudioSource crashSource;
    private bool hitGroundFire;
    public float durationRemainOfGroundFire;
    public float durationBeforeCough;
    private bool isCoughing;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
        currentPhase = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentPhase)
        {
            case 0:
                //Fire
                if(!fireLit && fireElements.active)
                {
                    fireLit = true;
                    timeFireElementsTurnedOn = Time.time;
                    //coughSource.Play();
                }
                if(!isCoughing && fireLit)
                {
                    if((Time.time - timeFireElementsTurnedOn) > durationBeforeCough)
                    {
                        timeFireElementsTurnedOn = Time.time;
                        coughSource.Play();
                        isCoughing = true;
                    }
                }
                if(isCoughing && !hitGroundFire)
                {
                    if((Time.time - timeFireElementsTurnedOn) > durationBeforeFall)
                    {
                        austin.transform.position = Vector3.Lerp(startPosFire, endPosFire, ((Time.time - timeFireElementsTurnedOn - durationBeforeFall) / durationOfFall));

                        if(((Time.time - timeFireElementsTurnedOn - durationBeforeFall) > durationOfFall))
                        {
                            hitGroundFire = true;
                            damageBoss();
                            crashSource.Play();
                            hurtSource.Play();
                            timeFireElementsTurnedOn = Time.time;
                        }
                    }
                }
                if(hitGroundFire)
                {
                    if ((Time.time - timeFireElementsTurnedOn) > durationRemainOfGroundFire)
                    {
                        Instantiate(smokeCloud, austin.transform.position, Quaternion.identity);
                        currentPhase++;
                        resetPhases();
                    }
                }
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
	}

    public void resetPhases()
    {
        for(int i = 0; i < phases.Length; i++)
        {
            if(i == currentPhase)
            {
                phases[i].SetActive(true);
            }
            else
            {
                phases[i].SetActive(false);
            }
        }
    }

    public void damageBoss()
    {
        currentHealth -= damageAmount;

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthBarFront.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        if(currentHealth <= 0)
        {
            //Boss is dead
        }
    }
}
