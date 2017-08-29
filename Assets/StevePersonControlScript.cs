using UnityEngine;
using System.Collections;

public class StevePersonControlScript : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent nav;
    public Transform doorPoint;
    public Animator anim;

    private float invincibility = 0f;
    private AudioSource scm;

    // Use this for initialization
    void Start ()
    {
        //nav.destination = doorPoint.position;
        scm = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        nav.destination = doorPoint.position;

        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
        }
        else
        {
            invincibility = 0;
        }
    }

    void Awake()
    {
        //nav.destination = doorPoint.position;
        //nav.Resume();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && invincibility == 0)
        {
            //nav.Stop();
            invincibility = 3f;
            scm.Play();
            anim.Play("Armature|Killed", -1, 0f);
            StoredInfoScript.persistantInfo.hitBoss(20);
        }
    }
}
