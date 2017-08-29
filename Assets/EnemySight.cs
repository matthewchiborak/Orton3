using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting; //For hearing

    private UnityEngine.AI.NavMeshAgent nav;
    public SphereCollider col;

    public Animator anim;

    public GameObject player;
    public Animator playerAnim;

    private Vector3 previousSighting;

    public GuardControllerScript myControlScript;

    public AudioSource alertSource;
    public GameObject mark;
    private float timeForMark = 1f;
    private float markTimer = 0f;


    void Awake()
    {
        player = StoredInfoScript.persistantInfo.getPlayerGameObject();
        playerAnim = StoredInfoScript.persistantInfo.getPlayerAnim();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        personalLastSighting = StoredInfoScript.persistantInfo.resetPosition;
        previousSighting = StoredInfoScript.persistantInfo.resetPosition;
    }

    void Update()
    {
        if (StoredInfoScript.persistantInfo.lastPosition != previousSighting)
        {
            personalLastSighting = StoredInfoScript.persistantInfo.lastPosition;
        }

        previousSighting = StoredInfoScript.persistantInfo.lastPosition;

        if (StoredInfoScript.persistantInfo.currentHealth < 0f)
        {
            playerInSight = false;
        }

        //Get rid of the mark if needed
        if (markTimer > 0)
        {
            markTimer -= Time.deltaTime;
        }
        else
        {
            mark.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
        }
    }

    

    void OnTriggerStay(Collider other)
    {
        if (myControlScript.dead || myControlScript.isStunned)
        {
            return;
        }

        if(other.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
            
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + new Vector3(0f,10f,0f), direction.normalized, out hit, 6 * col.radius))
                {
                    if(hit.collider.gameObject.CompareTag("Player") && !StoredInfoScript.persistantInfo.ignorePlayer)
                    {
                        playerInSight = true;

                        if(!anim.GetBool("PlayerInSight"))
                        {
                            mark.SetActive(true);
                           
                            markTimer = timeForMark;
                            alertSource.Play();
                        }

                        anim.SetBool("PlayerInSight", true);
                        //Vector3 tempVector = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + 10f, hit.collider.gameObject.transform.position.z);
                        //StoredInfoScript.persistantInfo.lastPosition = tempVector;
                        StoredInfoScript.persistantInfo.lastPosition = hit.collider.gameObject.transform.position;
                    }
                }
            }

            //Hear him
            if(playerAnim.GetBool("IsRunning") && !StoredInfoScript.persistantInfo.ignorePlayer)
            {
                if (!anim.GetBool("PlayerInSight"))
                {
                    mark.SetActive(true);
                   
                    markTimer = timeForMark;
                    alertSource.Play();
                }

                playerInSight = true;
                anim.SetBool("PlayerInSight", true);
                //alertSource.Play();
                //Vector3 tempVector = new Vector3(player.transform.position.x, player.transform.position.y + 10f, player.transform.position.z);
                //StoredInfoScript.persistantInfo.lastPosition = tempVector;
                StoredInfoScript.persistantInfo.lastPosition = player.transform.position;
            }
        }
    }
}
