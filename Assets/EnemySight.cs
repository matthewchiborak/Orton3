using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public bool isForOrton;
    public NewStoredInfoScript storedInfoShawn;
    public NewStoredInfoScriptOrton storedInfoOrton;

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
        //player = StoredInfoScript.persistantInfo.getPlayerGameObject();
        //playerAnim = StoredInfoScript.persistantInfo.getPlayerAnim();
        //nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //personalLastSighting = StoredInfoScript.persistantInfo.resetPosition;
        //previousSighting = StoredInfoScript.persistantInfo.resetPosition;
    }

    void Update()
    {
        if (isForOrton && storedInfoOrton.lastPosition != previousSighting)
        {
            personalLastSighting = storedInfoOrton.lastPosition;
        }
        if (!isForOrton && storedInfoShawn.lastPosition != previousSighting)
        {
            personalLastSighting = storedInfoShawn.lastPosition;
        }

        if(isForOrton)
        {
            previousSighting = storedInfoOrton.lastPosition;
        }
        else
        {
            previousSighting = storedInfoShawn.lastPosition;
        }
        
        if ((isForOrton && storedInfoOrton.currentHealth < 0f) || (!isForOrton && storedInfoShawn.currentHealth < 0f))
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
                    
                    if(isForOrton && hit.collider.gameObject.CompareTag("Player") && !storedInfoOrton.ignorePlayer)
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
                        //storedInfoOrton.lastPosition = hit.collider.gameObject.transform.position;
                        storedInfoOrton.alert(hit.collider.gameObject.transform.position);
                    }
                    else if (!isForOrton && hit.collider.gameObject.CompareTag("Player") && !storedInfoShawn.ignorePlayer)
                    {
                        playerInSight = true;

                        if (!anim.GetBool("PlayerInSight"))
                        {
                            mark.SetActive(true);

                            markTimer = timeForMark;
                            alertSource.Play();
                        }

                        anim.SetBool("PlayerInSight", true);
                        //Vector3 tempVector = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + 10f, hit.collider.gameObject.transform.position.z);
                        //StoredInfoScript.persistantInfo.lastPosition = tempVector;
                        //storedInfoShawn.lastPosition = hit.collider.gameObject.transform.position;
                        storedInfoShawn.alert(hit.collider.gameObject.transform.position);
                    }
                }
            }

            //Hear him
            if(playerAnim.GetBool("IsRunning") && isForOrton && !storedInfoOrton.ignorePlayer)
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
                //storedInfoOrton.lastPosition = player.transform.position;
                storedInfoOrton.alert(player.transform.position);
            }
            else if(playerAnim.GetBool("IsRunning") && !isForOrton && !storedInfoShawn.ignorePlayer)
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
                //storedInfoShawn.lastPosition = player.transform.position;
                storedInfoShawn.alert(player.transform.position);
            }
            else if(!isForOrton && !storedInfoShawn.ignorePlayer)
            {
                if(storedInfoShawn.getFiredShot())
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
                    //storedInfoShawn.lastPosition = player.transform.position;
                    storedInfoShawn.alert(player.transform.position);
                }
            }
        }
    }
}
