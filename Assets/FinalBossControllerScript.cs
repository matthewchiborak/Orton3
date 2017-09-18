using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public enum FinalBossAttack
{
    //Ball,
    //RKO,
    Ball,
    
    
    RKO,
    RKO2,
    Meteor,
    Slash,

    NumOfAttacks,
    Hurt,
    Hesitate,
    Dead,
    Test
}

public class FinalBossControllerScript : MonoBehaviour {

    private int turnsSinceLastBall;
    private FinalBossAttack lastMode;

    public FinalBossAttack currentMode;

    public float maxHealth;
    private float currentHealth;
    public float damageAmount;

    public float durationOfHesitation;
    private float timeHesitationStarted;

    public Transform shawnTransform;
    public Transform flairTransform;
    public RotateAroundPoint flairsRotation;

    public Image healthBar;
    public Animator anim;

    public float durationOfInvinsibility;
    private float timeInvinsibilityStarted;
    public float durationAfterHurt;

    public AudioSource hurtSource;

    public GameObject smokePuff;

    public Transform[] topRopePoints;

    private int randomTopRope;

    //Ball stuff
    public Vector3 startSizeBall;
    public Vector3 endSizeBall;
    public float durationOfCharge;
    private float timeChargeBegan;
    public GameObject ballShot;
    private bool ballLaunched;
    private GameObject ballInstance;
    public Transform ballSpawnLocation;
    private Vector3 ballShotTempDir;
    public HitByReflectedShot flairRefelctScirpt;

    //RKO
    private bool RKOjumped;
    public BoxCollider RKOAttackBox;
    public float durationOfRKO;
    private float timeRKOStarted;
    public float RKOForce;
    public float RKOVertForce;

    //Slash
    //Note to self will also use flair transform
    private bool swordIsOut;
    private bool isSlashing;
    public AudioSource slashSource;
    public float durationOfSheath;
    public float durationOfSlash;
    private float timeSlashBegan;
    public GameObject sword;
    public Vector3 swordStartPoint;
    public Vector3 swordEndPoint;
    public ParticleSystem swordSlashParticles;

    //Meteor
    public float durationOfMeteors;
    private float timeMeteorsBegan;
    public Transform[] meteorSpawnLocations;
    public float durationBetweenMeteors;
    private float timeOfLastMeteorSpawn;
    public GameObject meteorObject;

    public AudioSource flairHitSource;
    public AudioSource orbChargeSource;
    public AudioSource orbLaunchSource;
    public AudioSource orbBounceSource;
    public AudioSource flairWhackSource;

    //death
    public float durationOfSlowMo;
    private float timeOfVictory;
    public Image blackScreen;
    public Text loadingText;
    private Color startColor;
    private Color endColor;

    public AudioSource backgroundMusic;
    public AudioSource ortonJumpSource;

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
        timeInvinsibilityStarted = Time.time - durationOfInvinsibility;
        timeHesitationStarted = Time.time;
        transform.position = topRopePoints[0].position;
        transform.rotation = topRopePoints[0].rotation;

        startColor = new Color(0, 0, 0, 0);
        endColor = new Color(0, 0, 0, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentMode)
        {
            case FinalBossAttack.Hesitate:
                hesitate();
                break;
            case FinalBossAttack.Hurt:
                hurt();
                break;
            case FinalBossAttack.Ball:
                ball();
                break;
            case FinalBossAttack.RKO:
                RKO();
                break;
            case FinalBossAttack.RKO2:
                RKO();
                break;
            case FinalBossAttack.Meteor:
                meteor();
                break;
            case FinalBossAttack.Slash:
                slash();
                break;
            case FinalBossAttack.Dead:
                dead();
                break;
        }
	}

    private void switchModes(FinalBossAttack mode)
    {
        flairsRotation.disabled = false;
        ballLaunched = false;

        RKOAttackBox.enabled = false;
        RKOjumped = false;

        timeSlashBegan = Time.time;
        swordIsOut = false;
        isSlashing = false;

        timeMeteorsBegan = Time.time;
        timeOfLastMeteorSpawn = Time.time;

        if (mode == FinalBossAttack.Dead)
        {
            timeOfVictory = Time.time;
            blackScreen.enabled = true;
        }

        //Special cases for initing
        if (mode == FinalBossAttack.Ball)
        {
            timeChargeBegan = Time.time;
        }
        if (mode == FinalBossAttack.Hesitate)
        {
            anim.Play("Armature|Idle", -1, 0);
            timeHesitationStarted = Time.time;
        }

        currentMode = mode;
    }

    private void hesitate()
    {
        if((Time.time - timeHesitationStarted) > durationOfHesitation)
        {
            if(currentHealth == 10)
            {
                switchModes(FinalBossAttack.RKO);
            }
            else
            {
                if(turnsSinceLastBall >= 2)
                {
                    turnsSinceLastBall = 0;
                    switchModes(FinalBossAttack.Ball);
                    lastMode = FinalBossAttack.Ball;
                }
                else
                {
                    switchModes((FinalBossAttack)UnityEngine.Random.Range(0, (int)FinalBossAttack.NumOfAttacks));

                    while (currentMode == lastMode || (currentMode == FinalBossAttack.RKO && lastMode == FinalBossAttack.RKO2) || (currentMode == FinalBossAttack.RKO2 && lastMode == FinalBossAttack.RKO) || (currentMode == FinalBossAttack.Meteor && (currentHealth > 40)))
                    {
                        switchModes((FinalBossAttack)UnityEngine.Random.Range(0, (int)FinalBossAttack.NumOfAttacks));
                    }
                    
                    lastMode = currentMode;

                    if (currentMode != FinalBossAttack.Ball)
                    {
                        turnsSinceLastBall++;
                    }
                    else
                    {
                        turnsSinceLastBall = 0;
                    }
                }

                
            }
        }
    }

    private void dead()
    {
        Time.timeScale = 0.2f;
        blackScreen.color = Color.Lerp(startColor, endColor, (Time.time - timeOfVictory) / durationOfSlowMo);

        if(((Time.time - timeOfVictory) > durationOfSlowMo))
        {
            Time.timeScale = 1f;
            //blackScreen.enabled = true;
            loadingText.enabled = true;

            SceneManager.LoadScene("Ending", LoadSceneMode.Single);
        }
    }

    private void meteor()
    {
        if ((Time.time - timeOfLastMeteorSpawn) > durationBetweenMeteors)
        {
            timeOfLastMeteorSpawn = Time.time;
            Instantiate(meteorObject, meteorSpawnLocations[UnityEngine.Random.Range(0, meteorSpawnLocations.Length)].position, Quaternion.identity);
        }

        if ((Time.time - timeMeteorsBegan) > durationOfMeteors)
        {
            switchModes(FinalBossAttack.Hesitate);
        }
    }
    private void slash()
    {
        if(!swordIsOut)
        {
            swordIsOut = true;
            sword.SetActive(true);
            slashSource.Play();
            timeSlashBegan = Time.time;
            flairsRotation.disabled = true;
            Instantiate(smokePuff, flairTransform.position, Quaternion.identity);
            flairTransform.position = swordStartPoint;
            flairTransform.LookAt(shawnTransform);
            Instantiate(smokePuff, flairTransform.position, Quaternion.identity);
        }

        if(!isSlashing && (Time.time - timeSlashBegan) > durationOfSheath)
        {
            isSlashing = true;
            swordSlashParticles.Play();
            timeSlashBegan = Time.time;
        }

        if(isSlashing)
        {
            flairTransform.position = Vector3.Lerp(swordStartPoint, swordEndPoint, (Time.time - timeSlashBegan) / durationOfSlash);

            if((Time.time - timeSlashBegan) > durationOfSlash)
            {
                Instantiate(smokePuff, flairTransform.position, Quaternion.identity);
                //flairTransform.Rotate(-90, 0, 0);
                flairsRotation.disabled = false;
                Instantiate(smokePuff, flairTransform.position, Quaternion.identity);
                sword.SetActive(false);
                switchModes(FinalBossAttack.Hesitate);
            }
        }
    }

    private void hurt()
    {
        if((Time.time - timeInvinsibilityStarted) > durationAfterHurt)
        {
            //Pick a random attack and switch to that mode

            Instantiate(smokePuff, transform.position, Quaternion.identity);
            randomTopRope = UnityEngine.Random.Range(0, 4);
            transform.position = topRopePoints[randomTopRope].position;
            transform.rotation = topRopePoints[randomTopRope].rotation;
            Instantiate(smokePuff, transform.position, Quaternion.identity);

            switchModes(FinalBossAttack.Hesitate);
            //switchModes((FinalBossAttack)UnityEngine.Random.Range(0, (int)FinalBossAttack.NumOfAttacks));
        }
    }

    private void RKO()
    {
        if(!RKOjumped)
        {
            timeRKOStarted = Time.time;
            RKOjumped = true;
            RKOAttackBox.enabled = true;
            Vector3 tempPos = transform.position;
            transform.position = new Vector3(transform.position.x, shawnTransform.position.y, transform.position.z);
            transform.LookAt(shawnTransform);
            transform.Rotate(0, 180, 0);
            transform.position = tempPos;

            anim.Play("Armature|RKOLoop", -1, 0);
            ortonJumpSource.Play();

            //Apply force 1000 and 400 is the baseline so 2000? for full ring
            float zFactor = 2 * Vector3.Distance(transform.position, shawnTransform.position) / Vector3.Distance(new Vector3(-211.48f, 0, -87.24f), new Vector3(127.09f, 0, 7.75f));
            //float xFactor = Mathf.Abs(transform.position.x - shawnTransform.position.x) / (221.48f - 87.24f);
            GetComponent<Rigidbody>().AddForce(new Vector3(-1 * zFactor * transform.forward.normalized.x * RKOForce, RKOVertForce, -1 * zFactor * transform.forward.normalized.z * RKOForce), ForceMode.Impulse);

            //GetComponent<Rigidbody>().AddForce(new Vector3(movement.x, 0, movement.z), ForceMode.Impulse);
        }
        
        if((Time.time - timeRKOStarted) > durationOfRKO)
        {

            Instantiate(smokePuff, transform.position, Quaternion.identity);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            anim.Play("Armature|Idle", -1, 0);
            randomTopRope = UnityEngine.Random.Range(0, 4);
            transform.position = topRopePoints[randomTopRope].position;
            transform.rotation = topRopePoints[randomTopRope].rotation;
            Instantiate(smokePuff, transform.position, Quaternion.identity);
            
            switchModes(FinalBossAttack.Hesitate);
        }
    }

    private void ball()
    {
        if(!ballLaunched)
        {
            if(ballInstance == null)
            {
                ballInstance = Instantiate(ballShot, ballSpawnLocation.position, Quaternion.identity);
                ballInstance.GetComponent<BallShotControl>().shawnTransform = shawnTransform;
                ballInstance.GetComponent<BallShotControl>().flairTransform = flairTransform;
                ballInstance.GetComponent<BallShotControl>().orbBounceSource = orbBounceSource;

                orbChargeSource.Play();

                if(currentHealth == 20)
                {
                    flairRefelctScirpt.numberOfReflects = 4;
                }
                //else if (currentHealth / maxHealth <= 0.4)
                //{
                //    flairRefelctScirpt.numberOfReflects = 3;
                //}
                else if (currentHealth == 40 || currentHealth == 30)
                {
                    flairRefelctScirpt.numberOfReflects = 2;
                }
                else if (currentHealth == 50)
                {
                    flairRefelctScirpt.numberOfReflects = 1;
                }
                else
                {
                    flairRefelctScirpt.numberOfReflects = 0;
                }
            }

            ballInstance.transform.position = ballSpawnLocation.position;
            ballInstance.transform.localScale = Vector3.Lerp(startSizeBall, endSizeBall, (Time.time - timeChargeBegan) / durationOfCharge);

            if((Time.time - timeChargeBegan) > durationOfCharge)
            {

                //Determine the direction
                ballShotTempDir = new Vector3(shawnTransform.position.x - ballSpawnLocation.position.x,
                    shawnTransform.position.y + 9f - ballSpawnLocation.position.y,
                    shawnTransform.position.z - ballSpawnLocation.position.z).normalized;

                ballInstance.GetComponent<BallShotControl>().direction = ballShotTempDir;

                ballInstance.GetComponent<BallShotControl>().launch = true;
                ballLaunched = true;
                flairsRotation.disabled = true;

                orbLaunchSource.Play();
            }
        }
        else
        {
            if (ballInstance == null)
            {
                switchModes(FinalBossAttack.Hesitate);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack")&& (Time.time - timeInvinsibilityStarted) > durationOfInvinsibility)
        {
            damageBoss();
        }

        if (other.CompareTag("BallShot") && (Time.time - timeInvinsibilityStarted) > durationOfInvinsibility)
        {
            if(other.gameObject.GetComponent<BallShotControl>().canDamageFlair)
            {
                damageBoss();
                Destroy(other.gameObject);
            }
        }
    }

    public void damageFlair()
    {
        switchModes(FinalBossAttack.Hurt);
        currentHealth -= damageAmount;
        flairHitSource.Play();
        flairWhackSource.Play();
        timeInvinsibilityStarted = Time.time;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //Save and load next scene
            //loadSceneBox.enabled = true;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }

    void damageBoss()
    {
        RKOAttackBox.enabled = false;

        switchModes(FinalBossAttack.Hurt);
        currentHealth -= damageAmount;
        anim.Play("Armature|Hit", -1, 0);
        hurtSource.Play();
        timeInvinsibilityStarted = Time.time;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            backgroundMusic.clip = null;
            switchModes(FinalBossAttack.Dead);
            //Save and load next scene
            //loadSceneBox.enabled = true;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }
}
