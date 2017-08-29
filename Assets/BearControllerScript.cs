using UnityEngine;
using System.Collections;

public class BearControllerScript : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent nav;
    public AudioSource audioSource;
    public AudioSource sweetChinMusic;
    public bool isFighting = false; //Should be false when done testing
    private bool isAttacking = false;
    public Animator anim;

    private float timeForWait = 2f;
    private float waitTimer = 0f;
    public bool dead = false;
    private int deathCount = 240;
    private bool singleAttack = false;
    public bool isDead = false;
    public BearFightController myControllerFight;

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            if(!isDead)
            {
                myControllerFight.upDeathCount();
                isDead = true;
            }

            nav.Stop();
            if (deathCount > 0)
            {
                deathCount--;
            }
            else
            {
               
                
            }
        }

        if (isFighting && !isAttacking && !dead)
        {
            anim.SetBool("IsFighting", true);
            nav.destination = (StoredInfoScript.persistantInfo.getPlayerTransform().position);
        }

        if(isAttacking && !dead)
        {
            waitTimer+= Time.deltaTime;

            if(waitTimer > (0.5f) && !singleAttack)
            {
                // StoredInfoScript.persistantInfo.hitByBullet();
                StoredInfoScript.persistantInfo.getPlayerScript().hitByBullet();
                singleAttack = true;
            }

            if(waitTimer > timeForWait)
            {
                isAttacking = false;
                waitTimer = 0;
                singleAttack = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !dead)
        {
            anim.Play("Armature|BearDead", -1, 0f);
            sweetChinMusic.Play();
            dead = true;
        }

        if (isFighting && !isAttacking && !dead)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isAttacking = true;
                anim.Play("Armature|BearAttack", -1, 0f);
                //StoredInfoScript.persistantInfo.hitBySkeleton();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
