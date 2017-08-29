using UnityEngine;
using System.Collections;

public class TrumpFightControllScript : MonoBehaviour {

    public GameObject trumpObject;
    private bool fightOver = false;
    private bool phaseChange = false;
    public GameObject tower;

    public Transform startLocation;
    public Transform endLocation;
    private float towerFalltimer = 0;
    private float timeForFall = 7.5f; //Was 10 og

    private Vector3 playerStartPosition;
    public Transform playerEndPosition;
    public Transform playerEndPositionAir;

    //IMPORTNAT ONES
    public Transform phase2Point;
    public Transform seatPoint;
    public Transform seatPoint2;
    public Vector3 trumpStartLocation;
    private Vector3 trumpStartLocation2;
    private Vector3 startScale;
    private Vector3 endScale;

    public BoxCollider trumpHitBox;
    private bool stomp = false;

    public Transform[] spawnPoints;
    public GameObject trumpStomp;
    private float stompTimer = 0.5f;
    private float timeForStomp = 1f;

    public AudioSource bestCanDo;
    public AudioSource metalCrash;
    public AudioSource bigStomp;
    public ParticleSystem impact;
    public ParticleSystem explosion;
    public AudioSource explosionSound;
    private bool best = false;

    // Use this for initialization
    void Start ()
    {
        trumpObject.SetActive(true);
        StoredInfoScript.persistantInfo.playFinalBossMusic();
        StoredInfoScript.persistantInfo.StartBoss("TRUMP");
        StoredInfoScript.persistantInfo.getPlayerScript().SwitchToCyborg();
        startScale = new Vector3(1, 1, 1);
        endScale = new Vector3(10,10,10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!StoredInfoScript.persistantInfo.death)
        StoredInfoScript.persistantInfo.resetMusic();

        if (StoredInfoScript.persistantInfo.currentTrack != 2 && !fightOver && !StoredInfoScript.persistantInfo.death)
        {
            StoredInfoScript.persistantInfo.playFinalBossMusic();
        }

        if (StoredInfoScript.persistantInfo.getBossPercentage() <= 0.1 && !fightOver && !phaseChange)
        {
            phaseChange = true;
            trumpObject.GetComponent<TrumpControlScript>().phase1 = false;
            //trumpObject.transform.position = phase2Point.position;
            trumpStartLocation = trumpObject.transform.position;
            //trumpObject.transform.rotation = Quaternion.Euler(60, 0, 0);
            


            playerStartPosition = StoredInfoScript.persistantInfo.getPlayerTransform().position;
            //bestCanDo.Play();
            explosion.Play();
            explosionSound.Play();
            //seatPoint.position = StoredInfoScript.persistantInfo.getPlayerTransform().position;
            //Lerp the tower town
        }

        if(phaseChange && towerFalltimer <= timeForFall)
        {
            tower.transform.position = Vector3.Lerp(startLocation.position, endLocation.position, towerFalltimer / timeForFall);
            tower.transform.rotation = Quaternion.Lerp(startLocation.rotation, endLocation.rotation, towerFalltimer / timeForFall);

            trumpObject.transform.Rotate(0, 400, 0);

            //if (towerFalltimer/timeForFall < 0.5)
            //{
            //    StoredInfoScript.persistantInfo.getPlayerGameObject().transform.position = Vector3.Lerp(playerStartPosition, playerEndPositionAir.position, towerFalltimer*2 / timeForFall);
            //}
            //else
            //{
            if (towerFalltimer / timeForFall > 0.5) //Flyin to the location
            {
                if (!best)
                {
                    best = true;
                    bestCanDo.Play();
                }
                trumpObject.transform.position = Vector3.Lerp(seatPoint2.position, phase2Point.position, ((towerFalltimer) / (timeForFall / 2)) - 1);
            }
            else
            {
                //Stay on tower for first half
                trumpObject.transform.position = Vector3.Lerp(trumpObject.transform.position, seatPoint2.position, towerFalltimer / (timeForFall));
            }

            trumpObject.transform.localScale = Vector3.Lerp(startScale, endScale, towerFalltimer / (timeForFall));
            StoredInfoScript.persistantInfo.getPlayerGameObject().transform.position = Vector3.Lerp(StoredInfoScript.persistantInfo.getPlayerTransform().position, seatPoint.position, towerFalltimer/(timeForFall));
            //}
           // StoredInfoScript.persistantInfo.getPlayerGameObject().transform.position = seatPoint.position;

            towerFalltimer += Time.deltaTime;
        }

        if(!stomp && towerFalltimer >= timeForFall)
        {
            trumpObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            trumpObject.GetComponent<TrumpControlScript>().Stomp();
            stomp = true;
            trumpHitBox.size = new Vector3(10.06f, 27.52f, 8.7f);
            metalCrash.Play();
            impact.Play();
            StoredInfoScript.persistantInfo.getPlayerScript().KnockDown();
        }

        if(stomp && !fightOver)
        {
            if(stompTimer < timeForStomp)
            {
                stompTimer += Time.deltaTime;
            }
            else
            {
                stompTimer = 0;

                int placeNotToSpawn = Random.Range(0, 4);

                for(int i = 0; i < 4; i++)
                {
                    if(i != placeNotToSpawn)
                    {
                        bigStomp.Play();
                        Instantiate(trumpStomp, spawnPoints[i].position, Quaternion.identity);
                    }
                }
            }
        }

        if (StoredInfoScript.persistantInfo.getBossPercentage() == 0 && !fightOver)
        {
            fightOver = true;
            trumpObject.SetActive(false);
            StoredInfoScript.persistantInfo.EndBoss();
        }
    }
}
