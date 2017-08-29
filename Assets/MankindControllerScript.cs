using UnityEngine;
using System.Collections;

public class MankindControllerScript : MonoBehaviour {
    
    private int currentLocation = 0;
    public Transform[] locations;

    public UnityEngine.AI.NavMeshAgent nav;
    public Animator anim;

    private bool winded = false;
    private float windedTimer = 0;
    private float timeForWind = 4f;

    private float invincibility = 0f;

    public GameObject cactus;
    public GameObject pillow;
    public GameObject hgh;

    private float itemTimer = 0f;
    private float timeForItem = 4f;

    public AudioSource scm;
    public AudioSource dropItem;
    public AudioSource panting;

    void GenerateTarget()
    {
        
        int currentMod = currentLocation % 4;
        //int result = Random.Range(0, 4); //4->7
        //int result = 0;

        if (Random.Range(0,2) == 0)
        {
            currentLocation = (currentLocation + Random.Range(4 - currentMod, 8 - currentMod)) % 16;
        }
        else
        {
            currentLocation = (currentLocation - Random.Range(1 + currentMod, 5 + currentMod));
            if(currentLocation < 0)
            {
                currentLocation += 16;
            }
        }
        

        nav.destination = locations[currentLocation].position;
    }

    void GenerateTarget3()
    {
        int result = Random.Range(4, 13);

        currentLocation = (currentLocation + result) % 16;

        print(currentLocation);

        nav.destination = locations[currentLocation].position;
    }

    void GenerateTarget2()
    {
        int result = Random.Range(0, 16);

        while (true)
        {
            if (currentLocation == 0 || currentLocation == 1 || currentLocation == 2 || currentLocation == 3)
            {
                if (result != 0 && result != 1 && result != 2 && result != 3)
                {
                    break;
                }
            }
            else if (currentLocation == 4 || currentLocation == 5 || currentLocation == 6 || currentLocation == 7)
            {
                if (result != 4 && result != 5 && result != 6 && result != 7)
                {
                    break;
                }
            }
            else if (currentLocation == 8 || currentLocation == 9 || currentLocation == 10 || currentLocation == 11)
            {
                if (result != 8 && result != 9 && result != 10 && result != 11)
                {
                    break;
                }
            }
            else if (currentLocation == 12 || currentLocation == 13 || currentLocation == 14 || currentLocation == 15)
            {
                if (result != 12 && result != 13 && result != 14 && result != 15)
                {
                    break;
                }
            }
            else if (currentLocation == 16)
            {
                if(result != 16)
                {
                    break;
                }
            }
        }

        currentLocation = result;

        nav.destination = locations[currentLocation].position;
    }

    // Use this for initialization
    void Start ()
    {
        GenerateTarget();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(nav.remainingDistance == 0 && !winded)
        {
            winded = true;
            anim.Play("Armature|Winded", -1, 0f);
            windedTimer = timeForWind;
            nav.Stop();
            GenerateTarget();
            itemTimer = 0;
            panting.Play();
        }

        if(!winded)
        {
            if(itemTimer > timeForItem)
            {
                itemTimer = 0;
                //Spawn an item
                int rand = Random.Range(0, 11);
                dropItem.Play();

                if (rand == 10)
                {
                    Instantiate(hgh, new Vector3(transform.position.x, transform.position.y + 27.43f, transform.position.z), transform.rotation);
                }
                else if(rand%2 == 0)
                {
                    Instantiate(pillow, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(cactus, transform.position, transform.rotation);
                }
            }
            else
            {
                itemTimer += Time.deltaTime;
            }
        }

        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
        }
        else
        {
            invincibility = 0;
        }

        if (winded)
        {
            if (windedTimer > 0)
            {
                windedTimer -= Time.deltaTime;
            }
            else
            {
                //GenerateTarget();
                windedTimer = 0;
                winded = false;
                anim.Play("Armature|BossRun", -1, 0f);
                nav.Resume();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && invincibility == 0)
        {
            //nav.Stop();
            invincibility = 3f;
            scm.Play();
            anim.Play("Armature|Killed2", -1, 0f);
            StoredInfoScript.persistantInfo.hitBoss(20);
            //GenerateTarget();
            panting.Stop();
            windedTimer = 0;
            winded = false;
            nav.Resume();
        }
    }
}
