using UnityEngine;
using System.Collections;

public class GolddustControllerScript : MonoBehaviour {

    private int currentLocation = 0;
    public Transform[] locations;

    public Animator anim;
    public AudioSource scm;
    public AudioSource laugh;
    private float invincibility = 0;
    private bool attacking = false;

    private float startAttackTimer = 2;
    private float timeBeforeAttack = 2f;
    private float moveTimer = 0;
    private float timeBeforeMove = 3f;

    public ParticleSystem teleport;

    private Vector3 rotationSpeed;

    public Transform parentTransform;
    public GameObject GDProjectile;

    private float attackTimer = 0;
    private float attackRate = 0.5f;

	// Use this for initialization
	void Awake ()
    {
        StoredInfoScript.persistantInfo.PlayBossMusic();
        laugh.Play();
        StoredInfoScript.persistantInfo.StartBoss("GOLDUST");
        rotationSpeed = new Vector3(0, 1200, 0);
    }

    // Update is called once per frame
    void Update ()
    {
	    if(invincibility > 0)
        {
            invincibility -= Time.deltaTime;
            if(invincibility < 0)
            {
                invincibility = 0;
            }
        }

        if(moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
            if(moveTimer <= 0)
            {
                moveTimer = 0;
                startAttackTimer = timeBeforeAttack;
                //Move to a random location
                int newLocation = Random.Range(0, 4);
                while(newLocation == currentLocation)
                {
                    newLocation = Random.Range(0, 4);
                }
                teleport.Play();
                parentTransform.position = locations[newLocation].position;
                
                currentLocation = newLocation;
                anim.Play("Armature|Idle", -1, 0f);
                laugh.Play();
            }
        }

        if (startAttackTimer > 0)
        {
            startAttackTimer -= Time.deltaTime;
            if (startAttackTimer <= 0)
            {
                startAttackTimer = 0;
                attacking = true;
                anim.Play("Armature|Attack", -1, 0f);
            }
        }

        if (attacking)
        {
            //Spin and spawn shots
            parentTransform.Rotate(rotationSpeed * Time.deltaTime);

            if(attackTimer < 0)
            {
                Instantiate(GDProjectile, parentTransform.position, parentTransform.rotation);
                attackTimer = attackRate;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && invincibility == 0)
        {
            scm.Play();
            anim.Play("Armature|Killed2", -1, 0f);
            StoredInfoScript.persistantInfo.hitBoss(15);
            invincibility = 4f;
            moveTimer = timeBeforeMove;
            attacking = false;
        }
    }
}
