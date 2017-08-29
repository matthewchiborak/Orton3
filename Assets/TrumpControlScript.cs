using UnityEngine;
using System.Collections;

public class TrumpControlScript : MonoBehaviour {

    public bool phase1 = true;
    private bool attacking = false;
    private float invincibility = 0;
    public Animator anim;
    public AudioSource trumpHit;

    public Transform[] topRopePositions;

    private float attackTimer = 0f;
    private float timeForAttack = 2f;

    private int attackType;
    private float midAttackTimer;
    private float timeForCharge = 0.75f;
    private float timeForElbow = 1f; //For each section
    private int dropPhase = 0; //0 1 or 2

    private float chargeOverShoot = 30f;
    private float chargeOverShootWalk = 15f;

    private float elbowOverShoot = 30f;
    private float elbowOverWalk = 15f;

    private Vector3 attackLocation;
    private Vector3 startLocation;

    public GameObject attackBox;
    public GameObject elbowBox;

    public ParticleSystem fireball;
    public ParticleSystem groundPound;

    public AudioSource fired;
    public AudioSource taunt;
    public AudioSource groundHit;
    public AudioSource onFire;
    public AudioSource jump;
    public bool nextJump = false;

    // Use this for initialization
    void Start ()
    {
        attackBox.SetActive(false);
        elbowBox.SetActive(false);

    }

    public void hitPlayerEffects()
    {
        if(attackType != 3)
        {
            fired.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
        }
        else
        {
            invincibility = 0;
        }

        if(phase1)
        { 

            if (attackTimer > timeForAttack && !attacking)
            {
                attackTimer = 0;
                if (!nextJump)
                {
                    attackType = Random.Range(0, 4);
                }
                else
                {
                    nextJump = false;
                    attackType = 3;
                }
                //attackType = 0;
                attacking = true;

                if (attackType != 3) //NOTE EFFECTS THE BELOW
                {
                    midAttackTimer = 0;
                    startLocation = transform.position;
                    attackLocation = StoredInfoScript.persistantInfo.getPlayerTransform().position;

                    if (Random.Range(0, 2) == 0)
                        if (StoredInfoScript.persistantInfo.getPlayerAnim().GetBool("IsRunning"))
                        {
                            attackLocation = new Vector3(attackLocation.x + StoredInfoScript.persistantInfo.getPlayerTransform().forward.x * chargeOverShoot, attackLocation.y, attackLocation.z + StoredInfoScript.persistantInfo.getPlayerTransform().forward.z * chargeOverShoot);
                        }
                        else if (StoredInfoScript.persistantInfo.getPlayerAnim().GetBool("IsWalking"))
                        {
                            attackLocation = new Vector3(attackLocation.x + StoredInfoScript.persistantInfo.getPlayerTransform().forward.x * chargeOverShootWalk, attackLocation.y, attackLocation.z + StoredInfoScript.persistantInfo.getPlayerTransform().forward.z * chargeOverShootWalk);
                        }

                    transform.rotation = Quaternion.LookRotation(-1 * transform.position + StoredInfoScript.persistantInfo.getPlayerTransform().position, new Vector3(0, 1, 0));
                    //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                    anim.Play("Armature|Charge", -1, 0f);
                    fireball.Play();
                    attackBox.SetActive(true);
                    onFire.Play();
                }
                else
                {
                    midAttackTimer = 0;
                    dropPhase = 0;
                    startLocation = transform.position;
                    attackLocation = topRopePositions[Random.Range(0, 4)].position;
                    transform.rotation = Quaternion.LookRotation(-1 * transform.position + attackLocation, new Vector3(0, 1, 0));
                    //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                    jump.Play();
                    anim.Play("Armature|Toprope", -1, 0f);
                }
            }
            else if (!attacking)
            {
                attackTimer += Time.deltaTime;
            }

            if (attacking)
            {
                if (attackType != 3) //NOTE EFFECTS THE ABOVE
                {
                    //Charge
                    if (midAttackTimer < timeForCharge)
                    {
                        midAttackTimer += Time.deltaTime;
                        //Lerp to the players location
                        transform.position = Vector3.Lerp(startLocation, attackLocation, midAttackTimer / timeForCharge);
                    }
                    else
                    {
                        anim.Play("Armature|Idle", -1, 0f);
                        attacking = false;
                        fireball.Stop();
                        attackBox.SetActive(false);
                    }
                }
                else
                {
                    //ElbowDrop
                    //Charge
                    if (midAttackTimer < timeForElbow)
                    {
                        midAttackTimer += Time.deltaTime;
                        //Lerp to the players location
                        transform.position = Vector3.Slerp(startLocation, attackLocation, midAttackTimer / timeForElbow);
                    }
                    else
                    {
                        //anim.Play("Armature|Idle", -1, 0f);
                        //attacking = false;
                        if (dropPhase == 0)
                        {
                            dropPhase++;
                            midAttackTimer = 0;
                            startLocation = attackLocation;
                            transform.rotation = Quaternion.LookRotation(-1 * transform.position + StoredInfoScript.persistantInfo.getPlayerTransform().position, new Vector3(0, 1, 0));
                            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                            taunt.Play();
                        }
                        else if (dropPhase == 1)
                        {
                            dropPhase++;
                            midAttackTimer = 0;
                            attackLocation = StoredInfoScript.persistantInfo.getPlayerTransform().position;
                            jump.Play();

                            if (Random.Range(0, 2) == 0)
                                if (StoredInfoScript.persistantInfo.getPlayerAnim().GetBool("IsRunning"))
                                {
                                    attackLocation = new Vector3(attackLocation.x + StoredInfoScript.persistantInfo.getPlayerTransform().forward.x * elbowOverShoot, attackLocation.y, attackLocation.z + StoredInfoScript.persistantInfo.getPlayerTransform().forward.z * elbowOverShoot);
                                }
                                else if (StoredInfoScript.persistantInfo.getPlayerAnim().GetBool("IsWalking"))
                                {
                                    attackLocation = new Vector3(attackLocation.x + StoredInfoScript.persistantInfo.getPlayerTransform().forward.x * elbowOverWalk, attackLocation.y, attackLocation.z + StoredInfoScript.persistantInfo.getPlayerTransform().forward.z * elbowOverWalk);
                                }

                            transform.rotation = Quaternion.LookRotation(-1 * transform.position + StoredInfoScript.persistantInfo.getPlayerTransform().position, new Vector3(0, 1, 0));
                            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                            elbowBox.SetActive(true);
                        }
                        else if (dropPhase == 2)
                        {
                            dropPhase = 0;
                            midAttackTimer = 0;
                            attacking = false;
                            elbowBox.SetActive(false);
                            //anim.Play("Armature|Idle", -1, 0f);
                            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                            groundPound.Play();
                            groundHit.Play();
                        }
                    }
                }
            }
        }
        else //Phase 2
        {
            //print("Phase2");
        }
    }

    public void Stomp()
    {
        anim.Play("Armature|StompTrump", -1, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !attacking && invincibility == 0)
        {
            //nav.Stop();
            invincibility = 3f;
            attackTimer = 0;
            
            anim.Play("Armature|Killed2", -1, 0f);
            StoredInfoScript.persistantInfo.hitBoss(8); //Was 8 before

            if(StoredInfoScript.persistantInfo.getBossPercentage() > 0.1)
            {
                trumpHit.Play();
            }
            //GenerateTarget();
            //panting.Stop();
            //nav.Resume();
        }
    }
}
