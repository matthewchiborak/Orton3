using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class GoldustBossControl : MonoBehaviour {

    public BoxCollider loadSceneBox;

    public float maxHealth;
    private float currentHealth;

    public Image healthBar;

    public GameObject[] faces;
    public int currentWeakness;

    public BoxCollider hitbox;
    public BoxCollider groundHitBox;
    public float damageAmount;

    public Animator[] anim;

    public bool isAttacking;

    public Vector3 startPos;
    public Vector3 endPos;
    public float durationOfDecend;
    public float durationOfWait;
    public float duartionOfDeath;
    private float timeAttackingStarted;
    private bool wasHit;

    public GameObject smokePuff;

    public AudioSource hurtSource;

    public SkinnedMeshRenderer skin;

    private int tempSkinValue;
    
    public float yAngleIncrement;

    public GameObject fireBall;
    public Transform[] spawnPoints;
    public float durationBetweenSpawnBall;
    private float timeOfLastSpawn;
    private int randPoint;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
        timeAttackingStarted = Time.time;
        timeOfLastSpawn = Time.time;
        isAttacking = true;
        anim[0].Play("Armature|Attack", -1, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(wasHit)
        {
            if ((Time.time - timeAttackingStarted) > duartionOfDeath)
            {
                tempSkinValue = UnityEngine.Random.Range(0, 4);
                if (tempSkinValue == currentWeakness)
                {
                    tempSkinValue++;
                    if (tempSkinValue == 4)
                    {
                        tempSkinValue = 0;
                    }
                }
                currentWeakness = tempSkinValue;

                for (int i = 0; i < faces.Length; i++)
                {
                    if (i == currentWeakness)
                    {
                        faces[i].SetActive(true);
                    }
                    else
                    {
                        faces[i].SetActive(false);
                    }
                }
                
                hitbox.enabled = false;
                groundHitBox.enabled = false;
                timeAttackingStarted = Time.time;
                isAttacking = true;
                Instantiate(smokePuff, transform.position, Quaternion.identity);
                anim[currentWeakness].Play("Armature|Attack", -1, 0);
                wasHit = false;
            }

            return;
        }

		if(isAttacking)
        {
            if ((Time.time - timeOfLastSpawn) > durationBetweenSpawnBall * ((currentHealth / (maxHealth + 0.1f))))
            {
                randPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
                Instantiate(fireBall, spawnPoints[randPoint].position, spawnPoints[randPoint].rotation);
                timeOfLastSpawn = Time.time;
            }

            transform.Rotate(0, yAngleIncrement, 0);

            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - timeAttackingStarted) / durationOfDecend);
            if((Time.time - timeAttackingStarted) > durationOfDecend)
            {
                timeAttackingStarted = Time.time;
                isAttacking = false;

                if(currentWeakness == 1)
                {
                    groundHitBox.enabled = true;
                }
                else
                {
                    hitbox.enabled = true;
                }

                anim[currentWeakness].Play("Armature|Idle", -1, 0);
            }
        }
        else
        {
            if ((Time.time - timeAttackingStarted) > durationOfWait)
            {
                hitbox.enabled = false;
                groundHitBox.enabled = false;
                timeAttackingStarted = Time.time;
                isAttacking = true;
                Instantiate(smokePuff, transform.position, Quaternion.identity);
                

                tempSkinValue = UnityEngine.Random.Range(0, 4);
                if(tempSkinValue == currentWeakness)
                {
                    tempSkinValue++;
                    if(tempSkinValue == 4)
                    {
                        tempSkinValue = 0;
                    }
                }
                currentWeakness = tempSkinValue;
                for(int i = 0; i < faces.Length; i++)
                {
                    if(i == currentWeakness)
                    {
                        faces[i].SetActive(true);
                    }
                    else
                    {
                        faces[i].SetActive(false);
                    }
                }

                anim[currentWeakness].Play("Armature|Attack", -1, 0);
            }
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && !wasHit)
        {
            if(currentWeakness == 0)
            {
                damageBoss();
            }
        }
        if (other.CompareTag("StompBolt") && !wasHit)
        {
            if (currentWeakness == 1)
            {
                damageBoss();
            }
        }
        if (other.CompareTag("Socko") && !wasHit)
        {
            if (currentWeakness == 2)
            {
                damageBoss();
            }
        }
        if (other.CompareTag("C4Explosion") && !wasHit)
        {
            if (currentWeakness == 3)
            {
                damageBoss();
            }
        }
        
        if (other.CompareTag("Cena4"))
        {
            
            other.gameObject.GetComponent<MineProximityScript>().setOff();
        }

    }

    void damageBoss()
    {
        currentHealth -= damageAmount;
        anim[currentWeakness].Play("Armature|Killed2", -1, 0);
        wasHit = true;
        hurtSource.Play();

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //Save and load next scene
            loadSceneBox.enabled = true;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        timeAttackingStarted = Time.time;
    }
}
