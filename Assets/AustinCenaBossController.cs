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
    public int currentPhase;
    public AudioSource hurtSource;
    public GameObject smokeCloud;
    public Transform ortonTransform;

    //Fire
    public GameObject austin;
    public Transform firePitTransform;
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

    //Can through wall
    public GameObject beerAustin;
    public NewStoredInfoScriptOrton storedInfo;
    private Vector3 startPointBeer;
    private Vector3 endPointBeer;
    public float speedRunToCan;
    private float durationRunToCan;
    private float timeRunStarted;
    private bool canPlaced;
    private bool reachCan;
    public Animator beerAustinAnim;
    public BeerAustinScript checkIfAustinBeerHit;
    public AudioSource austinBeerFootsteps;

    //Gate
    public GameObject gateAustin;
    public BeerAustinScript checkIfAustinGateHit;
    public GameObject gate;
    public Vector3 closedPosGate;
    public Vector3 openPosGate;
    public float durationOfOpening;
    private float timeStartOpen;
    public Transform cameraTransform;
    private Vector3 tempGatePos;
    private bool playerLookingAway;
    public Animator gateAustinAnim;

    //Torches
    public BeerAustinScript checkIfAustinTorchesHit;
    public Vector3 correctPos;
    public GameObject redTorch;
    public Vector3 redTorchCorrectPos;
    public GameObject greenTorch;
    public Vector3 greenTorchCorrectPos;
    public GameObject blueTorch;
    public Vector3 blueTorchCorrectPos;
    public GameObject purpleTorch;
    public Vector3 purpleTorchCorrectPos;

    //Washing Machine
    public GameObject washingMachine;
    public WashingMachineHitboxScript washingHitboxScript;

    //Stomponface
    public StompOnAustinHitBox austinGlyphic;
    public Transform austinGlyphicTranform;

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
        //currentPhase = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentPhase)
        {
            case 1:
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
            case 0:
                if(!canPlaced && (storedInfo.resetPosition != storedInfo.lastPosition))
                {
                    canPlaced = true;
                    timeRunStarted = Time.time;
                    durationRunToCan = Vector3.Distance(beerAustin.transform.position, storedInfo.lastPosition) / speedRunToCan;
                    startPointBeer = beerAustin.transform.position;
                    endPointBeer = storedInfo.lastPosition;
                    beerAustinAnim.Play("Armature|Run", -1, 0f);
                    beerAustin.transform.LookAt(storedInfo.lastPosition);
                    beerAustin.transform.Rotate(0, 180, 0);
                    austinBeerFootsteps.Play();
                }
                if(canPlaced && !reachCan)
                {
                    storedInfo.cancelAlertWithoutPenalty();

                    //Check if hit
                    if (checkIfAustinBeerHit.wasHit)
                    {
                        hurtSource.Play();
                        damageBoss();
                        Instantiate(smokeCloud, austin.transform.position, Quaternion.identity);
                        currentPhase++;
                        resetPhases();
                        Instantiate(smokeCloud, firePitTransform.position, Quaternion.identity);
                    }

                    beerAustin.transform.position = Vector3.Lerp(startPointBeer, endPointBeer, (Time.time - timeRunStarted) / durationRunToCan);
                    if((Time.time - timeRunStarted) > durationRunToCan)
                    {
                        beerAustinAnim.Play("Armature|Putdown", -1, 0f);
                        reachCan = true;
                        austinBeerFootsteps.Stop();
                    }
                }
                if(reachCan)
                {
                    storedInfo.cancelAlertWithoutPenalty();

                    //Check if hit
                    if (checkIfAustinBeerHit.wasHit)
                    {
                        damageBoss();
                        hurtSource.Play();
                        Instantiate(smokeCloud, beerAustin.transform.position, Quaternion.identity);
                        currentPhase++;
                        resetPhases();
                        Instantiate(smokeCloud, firePitTransform.position, Quaternion.identity);

                    }
                }
                break;
            case 3:
                //Gate
                if(checkIfAustinGateHit.wasHit)
                {
                    gate.SetActive(false);
                    damageBoss();
                    hurtSource.Play();
                    Instantiate(smokeCloud, gateAustin.transform.position, Quaternion.identity);
                    currentPhase++;
                    resetPhases();

                    //Washing Machin eis next
                    Instantiate(smokeCloud, washingMachine.transform.position, Quaternion.identity);
                    return;
                }
                //Look at gate
                if(cameraTransform.rotation.eulerAngles.y > 0 && cameraTransform.rotation.eulerAngles.y < 180)
                {
                    gate.SetActive(true);
                    //if(playerLookingAway)
                    //{
                    //    playerLookingAway = false;
                    //    tempGatePos = gate.transform.position;
                    //    timeStartOpen = Time.time;
                    //}
                    //gate.transform.position = Vector3.Lerp(tempGatePos, closedPosGate, (Time.time - timeStartOpen) / durationOfOpening);
                }
                else
                {
                    //looking away
                    gate.SetActive(false);
                    //if (!playerLookingAway)
                    //{
                    //    playerLookingAway = true;
                    //    tempGatePos = gate.transform.position;
                    //    timeStartOpen = Time.time;
                    //}
                    //gate.transform.position = Vector3.Lerp(tempGatePos, openPosGate, (Time.time - timeStartOpen) / durationOfOpening);
                }
                break;
            case 2:
                //Torches
                if (checkIfAustinTorchesHit.wasHit)
                {
                    damageBoss();
                    hurtSource.Play();
                    currentPhase++;
                    resetPhases();

                    //Next is gate
                    gateAustinAnim.Play("Armature|Idle", -1, 0f);

                    
                }

                redTorch.transform.position = new Vector3(redTorchCorrectPos.x + (correctPos.z - ortonTransform.position.z), redTorchCorrectPos.y, redTorchCorrectPos.z);
                greenTorch.transform.position = new Vector3(greenTorchCorrectPos.x, greenTorchCorrectPos.y, greenTorchCorrectPos.z - 0.5f * (correctPos.x - ortonTransform.position.x));
                blueTorch.transform.position = new Vector3(blueTorchCorrectPos.x - (correctPos.x - ortonTransform.position.x), blueTorchCorrectPos.y, blueTorchCorrectPos.z + (correctPos.z - ortonTransform.position.z));
                purpleTorch.transform.position = new Vector3(purpleTorchCorrectPos.x, purpleTorchCorrectPos.y + (correctPos.z - ortonTransform.position.z), purpleTorchCorrectPos.z);
                break;
            case 4:
                //Check if hit
                if (washingHitboxScript.wasHit)
                {
                    hurtSource.Play();
                    damageBoss();
                    Instantiate(smokeCloud, washingMachine.transform.position, Quaternion.identity);
                    currentPhase++;
                    resetPhases();

                    //Next is stompOnFace
                    Instantiate(smokeCloud, austinGlyphicTranform.position, Quaternion.identity);
                }
                break;
            case 5:
                //Check if hit
                if (austinGlyphic.wasHit)
                {
                    hurtSource.Play();
                    damageBoss();
                    currentPhase++;
                    resetPhases();
                }
                break;
        }

        storedInfo.cancelAlertWithoutPenalty();
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
