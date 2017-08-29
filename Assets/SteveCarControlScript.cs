using UnityEngine;
using System.Collections;

public class SteveCarControlScript : MonoBehaviour {

    private float PI = 3.1415f;
    public GameObject SteveVuln;
    public GameObject ShawnMichaels;
    private float landRadius = 94.31f; //Add this is to postiion?
    public GameObject doorPoint;

    public GameObject SteveInSeat;

    public GameObject steveFly;

    public GameObject carBounds;

    private UnityEngine.AI.NavMeshAgent nav;

    private bool VulnActive = false;
    private float launchTimer = 0f;
    private float timeForLaunch = 0.7f;

    private float rotationTimer = 0f;
    private float timeBeforeCharge = 5f;
    private bool charging = false;
    private Vector3 placeToGo;
    private int lastBoundUsed = -1;

    private float carTimer = 0f;//10f;
    private float timeForCar = 10f;
    public GameObject bossCar;

    public AudioSource carCrashSource;
    public AudioSource burnoutSource;
    public AudioSource playerHitSource;

	// Use this for initialization
	void Start ()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        StoredInfoScript.persistantInfo.StartBoss("STEVE AUSTIN");
        nav.destination = StoredInfoScript.persistantInfo.getPlayerTransform().position;
        StoredInfoScript.persistantInfo.switchTracks();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //SteveVuln.GetComponent<StevePersonControlScript>().doorPoint = 

       // if(SteveVuln.transform.position == doorPoint.transform.gl)
        if(StoredInfoScript.persistantInfo.getBossPercentage() <= 0.0f)
        {
            StoredInfoScript.persistantInfo.EndBoss();
        }

        if(StoredInfoScript.persistantInfo.getBossPercentage() <= 0.2f)
        {
            timeBeforeCharge = 1.25f;
        }
        else if(StoredInfoScript.persistantInfo.getBossPercentage() <= 0.6f)
        {
            timeBeforeCharge = 2.5f;
        }

        if(!VulnActive)
        {
            if(!charging)
            {
                if (rotationTimer < timeBeforeCharge)
                {
                    rotationTimer += Time.deltaTime;
                    //Rotate to look at the player
                    //transform.rotation = Quaternion.Euler(90, SteveCar.transform.rotation.eulerAngles.y, 0); EXAMPLE NOT REAL CODE
                    //transform.rotation = Quaternion.LookRotation(StoredInfoScript.persistantInfo.getPlayerTransform().position);
                   
                    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(StoredInfoScript.persistantInfo.getPlayerTransform().position), Time.deltaTime * 2.0f);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-1 * transform.position + StoredInfoScript.persistantInfo.getPlayerTransform().position, new Vector3(0,1,0)), Time.deltaTime * 2.0f);
                }
                else
                {
                    rotationTimer = 0;
                    charging = true;
                    nav.Resume();

                    placeToGo = StoredInfoScript.persistantInfo.getPlayerTransform().position;
                    //float pxt = (200 - transform.position.x) / (StoredInfoScript.persistantInfo.getPlayerTransform().position.x)

                    //placeToGo = StoredInfoScript.persistantInfo.getPlayerTransform().position;
                    //nav.destination = 
                    burnoutSource.Play();
                    nav.destination = placeToGo;
                }
            }
            else
            {
                //Charge for a limited amount of time? no distnace thats right
                //if (placeToGo == transform.position)
                if(nav.remainingDistance == 0)
                {
                    nav.Stop();
                    charging = false;
                }
            }
        }

        if(carTimer < timeForCar)
        {
            carTimer += Time.deltaTime;
        }
        if(carTimer>timeForCar)
        {
            carTimer = timeForCar;
            if (transform.position.x < 0)
            {
                Instantiate(bossCar, new Vector3((float)(85), (float)(75), (float)(0)), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(bossCar, new Vector3((float)(-85), (float)(75), (float)(0)), Quaternion.Euler(0, 0, 0));
            }
        }

        if (launchTimer > 0)
        {
            launchTimer -= Time.deltaTime;
        }

        if (launchTimer < 0 && !VulnActive)
        {
            launchTimer = 0;

            VulnActive = true;
            SteveVuln.SetActive(true);
            SteveVuln.transform.position = new Vector3((transform.position.x + landRadius * Mathf.Sin(transform.rotation.eulerAngles.y * PI / 180F)), 0, (transform.position.z + landRadius * Mathf.Cos(transform.rotation.eulerAngles.y * PI / 180F)));
            SteveVuln.transform.rotation = transform.rotation;
            
            //SteveVuln.transform.position = new Vector3((transform.position.x + landRadius * Mathf.Sin(transform.rotation.eulerAngles.y * PI / 180F)), 0, (transform.position.z + landRadius * Mathf.Cos(transform.rotation.eulerAngles.y * PI / 180F)));
            //SteveVuln.transform.rotation = transform.rotation;
        }
    }

    public void GotToDoor()
    {
        VulnActive = false;
        SteveVuln.SetActive(false);
        SteveInSeat.SetActive(true);
        nav.Resume();
        carBounds.SetActive(false);
        carTimer = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarBlock"))
        {
            nav.Stop();
            Destroy(other.gameObject);

            carCrashSource.time = 0.7f;
            burnoutSource.Stop();
            carCrashSource.Play();
            carBounds.SetActive(true);
            //Launch Austin. Start at location relative to truck
            SteveInSeat.SetActive(false);
            Instantiate(steveFly, new Vector3((float)(transform.position.x - 6.9f), (float)(transform.position.y + 7.87f), (float)(transform.position.z - 8.49f)), Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0));
            launchTimer = timeForLaunch;
        }

        if(other.gameObject.CompareTag("Player") && !VulnActive)
        {
            if(rotationTimer == 0)
            {
                playerHitSource.Play();
            }
            other.gameObject.GetComponentInParent<ShawnMichaelsControl>().hitByBullet();
            rotationTimer = -0.5f;
        }
    }
}
