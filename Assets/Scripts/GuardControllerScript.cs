using UnityEngine;
using System.Collections;

public class GuardControllerScript : MonoBehaviour {

    public Animator anim;
    public bool dead = false;
    private int deathCount = 240;

    public Transform rkoPoint;
    public BoxCollider solidBox;
    public Renderer guardBody;
    public Renderer gunBody;
    public GameObject ortonRKOguard;

    //RKO
    public bool beingRKOed;
    public AudioSource RKOthump;

    //Movement speeds
    public float patrolSpeed;// = 120f;
    public float chaseSpeed;// = 240f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 15f;
    public Transform[] patrolWayPoints;

    public EnemySight enemySight;
    public UnityEngine.AI.NavMeshAgent nav;
    public Transform player;
    //Reference health through static function
    //Same with last seen position. All global stuff is just in the static thingy

    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

    private float callinWaitTime = 2f;
    private float callinTimer = 0f;

    public bool isForOrton;
    public NewStoredInfoScript storedInfoShawn;
    public NewStoredInfoScriptOrton storedInfoOrton;

    public ShawnMichaelsControl playerScript;
    public OrtonControlScript ortonPlayerScript;
    private float timeForShot = 1.5f;
    private float shotTimer = 0.875f;
    bool midChase = false;

    public AudioSource gunSource;
    public AudioSource electricSource;
    public AudioClip electricClip;
    public AudioClip sockoClip;

    public bool isStunned = false;
    public float stunTime = 3f;
    public float stunTimer = 3f;

    public float sockoDropTimer = 3f;
    public float timeForDrop = 3f;
    private bool alreadyDropped = false;

    public GameObject pills;
    public GameObject meds;
    public GameObject can;
    public AudioSource dropSource;

    public GameObject mine;
    public bool canDropMines;
    public GameObject ammo;
    public bool canDropAmmo;

    public AudioSource whereYouGo;

    private float angleInQuesiton;

    void Awake()
    {
        //player = StoredInfoScript.persistantInfo.getPlayerTransform();
        //playerScript = StoredInfoScript.persistantInfo.getPlayerScript();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        //Despawn the enemy if its dead
        if (dead)
        {
            nav.Stop();
            if (deathCount > 0)
            {
                deathCount--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else //NOT DEAD
        {
            //If being RKOed
            if(beingRKOed)
            {
                stunTimer = 0;
                electricSource.Stop();
                
                solidBox.enabled = false;
                guardBody.enabled = false;
                gunBody.enabled = false;
                ortonRKOguard.SetActive(true);

                if (!ortonPlayerScript.checkIfInAir())
                {
                    //Hit ground, kill guard
                    transform.position = rkoPoint.position;
                    ortonRKOguard.SetActive(false);
                    guardBody.enabled = true;
                    gunBody.enabled = true;
                    solidBox.enabled = true;
                    stunTimer = stunTime;
                    anim.SetBool("Stunned", false);
                    GetComponentInChildren<Animator>().Play("Armature|Killed", -1, 0f);
                    dead = true;
                    RKOthump.Play();
                }
            }

            if(stunTimer != stunTime)
            {
                if(!isStunned)
                {
                    electricSource.Play();
                }

                {
                    isStunned = true;
                    anim.SetBool("Stunned", true);
                   // anim.Play("Armature|Grabbed", -1, 0f);
                }

                if(sockoDropTimer != timeForDrop)
                {
                    sockoDropTimer += Time.deltaTime;

                    if(sockoDropTimer >= timeForDrop)
                    {
                        sockoDropTimer = timeForDrop;

                        if(!alreadyDropped)
                        {
                            alreadyDropped = true;
                            //dropSource.Play();
                            //Instant a drop
                            int randomValue = Random.Range(0, 6);

                            if (randomValue == 0)
                            {
                                dropSource.Play();
                                Instantiate(pills, new Vector3((float)(transform.position.x), (float)(transform.position.y + 30f), (float)(transform.position.z)), transform.rotation);
                            }
                            else if (randomValue == 1)
                            {
                                dropSource.Play();
                                Instantiate(meds, new Vector3((float)(transform.position.x), (float)(transform.position.y + 30f), (float)(transform.position.z)), transform.rotation);
                            }
                            else if (randomValue == 2)
                            {
                                dropSource.Play();
                                Instantiate(can, new Vector3((float)(transform.position.x), (float)(transform.position.y + 30f), (float)(transform.position.z)), transform.rotation);
                            }
                            else if (randomValue == 3 && canDropMines)
                            {
                                dropSource.Play();
                                Instantiate(mine, new Vector3((float)(transform.position.x), (float)(transform.position.y + 5f), (float)(transform.position.z)), transform.rotation);
                            }
                            else if (randomValue == 4 && canDropAmmo)
                            {
                                dropSource.Play();
                                Instantiate(ammo, new Vector3((float)(transform.position.x), (float)(transform.position.y + 5f), (float)(transform.position.z)), transform.rotation);
                            }
                        }
                    }
                }
                

                nav.Stop();
                //isStunned = true;
                //anim.SetBool("Stunned", true);
                stunTimer += Time.deltaTime;

                if(stunTimer > stunTime)
                {
                    stunTimer = stunTime;
                    enemySight.triggerAlert();
                }
            }
            else if(enemySight.playerInSight && isForOrton && storedInfoOrton.currentHealth > 0)
            {
                isStunned = false;
                anim.SetBool("Stunned", false);
                Shooting();
            }
            else if (enemySight.playerInSight && !isForOrton && storedInfoShawn.currentHealth > 0)
            {
                isStunned = false;
                anim.SetBool("Stunned", false);
                Shooting();
            }
            else if(isForOrton && enemySight.personalLastSighting != storedInfoOrton.resetPosition && storedInfoOrton.currentHealth > 0)
            {
                isStunned = false;
                anim.SetBool("Stunned", false);
                Chasing();
            }
            else if (!isForOrton && enemySight.personalLastSighting != storedInfoShawn.resetPosition && storedInfoShawn.currentHealth > 0)
            {
                isStunned = false;
                anim.SetBool("Stunned", false);
                Chasing();
            }
            else
            {
                isStunned = false;
                anim.SetBool("Stunned", false);
                Patrolling();
            }
        }
    }

    public void TriggerStun()
    {
        electricSource.clip = electricClip;
        stunTimer = 0;
    }

    public void TriggerSocko()
    {
        electricSource.clip = sockoClip;
        sockoDropTimer = 0f;
        stunTimer = 0;
    }

    void Shooting()
    {
        nav.Stop();

        if (callinTimer > 0f && !midChase)
        {
            nav.Stop();
            callinTimer -= Time.deltaTime;
            return;
        }

        midChase = true;

        //Do shooting. Maybe should do in separate script?
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Shooting", true);

        Vector3 _direction = (enemySight.personalLastSighting - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5);

        if (shotTimer > timeForShot)
        {
            //Rotate to point at shawn

            gunSource.Play();
            if (playerScript != null)
            {
                playerScript.hitByBullet();
            }
            else if(ortonPlayerScript != null)
            {
                ortonPlayerScript.hitByBullet();
            }
            
            //anim.Play("Armature|Shoot", -1, 0f);
            shotTimer = shotTimer - timeForShot;
        }
        else
        {
            shotTimer += Time.deltaTime;
        }
    }

    void Chasing()
    {
        shotTimer = 0.875f;

        if (callinTimer > 0f && !midChase)
        {
            nav.Stop();
            callinTimer -= Time.deltaTime;
            return;
        }

        midChase = true;

        anim.SetBool("Shooting", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Running", true);

        nav.Resume();
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        

        if(sightingDeltaPos.sqrMagnitude > 4f)
        {
            nav.destination = enemySight.personalLastSighting;
        }

        nav.speed = chaseSpeed;

        //Check if alert cancelled
        if (isForOrton)
        {
            if(storedInfoOrton.checkIfAlertCancelled())
            {
                enemySight.personalLastSighting = storedInfoOrton.resetPosition;
            }
        }
        else
        {
            if (storedInfoShawn.checkIfAlertCancelled())
            {
                enemySight.personalLastSighting = storedInfoShawn.resetPosition;
            }
        }

        //Reached last seen position
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            anim.SetBool("PlayerInSight", false);
            anim.SetBool("Running", false);
            chaseTimer += Time.deltaTime;

            if(chaseTimer > chaseWaitTime)
            {
                //if(isForOrton)
                //{
                //    storedInfoOrton.lastPosition = storedInfoOrton.resetPosition;
                //    enemySight.personalLastSighting = storedInfoOrton.resetPosition;
                //}
                //else
                //{
                //    storedInfoShawn.lastPosition = storedInfoShawn.resetPosition;
                //    enemySight.personalLastSighting = storedInfoShawn.resetPosition;
                //}
                
                chaseTimer = 0f;
                //Play where'd you go
                //whereYouGo.Play();
            }
        }
        else
        {
            chaseTimer = 0f;
        }
    }

    void Patrolling()
    {
        shotTimer = 0.875f;
        midChase = false;

        callinTimer = callinWaitTime;

        anim.SetBool("Shooting", false);
        anim.SetBool("Running", false);
        anim.SetBool("Walking", true);

        nav.speed = patrolSpeed;
        nav.Resume();

        if ((isForOrton && nav.destination == storedInfoOrton.resetPosition) || (!isForOrton && nav.destination == storedInfoShawn.resetPosition) || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            anim.SetBool("Walking", false);

            if (patrolTimer >= patrolWaitTime)
            {
                if(wayPointIndex == patrolWayPoints.Length-1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }

                patrolTimer = 0f;
            }
        }
        else
        {
            patrolTimer = 0f;
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
    
}
