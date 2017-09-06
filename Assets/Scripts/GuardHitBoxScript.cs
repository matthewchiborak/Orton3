using UnityEngine;
using System.Collections;

public class GuardHitBoxScript : MonoBehaviour {

    public bool isForOrton;
    public AudioClip sweetChinMusic;
    public AudioClip RKO; 

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
