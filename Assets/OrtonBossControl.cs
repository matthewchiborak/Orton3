using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public enum OrtonBehaviour
{
    Staying,
    Teleporting,
    Vanished,
    Attacking,
    Hurt,
    Dead
}

public class OrtonBossControl : MonoBehaviour {

    public NewStoredInfoScript shawnStored;

    private OrtonBehaviour currentMode;

    public GameObject smokePuff;

    public Image healthBar;
    public AudioSource hurtSound;
    public AudioSource laughSound;

    public Transform lookatPoint;

    public Animator anim;

    public float maxHealth;
    private float currentHealth;

    public Transform[] positions;
    private int currentPosition;
    private int potentialNextPos;

    private int numOfMoves;
    public int minMoves;
    public int maxMoves;

    public float durationBeforeTele;
    private float timeOfLastTele;
    public float durationofRemainAtPoint;
    public float durationOfHurt;

    public GameObject cannonDropper;
    public GameObject mineDropper;

    public float distanceAwayFromPlayerBeforeTele;
    public float percentageChangeOfAttacking;

    public Transform vanishPoint;
    public float durationBeforeAttack;
    private float timeVanishOccured;
    public float durationOfAttack;
    private float timeAttackStarted;
    public float distanceBehindPlayerToAttack;
    public float heightinAbovePlayerToAttack;
    public float heightinAbovePlayerToAttackCrouch;
    public float forceAppliedOnRKO;

    public BoxCollider RKObox;

    public Animator shawnsAnim;

    private Vector2 tempPos;
    private float randAngle;

    public GameObject loadTriggerAfterDefeat;

    private float timeDied;
    public float durationBeforeBlast;
    public float durationBeforeLoad;
    private bool blastTriggered;
    public GameObject blast;
    private bool laughStarted;

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
        currentPosition = 0;
        currentMode = OrtonBehaviour.Staying;
        numOfMoves = UnityEngine.Random.Range(minMoves, maxMoves + 1);
        timeOfLastTele = Time.time;
        anim.Play("Armature|Idle", -1, 0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentMode)
        {
            case OrtonBehaviour.Staying:
                staying();
                break;
            case OrtonBehaviour.Teleporting:
                teleporting();
                break;
            case OrtonBehaviour.Vanished:
                vanished();
                break;
            case OrtonBehaviour.Attacking:
                attacking();
                break;
            case OrtonBehaviour.Hurt:
                hurt();
                break;
            case OrtonBehaviour.Dead:
                dead();
                break;
        }
	}

    private void staying()
    {
        if(currentHealth <= 10)
        {
            numOfMoves = UnityEngine.Random.Range(minMoves, maxMoves + 1);
            currentMode = OrtonBehaviour.Teleporting;
        }

        if((Time.time - timeOfLastTele) > (durationofRemainAtPoint))
        {
            anim.Play("Armature|Idle", -1, 0f);
            numOfMoves = UnityEngine.Random.Range(minMoves, maxMoves + 1);
            currentMode = OrtonBehaviour.Teleporting;
        }

        //TeleportawayIf get too close
        if(Vector3.Distance(lookatPoint.position, transform.position) < distanceAwayFromPlayerBeforeTele)
        {
            anim.Play("Armature|Idle", -1, 0f);
            numOfMoves = UnityEngine.Random.Range(minMoves, maxMoves + 1);
            currentMode = OrtonBehaviour.Teleporting;
            timeOfLastTele = Time.time - durationBeforeTele;
        }
    }
    private void teleporting()
    {
        if ((Time.time - timeOfLastTele) > durationBeforeTele)
        {
            do
            {
                potentialNextPos = UnityEngine.Random.Range(0, positions.Length);
            } while (currentPosition == potentialNextPos);

            Instantiate(smokePuff, transform.position, Quaternion.identity);

            currentPosition = potentialNextPos;
            transform.position = positions[currentPosition].position;
            transform.LookAt(lookatPoint);

            Instantiate(smokePuff, transform.position, Quaternion.identity);

            numOfMoves--;
            timeOfLastTele = Time.time;

            if(numOfMoves <= 0)
            {
                if(UnityEngine.Random.Range(0, 100) < percentageChangeOfAttacking)
                {
                    Instantiate(smokePuff, transform.position, Quaternion.identity);
                    transform.position = vanishPoint.position;
                    timeVanishOccured = Time.time;
                    laughSound.Play();
                    randAngle = UnityEngine.Random.Range(0, 360);
                    currentMode = OrtonBehaviour.Vanished;
                }
                else
                {
                    anim.Play("Armature|Idle", -1, 0f);
                    currentMode = OrtonBehaviour.Staying;
                }
            }
        }
    }
    private void vanished()
    {
        if((Time.time - timeVanishOccured) > durationBeforeAttack)
        {
            //transform.position = new Vector3(lookatPoint.position.x + lookatPoint.forward.normalized.x * -1 * distanceBehindPlayerToAttack, lookatPoint.position.y, lookatPoint.position.z + lookatPoint.forward.normalized.z * -1 * distanceBehindPlayerToAttack);
            
            if (shawnsAnim.GetBool("IsRunning"))
            {
                tempPos.x = lookatPoint.transform.position.x + (distanceBehindPlayerToAttack/3) * Mathf.Sin(Mathf.Deg2Rad * randAngle);
                tempPos.y = lookatPoint.transform.position.z + (distanceBehindPlayerToAttack / 3) * Mathf.Cos(Mathf.Deg2Rad * randAngle);
            }
            else
            {
                tempPos.x = lookatPoint.transform.position.x + distanceBehindPlayerToAttack * Mathf.Sin(Mathf.Deg2Rad * randAngle);
                tempPos.y = lookatPoint.transform.position.z + distanceBehindPlayerToAttack * Mathf.Cos(Mathf.Deg2Rad * randAngle);
            }
            
            transform.position = new Vector3(tempPos.x, lookatPoint.position.y, tempPos.y);

            transform.LookAt(lookatPoint);
            anim.Play("Armature|RKOLoop", -1, 0f);

            if(shawnsAnim.GetBool("IsCrouching") || shawnsAnim.GetBool("IsRunning"))
            {
                transform.position = new Vector3(tempPos.x, lookatPoint.position.y + heightinAbovePlayerToAttackCrouch, tempPos.y);
                //transform.position = new Vector3(transform.position.x, lookatPoint.position.y + heightinAbovePlayerToAttackCrouch, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(tempPos.x, lookatPoint.position.y + heightinAbovePlayerToAttack, tempPos.y);
                //transform.position = new Vector3(transform.position.x, lookatPoint.position.y + heightinAbovePlayerToAttack, transform.position.z);
            }

            Instantiate(smokePuff, transform.position, Quaternion.identity);
            RKObox.enabled = true;
            timeAttackStarted = Time.time;

            //Apply the force
            if(shawnsAnim.GetBool("IsRunning"))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.normalized.x * forceAppliedOnRKO * 2, 0, transform.forward.normalized.z * forceAppliedOnRKO * 2), ForceMode.Impulse);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.normalized.x * forceAppliedOnRKO, 0, transform.forward.normalized.z * forceAppliedOnRKO), ForceMode.Impulse);
            }
            
            currentMode = OrtonBehaviour.Attacking;
        }
    }
    private void attacking()
    {
        if((Time.time - timeAttackStarted) > durationOfAttack)
        {
            RKObox.enabled = false;
            anim.Play("Armature|Idle", -1, 0f);
            numOfMoves = UnityEngine.Random.Range(minMoves, maxMoves + 1);
            currentMode = OrtonBehaviour.Teleporting;
            timeOfLastTele = Time.time - durationBeforeTele;
        }
    }
    private void hurt()
    {
        if((Time.time - timeOfLastTele) > durationOfHurt)
        {
            if(currentHealth <= 0)
            {
                Instantiate(smokePuff, transform.position, Quaternion.identity);
                anim.Play("Armature|Crouch", -1, 0f);
                transform.position = new Vector3(lookatPoint.position.x + lookatPoint.forward.normalized.x * distanceBehindPlayerToAttack, lookatPoint.position.y, lookatPoint.position.z + lookatPoint.forward.normalized.z * distanceBehindPlayerToAttack);
                transform.LookAt(lookatPoint);
                Instantiate(smokePuff, transform.position, Quaternion.identity);

                timeDied = Time.time;
                currentMode = OrtonBehaviour.Dead;
            }
            else
            {
                anim.Play("Armature|Idle", -1, 0f);
                numOfMoves = UnityEngine.Random.Range(minMoves, maxMoves + 1);
                currentMode = OrtonBehaviour.Teleporting;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentMode != OrtonBehaviour.Hurt && currentMode != OrtonBehaviour.Dead)
        {
            if (other.CompareTag("PlayerAttack") || other.CompareTag("C4Explosion") || other.CompareTag("Cannonball"))
            {
                hurtSound.Play();
                currentHealth -= 10;
                anim.Play("Armature|Hit", -1, 0f);
                currentMode = OrtonBehaviour.Hurt;
                timeOfLastTele = Time.time;

                durationofRemainAtPoint -= 0.5f;

                if(currentHealth <= 10)
                {
                    cannonDropper.SetActive(false);
                    mineDropper.SetActive(true);
                }

                if(currentHealth < 0)
                {
                    currentHealth = 0;
                }

                //Update health bar when that is set up
                healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
            }
        }

        if (other.CompareTag("Cena4"))
        {
            other.gameObject.GetComponent<MineProximityScript>().setOff();
        }

        if (other.CompareTag("Cannonball"))
        {
            Destroy(other.gameObject);
        }
    }

    private void dead()
    {
        if(!blastTriggered && ((Time.time - timeDied) > durationBeforeBlast))
        {
            anim.Play("Armature|Stomp", -1, 0f);
            blastTriggered = true;
            Instantiate(blast, transform.position, Quaternion.identity);
        }

        if(((Time.time - timeDied) > durationBeforeBlast / 2))
        {
            if(!laughStarted)
            {
                laughStarted = true;
                laughSound.Play();
            }

            anim.SetBool("StandUp", true);
            currentHealth = Mathf.Lerp(0, maxHealth, ((Time.time - timeDied - (durationBeforeBlast / 2)) / (durationBeforeBlast / 2)));
            healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        }

        if ((Time.time - timeDied) > durationBeforeLoad)
        {
            shawnStored.setHealth(100);
            loadTriggerAfterDefeat.SetActive(true);
        }
    }
}
