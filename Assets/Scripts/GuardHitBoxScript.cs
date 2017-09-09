using UnityEngine;
using System.Collections;

public class GuardHitBoxScript : MonoBehaviour {

    public bool isForOrton;
    public AudioClip sweetChinMusic;
    public AudioClip RKO;

    public AudioClip hitByBall;
    public AudioClip[] hitByLava;

    public AudioSource screamSource;
    public AudioSource altSource;

	// Use this for initialization
	//void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//}

    void OnTriggerEnter(Collider other)
    {
        //print("Test");

        if (other.gameObject.CompareTag("PlayerAttack") && !GetComponentInParent<GuardControllerScript>().beingRKOed && !GetComponentInParent<GuardControllerScript>().dead)
        {
            if (!isForOrton)
            {
                GetComponentInParent<AudioSource>().clip = sweetChinMusic;
                GetComponent<Animator>().Play("Armature|Killed", -1, 0f);
                GetComponentInParent<GuardControllerScript>().anim.SetBool("Stunned", false);
                GetComponentInParent<GuardControllerScript>().dead = true;
            }
            else
            {
                GetComponentInParent<AudioSource>().clip = RKO;
                GetComponentInParent<GuardControllerScript>().stunTimer = 0;
                GetComponentInParent<GuardControllerScript>().beingRKOed = true;
            }
            GetComponentInParent<AudioSource>().Play();
        }

        //Cena4
        if (other.gameObject.CompareTag("C4Explosion"))
        {
            GetComponent<Animator>().Play("Armature|Killed", -1, 0f);
            GetComponentInParent<GuardControllerScript>().anim.SetBool("Stunned", false);
            GetComponentInParent<GuardControllerScript>().dead = true;
        }

        //Cannonball
        if (other.gameObject.CompareTag("Cannonball"))
        {
            GetComponent<Animator>().Play("Armature|Killed", -1, 0f);
            GetComponentInParent<GuardControllerScript>().anim.SetBool("Stunned", false);
            GetComponentInParent<GuardControllerScript>().dead = true;

            GetComponentInParent<AudioSource>().clip = hitByBall;
            GetComponentInParent<AudioSource>().Play();

            Destroy(other.gameObject);
        }

        //Lava
        if (other.gameObject.CompareTag("LavaShot") && !GetComponentInParent<GuardControllerScript>().dead)
        {
            GetComponent<Animator>().Play("Armature|Killed", -1, 0f);
            GetComponentInParent<GuardControllerScript>().anim.SetBool("Stunned", false);
            GetComponentInParent<GuardControllerScript>().dead = true;
            
            screamSource.clip = hitByLava[Random.Range(0, hitByLava.Length)];
            screamSource.Play();

            altSource.clip = sweetChinMusic;
            //GetComponentInParent<AudioSource>().clip = hitByLava[Random.Range(0, hitByLava.Length)];
            altSource.Play();
        }

        if (other.gameObject.CompareTag("Cena4"))
        {
            other.gameObject.GetComponent<MineProximityScript>().setOff();
        }

        if (other.gameObject.CompareTag("Socko"))
        {
            GetComponentInParent<GuardControllerScript>().sockoDropTimer = 0f;
            GetComponentInParent<GuardControllerScript>().TriggerSocko();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Socko"))
            GetComponentInParent<GuardControllerScript>().stunTimer = 0;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Socko"))
        {
            GetComponentInParent<GuardControllerScript>().stunTimer = GetComponentInParent<GuardControllerScript>().stunTime - 0.5f;
        }
    }
}
