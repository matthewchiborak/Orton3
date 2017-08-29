using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FinalCutsceneControl : MonoBehaviour {

    public int currentProgress = 0;

    // Update is called once per frame
    //void Update ()
    //   {
    //    if(currentProgress == )
    //       {

    //       }
    //}
    public Camera mainCam;

    private bool playing = false;

    public AudioSource soundEffect;
    public AudioSource musicTrack;

    public Image cutsceneBackGround;
    public Image portraitRight;
    public Image portraitLeft;
    public Image dialogBox;
    public Image nameLeft;
    public Image nameRight;
    public Text nameLeftText;
    public Text nameRightText;
    public Text dialog;
    public Text dialogRight;

    public Material[] portraits;
    public Material[] backgrounds;

    public TextAsset[] instructions;

    public AudioClip[] music;
    public AudioClip[] soundEffects;

    public Material[] movies;
    public AudioClip[] movieAudio;
    //public MovieTexture[] movies;

    public GameObject[] sceneObjects;
    public ParticleSystem[] particleSystems;

    public Image blackScreen;

    private string instructionsAsString;
    private List<string> eachLine;
    private int currentLine = 0;

    private List<string> parsedLine;

    private bool movieStarted = false;
    //private bool movieFinished = false;
    //private float movieLength = 0;

    private float cutsceneTimer = 0;
    private float timeForTimer = 0;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 startRot;
    private Vector3 endRot;

    private Vector3 startPoint2;
    private Vector3 endPoint2;

    private float startZ;
    private bool altSides = false;
    private bool backmmon = false;

    private bool creditsPlaying = false;
    public AudioSource creditMusic;

    void Start()
    {
        Cursor.visible = false;

        mainCam.transform.position = new Vector3(4605.2f, 25.1209f, -1246.4f);
        mainCam.transform.rotation = Quaternion.Euler(-8.437f, -148.36f, 0);

        sceneObjects[0].transform.position = new Vector3(4499, 0, -1524);
        sceneObjects[0].transform.rotation = Quaternion.Euler(0,0,0);
        sceneObjects[0].transform.localScale = new Vector3(80,80,80);

        sceneObjects[1].transform.position = new Vector3(4500, 0, -1332);
        sceneObjects[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        sceneObjects[1].transform.localScale = new Vector3(8, 8, 8);
    }

    public void CheckCurrentProgresses() //Will be called whenever a cutscene isnt playing
    {
        if(currentProgress == 0)
        {
            PlayCutscene(0); //Opening dialog
        }
        if (currentProgress == 1)
        {
            soundEffect.clip = soundEffects[3];
            soundEffect.Play();
            sceneObjects[0].GetComponent<Animator>().Play("Armature|Toprope", -1, 0f);
            timeForTimer = 2f;
            startPoint = sceneObjects[0].transform.position;
            endPoint = new Vector3(4499, 1000, -1524);
            currentProgress++;
        }
        if (currentProgress == 2)
        {
            sceneObjects[0].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            cutsceneTimer += Time.deltaTime;

            if(cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 3)
        {
            //soundEffect.clip = soundEffects[3];
            //soundEffect.Play();
            //sceneObjects[0].GetComponent<Animator>().Play("Armature|Toprope", -1, 0f);
            timeForTimer = 2f;
            startPoint = mainCam.transform.position;
            startRot = mainCam.transform.rotation.eulerAngles;
            endPoint = new Vector3(4493.85f, 10.4227f, -1346.2f);
            endRot = new Vector3(-2.109f + 360, 50.852f + 360, 0);
            currentProgress++;
        }
        if (currentProgress == 4)
        {
            mainCam.transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            mainCam.transform.rotation = Quaternion.Euler(Vector3.Slerp(startRot, endRot, cutsceneTimer / timeForTimer));
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 5)
        {
            cutsceneTimer = 0;
            PlayCutscene(1);
            //currentProgress++;
        }
        if (currentProgress == 6) //Grow
        {
            mainCam.transform.position = new Vector3(4500, 2.235f, -1410.3f);
            mainCam.transform.rotation = Quaternion.Euler(new Vector3(-25, 0, 0));
            soundEffect.clip = soundEffects[7];
            soundEffect.Play();
            //sceneObjects[0].GetComponent<Animator>().Play("Armature|Toprope", -1, 0f);
            timeForTimer = 2f;
            startPoint = sceneObjects[1].transform.localScale;
            //startRot = mainCam.transform.rotation.eulerAngles;
            endPoint = new Vector3(40f, 40f, 40f);
            //endRot = new Vector3(-12.388f + 360, -380.98f + 720, 0);
            currentProgress++;
        }
        if (currentProgress == 7)
        {
            sceneObjects[1].transform.localScale = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            //mainCam.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, cutsceneTimer / timeForTimer));
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 8) //Shake and smoke
        {
            //mainCam.transform.position = new Vector3(4500, 2.235f, -1410.3f);
            soundEffect.clip = soundEffects[11];
            soundEffect.Play();
            //sceneObjects[0].GetComponent<Animator>().Play("Armature|Toprope", -1, 0f);
            timeForTimer = 3f;
            startZ = mainCam.transform.position.x;

            particleSystems[0].Play();

            //startPoint = sceneObjects[1].transform.localScale;
            //startRot = mainCam.transform.rotation.eulerAngles;
            //endPoint = new Vector3(40f, 40f, 40f);
            //endRot = new Vector3(-12.388f + 360, -380.98f + 720, 0);
            currentProgress++;
        }
        if (currentProgress == 9)
        {
            //if(altSides)
            //{
            //    mainCam.transform.position = new Vector3(startZ + 5f, mainCam.transform.position.y, mainCam.transform.position.z);
            //    altSides = false;
            //}
            //else
            //{
            //    mainCam.transform.position = new Vector3(startZ - 5f, mainCam.transform.position.y, mainCam.transform.position.z);
            //    altSides = true;
            //}
            //mainCam.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, cutsceneTimer / timeForTimer));
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 10) //Launch
        {
            timeForTimer = 5f;
            particleSystems[1].Play();
            //startZ = sceneObjects[1].transform.position.z;
            startPoint = sceneObjects[1].transform.position;
            //startRot = mainCam.transform.rotation.eulerAngles;
            endPoint = new Vector3(4500f, 200f, -1332f);
            //endRot = new Vector3(-12.388f + 360, -380.98f + 720, 0);
            currentProgress++;
        }
        if (currentProgress == 11)
        {
            if (altSides)
            {
                mainCam.transform.position = new Vector3(startZ + 2f, mainCam.transform.position.y, mainCam.transform.position.z);
                altSides = false;
            }
            else
            {
                mainCam.transform.position = new Vector3(startZ - 2f, mainCam.transform.position.y, mainCam.transform.position.z);
                altSides = true;
            }

            sceneObjects[1].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            //mainCam.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, cutsceneTimer / timeForTimer));
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 12)
        {
            timeForTimer = 5f;
            
            currentProgress++;
        }
        if (currentProgress == 13)
        {
            if (altSides)
            {
                mainCam.transform.position = new Vector3(startZ + 2f, mainCam.transform.position.y, mainCam.transform.position.z);
                altSides = false;
            }
            else
            {
                mainCam.transform.position = new Vector3(startZ - 2f, mainCam.transform.position.y, mainCam.transform.position.z);
                altSides = true;
            }
            
            blackScreen.color = new Vector4(0, 0, 0, cutsceneTimer / timeForTimer);
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 14)
        {
            particleSystems[0].Stop();

            soundEffect.clip = soundEffects[8];
            soundEffect.Play();

            mainCam.transform.position = new Vector3(556.677f, 36.5602f, -470.35f);
            mainCam.transform.rotation = Quaternion.Euler(-1.258f, -63.83f, 0);

            blackScreen.enabled = false;

            musicTrack.clip = music[1];
            musicTrack.Play();

            //Move the 2 game objects
            //Trump
            sceneObjects[0].transform.position = new Vector3(-1097, 17, -323);
            sceneObjects[0].transform.rotation = Quaternion.Euler(35, 90, 0);
            sceneObjects[0].transform.localScale = new Vector3(20, 20, 20);
            //sceneObjects[0].GetComponent<Animator>().Play("Armature|Fly", -1, 0f);

            sceneObjects[1].transform.position = new Vector3(-2459, 48.9f, -240);
            sceneObjects[1].transform.rotation = Quaternion.Euler(-75, -90, 0);
            sceneObjects[1].transform.localScale = new Vector3(20, 20, 20);

            startPoint = sceneObjects[0].transform.position; //Tturmp
            startPoint2 = sceneObjects[1].transform.position;
            endPoint = new Vector3(692.5f, 6, -453);
            endPoint2 = new Vector3(554, 35, -453);

            timeForTimer = 5f;
            currentProgress++;
        }
        if (currentProgress == 15)
        {
            sceneObjects[0].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            sceneObjects[1].transform.position = Vector3.Lerp(startPoint2, endPoint2, cutsceneTimer / timeForTimer);
            sceneObjects[0].GetComponent<Animator>().Play("Armature|Toprope", -1, 0.15f);



            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 16)
        {
            mainCam.transform.position = new Vector3(713.367f, 48.9787f, -558.62f);
            mainCam.transform.rotation = Quaternion.Euler(5.902f, 42.779f, 0);

            sceneObjects[0].transform.position = new Vector3(650, 200f, -266.8f);
            sceneObjects[0].transform.rotation = Quaternion.Euler(0, 90, 0);
            sceneObjects[0].transform.localScale = new Vector3(20, 20, 20);
            
            startPoint = sceneObjects[0].transform.position; 
            endPoint = new Vector3(918.1f, 17, -266.8f);
            sceneObjects[0].GetComponent<Animator>().Play("Armature|Idle", -1, 0f);

            timeForTimer = 3;
            currentProgress++;
        }
        if (currentProgress == 17)
        {
            sceneObjects[0].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 18)
        {
            timeForTimer = 1.5f;
            particleSystems[1].Stop();
            soundEffect.clip = soundEffects[6];
            soundEffect.Play();

            currentProgress++;
        }
        if (currentProgress == 19)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 20)
        {
            mainCam.transform.position = new Vector3(556.677f, 36.5602f, -470.35f);
            mainCam.transform.rotation = Quaternion.Euler(-1.258f, -24.94f, 0);

            sceneObjects[1].transform.position = new Vector3(401.5f, 42.8f, -440.4f);
            sceneObjects[1].transform.rotation = Quaternion.Euler(-74.976f, -90, 0);
            sceneObjects[1].transform.localScale = new Vector3(20, 20, 20);

            startPoint = sceneObjects[1].transform.position;
            endPoint = new Vector3(524.8f, 13, -441.5f);
            //sceneObjects[0].GetComponent<Animator>().Play("Armature|Idle", -1, 0f);

            //
            timeForTimer = 1.5f;
            currentProgress++;
        }
        if (currentProgress == 21)
        {
            sceneObjects[1].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 22)
        {
            sceneObjects[1].transform.rotation = Quaternion.Euler(0f, -90, 0);
            
            PlayCutscene(2);
        }
        if (currentProgress == 23)
        {
            mainCam.transform.position = new Vector3(602.247f, 37.5073f, -440.48f);
            mainCam.transform.rotation = Quaternion.Euler(3.167f, -68.774f, 0);

            sceneObjects[2].transform.position = new Vector3(540.7f, -75f, -391.5f);
            sceneObjects[2].transform.rotation = Quaternion.Euler(0, 133.8f, 0);
            sceneObjects[2].transform.localScale = new Vector3(30, 30, 30);

            startPoint = sceneObjects[2].transform.position;
            endPoint = new Vector3(540.7f, 12.1f, -391.5f);
            //sceneObjects[0].GetComponent<Animator>().Play("Armature|Idle", -1, 0f);

            //
            timeForTimer = 1.5f;
            currentProgress++;
        }
        if (currentProgress == 24)
        {
            sceneObjects[2].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 25)
        {
            PlayCutscene(3);
        }
        if (currentProgress == 26)
        {
            mainCam.transform.position = new Vector3(866.1f, 53.8f, -317.74f);
            mainCam.transform.rotation = Quaternion.Euler(5.046f, 46.4f, 0);
            sceneObjects[0].transform.rotation = Quaternion.Euler(0, -90, 0);

            //Play the charging sound effect. Remember to rewarm Matt

            PlayCutscene(4);
        }
        if (currentProgress == 27)
        {
            mainCam.transform.position = new Vector3(647.029f, 9.87029f, -455.54f);
            mainCam.transform.rotation = Quaternion.Euler(0.782f, 46.85f, 0);

            //Start playing the particle effects for firing
            soundEffect.clip = soundEffects[9];
            soundEffect.Play();
            particleSystems[2].Play();
            timeForTimer = 1.5f;
            currentProgress++;
        }
        if (currentProgress == 28)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 29)
        {
            //Stop music
            musicTrack.Stop();
            particleSystems[2].Stop();

            sceneObjects[3].SetActive(true); //Trhusters
            mainCam.transform.position = new Vector3(531.751f, 46.73f, -441.81f);
            mainCam.transform.rotation = Quaternion.Euler(-1.428f, -88.201f, 0);

            timeForTimer = 2f;
            currentProgress++;
        }
        if (currentProgress == 30)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 31)
        {
            //Start music. Yes go to beat
            musicTrack.clip = music[2];
            musicTrack.Play();

            sceneObjects[2].SetActive(false);
            sceneObjects[1].GetComponent<Animator>().Play("Armature|Stomp", -1, 0f);
            //soundEffect.clip = soundEffects[4];
            //soundEffect.Play();

            mainCam.transform.position = new Vector3(549.656f, 15.0607f, -452.49f);
            mainCam.transform.rotation = Quaternion.Euler(-2.62f, -46.552f, 0);

            timeForTimer = 1f;
            currentProgress++;
        }
        if (currentProgress == 32)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 33)
        {
            //Start music. Yes go to beat
            sceneObjects[1].GetComponent<Animator>().Play("Armature|Stomp", -1, 0f);
            //soundEffect.Play();

            timeForTimer = 1f;
            currentProgress++;
        }
        if (currentProgress == 34)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 35)
        {
            //Start music. Yes go to beat
            sceneObjects[1].GetComponent<Animator>().Play("Armature|Stomp", -1, 0f);
           // soundEffect.Play();

            timeForTimer = 1f;
            currentProgress++;
        }
        if (currentProgress == 36)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 37)
        {

            mainCam.transform.position = new Vector3(993f, 344f, -1197f);
            mainCam.transform.rotation = Quaternion.Euler(0f, 180f, 0);

            timeForTimer = 1f;
            currentProgress++;
        }
        if (currentProgress == 38)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 39)
        {
            sceneObjects[1].transform.position = new Vector3(0, 13.0183f, -266.8f);
            sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            mainCam.transform.position = new Vector3(43.0329f, 19.1684f, -396.16f);
            mainCam.transform.rotation = Quaternion.Euler(-6.551f, 0f, 0);
            sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);

            //Play laser long
            //particleSystems[3].GetComponent<Renderer>().sortingLayerName = "Foreground";
            particleSystems[3].Play();
            particleSystems[6].Play();

            timeForTimer = 0.5f;
            currentProgress++;
        }
        if (currentProgress == 40)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 41)
        {
            //Length before 2nd zoom on real face

            //Play deflected lazer
            particleSystems[4].Play();

            timeForTimer = 16f;
            startZ = mainCam.transform.position.y;
            currentProgress++;
        }
        if (currentProgress == 42)
        {
            sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0.5f);
            cutsceneTimer += Time.deltaTime;

            if (altSides)
            {
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, startZ + 2f, mainCam.transform.position.z);
                altSides = false;
            }
            else
            {
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, startZ - 2f, mainCam.transform.position.z);
                altSides = true;
            }

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 43)
        {

            mainCam.transform.position = new Vector3(993f, 344f, -1197f);
            mainCam.transform.rotation = Quaternion.Euler(0f, 180f, 0);

            timeForTimer = 1f;
            currentProgress++;
        }
        if (currentProgress == 44)
        {
            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 45)
        {
            //sceneObjects[1].transform.position = new Vector3(0, 13.0183f, -266.8f);
            //sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            mainCam.transform.position = new Vector3(43.0329f, 19.1684f, -396.16f);
            mainCam.transform.rotation = Quaternion.Euler(-6.551f, 0f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            startPoint = sceneObjects[1].transform.position;
            endPoint = new Vector3(207.32f, 12.1f, -266.8f);

            startPoint2 = sceneObjects[3].transform.position - sceneObjects[1].transform.position;

            //particleSystems[5].Play();

            //timeForTimer = 1f;
            timeForTimer = 1f;
            currentProgress++;
        }
        if (currentProgress == 46)
        {
            //Push back laser

            sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0.5f);
            sceneObjects[1].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            sceneObjects[3].transform.position = sceneObjects[1].transform.position + startPoint2;

            if (altSides)
            {
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, startZ + 2f, mainCam.transform.position.z);
                altSides = false;
            }
            else
            {
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, startZ - 2f, mainCam.transform.position.z);
                altSides = true;
            }

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 47)
        {
            //sceneObjects[1].transform.position = new Vector3(0, 13.0183f, -266.8f);
            //sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            //Zoom in on foot
            particleSystems[3].Stop();
            particleSystems[6].Stop();
            particleSystems[4].Stop();
            particleSystems[5].Stop();
            sceneObjects[8].SetActive(false);
            sceneObjects[9].SetActive(false);
            sceneObjects[10].SetActive(false);

            mainCam.transform.position = new Vector3(243.6f, 43.7238f, -265.59f);
            mainCam.transform.rotation = Quaternion.Euler(8.472f, -87.133f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            startPoint = mainCam.transform.position;
            endPoint = new Vector3(225.282f, 43.7238f, -265.59f);

            timeForTimer = 0.5f;
            currentProgress++;
        }
        if (currentProgress == 48)
        {
            mainCam.transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0.5f);

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 49)
        {
            //Trump zoom
            
            //sceneObjects[1].transform.position = new Vector3(0, 13.0183f, -266.8f);
            //sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            mainCam.transform.position = new Vector3(867.9f, 54.1f, -266.8f);
            mainCam.transform.rotation = Quaternion.Euler(-13.076f, 90f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            startPoint = mainCam.transform.position;
            endPoint = new Vector3(912.37f, 68.1331f, -266.8f);

            timeForTimer = 0.5f;
            currentProgress++;
        }
        if (currentProgress == 50)
        {
            mainCam.transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 51)
        {
            //sceneObjects[1].transform.position = new Vector3(0, 13.0183f, -266.8f);
            //sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            mainCam.transform.position = new Vector3(867.9f, 54.1f, -266.8f);
            //mainCam.transform.rotation = Quaternion.Euler(-2.961f, 52.958f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            startPoint = mainCam.transform.position;
            endPoint = new Vector3(912.37f, 68.1331f, -266.8f);

            timeForTimer = 0.5f;
            currentProgress++;
        }
        if (currentProgress == 52)
        {
            mainCam.transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 53)
        {
            //sceneObjects[1].transform.position = new Vector3(0, 13.0183f, -266.8f);
            //sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            mainCam.transform.position = new Vector3(867.9f, 54.1f, -266.8f);
            //mainCam.transform.rotation = Quaternion.Euler(-2.961f, 52.958f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            startPoint = mainCam.transform.position;
            endPoint = new Vector3(912.37f, 68.1331f, -266.8f);

            timeForTimer = 0.5f;
            currentProgress++;
        }
        if (currentProgress == 54)
        {
            mainCam.transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);

            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 55)
        {
            //Moon crash

            sceneObjects[1].transform.position = new Vector3(890.89f, 31.8f, -266.8f);
            sceneObjects[1].transform.rotation = Quaternion.Euler(0, -90, 0);

            sceneObjects[0].transform.position = new Vector3(917.864f, 17.1605f, -266.8f);
            sceneObjects[0].transform.rotation = Quaternion.Euler(0, -90, 0);

            mainCam.transform.position = new Vector3(871.215f, 206.315f, -1060.9f);
            mainCam.transform.rotation = Quaternion.Euler(10.017f, 29.846f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            startPoint = sceneObjects[0].transform.position;
            endPoint = new Vector3(1804f, 17.1605f, -6.9f);

            sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0.5f);
            soundEffect.clip = soundEffects[5];
            soundEffect.Play();

            timeForTimer = 2.5f;
            currentProgress++;
        }
        if (currentProgress == 56)
        {
            sceneObjects[0].transform.position = Vector3.Lerp(startPoint, endPoint, cutsceneTimer / timeForTimer);
            sceneObjects[0].transform.Rotate(-50, 0, 0);


            cutsceneTimer += Time.deltaTime;

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 57)
        {
            //Delay after moon crash

            particleSystems[7].Play();
            particleSystems[8].Play();
            particleSystems[9].Play();

            soundEffect.clip = soundEffects[4];
            soundEffect.Play();

            timeForTimer = 3f;
            currentProgress++;
        }
        if (currentProgress == 58)
        {
            cutsceneTimer += Time.deltaTime;

            if(cutsceneTimer > 1 && !backmmon)
            {
                particleSystems[10].Play();
                soundEffect.Play();
                backmmon = true;
            }

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 59)
        {
            //Wide shot and fade to black
            //Disable not needed objects
            sceneObjects[0].SetActive(false);
            sceneObjects[1].SetActive(false);
            sceneObjects[2].SetActive(false);
            sceneObjects[4].SetActive(false);
            sceneObjects[5].SetActive(false);
            sceneObjects[6].SetActive(false);
            sceneObjects[7].SetActive(true);

            blackScreen.color = new Vector4(0, 0, 0, 0);
            blackScreen.enabled = true;
            

            mainCam.transform.position = new Vector3(-1352.0f, -30.256f, -834.36f);
            mainCam.transform.rotation = Quaternion.Euler(-0.577f, 59.9f, 0);
            //sceneObjects[1].GetComponent<Animator>().Play("Armature|SweetChinMusic", -1, 0f);
            
            timeForTimer = 10f;
            currentProgress++;
        }
        if (currentProgress == 60)
        {
            cutsceneTimer += Time.deltaTime;
            blackScreen.color = new Vector4(0, 0, 0, cutsceneTimer / timeForTimer);
            musicTrack.volume = 1 - (cutsceneTimer / timeForTimer);

            if (cutsceneTimer > timeForTimer)
            {
                cutsceneTimer = 0;
                currentProgress++;
            }
        }
        if (currentProgress == 61)
        {
            musicTrack.Stop();
            musicTrack.volume = 1;
            blackScreen.enabled = false;
            PlayCutscene(5);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Create a way for the player of quitting the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playing)
        {
            //if (movieLength > 0)
            //{
            //    //Movie is playing
            //    movieLength -= Time.deltaTime;
            //    return;
            //}
            //else if (movieStarted)
            //{
            //    movieLength = 0;
            //    movieFinished = true;
            //    movieStarted = false;
            //}

            //Pressing enter should advance to the next instruction and put it to the screen
            //if (Input.GetKeyDown(KeyCode.Return) || currentLine == -1 || movieFinished)
            if (Input.GetKeyDown(KeyCode.Return) || currentLine == -1)
            {
                //movieFinished = false;
                currentLine++;
                soundEffect.Stop();
                creditMusic.volume = 1f;

                if (movieStarted)
                {
                    ((MovieTexture)cutsceneBackGround.material.mainTexture).Stop();
                    movieStarted = false;
                }

                if (currentLine >= eachLine.Count)
                {
                    //End the cutscene
                    playing = false;
                    //musicTrack.Stop();
                    cutsceneBackGround.enabled = false;
                    portraitRight.enabled = false;
                    portraitLeft.enabled = false;
                    dialogBox.enabled = false;
                    nameLeft.enabled = false;
                    nameRight.enabled = false;
                    nameLeftText.enabled = false;
                    nameRightText.enabled = false;
                    dialog.enabled = false;
                    dialogRight.enabled = false;
                    Time.timeScale = 1.0f;
                    currentProgress++;

                    return;
                }

                parsedLine = new List<string>();
                parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));

                if (int.Parse(parsedLine[0]) > 10000 && int.Parse(parsedLine[1]) > 10000 && int.Parse(parsedLine[2]) > 10000)
                {
                    //Load Opening Scene
                    SceneManager.LoadScene("outsideDay", LoadSceneMode.Single);
                    currentLine++;
                    return;
                }

                if (int.Parse(parsedLine[0]) > 10000 && int.Parse(parsedLine[1]) > 10000)
                {
                    creditMusic.Stop();
                    currentLine++;
                    parsedLine = new List<string>();
                    parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));
                }

                if (int.Parse(parsedLine[0]) > 10000)
                {
                    creditsPlaying = true;
                    creditMusic.Play();
                    currentLine++;
                    parsedLine = new List<string>();
                    parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));
                }

                    //Check if change music or play sound effect. Music will always proceed sound effect
                if (int.Parse(parsedLine[0]) < 0 && int.Parse(parsedLine[0]) > -100)
                {
                    musicTrack.clip = music[-1 * int.Parse(parsedLine[0]) - 1];
                    musicTrack.Play();

                    currentLine++;
                    parsedLine = new List<string>();
                    parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));
                }

                //Sound effect
                if (int.Parse(parsedLine[0]) <= -100)
                {
                    soundEffect.clip = soundEffects[(-1 * int.Parse(parsedLine[0])) - 100];
                    soundEffect.Play();

                    currentLine++;
                    parsedLine = new List<string>();
                    parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));
                }

                //Movie
                if (int.Parse(parsedLine[0]) >= 1000 && int.Parse(parsedLine[0]) < 10000)
                {
                    creditMusic.volume = 0.075f;

                    //movieLength = float.Parse(parsedLine[1]);
                    dialogBox.enabled = false;
                    nameRight.enabled = false;
                    nameRightText.enabled = false;
                    nameLeft.enabled = false;
                    nameLeftText.enabled = false;
                    portraitLeft.material = portraits[0];
                    portraitRight.material = portraits[0];
                    cutsceneBackGround.material = movies[int.Parse(parsedLine[0]) - 1000];
                    //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
                    //(MovieTexture)(cutsceneBackGround.material.mainTexture).play();
                    ((MovieTexture)cutsceneBackGround.material.mainTexture).Play();

                    soundEffect.clip = movieAudio[int.Parse(parsedLine[0]) - 1000];
                    soundEffect.Play();
                    //nameLeftText.text = parsedLine[4];
                    //nameLeft.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialog.text = "";
                    dialogRight.text = "";
                    movieStarted = true;

                    return;
                }

                //FORMAT: BG. LEFTPORTRAIT. RIGHTPORTRAIT. WHATSIDENAME. NAME. COLOR. DIALOG LINE //MAYBE MUSIC BUT ILL ADD THAT LATER
                cutsceneBackGround.material = backgrounds[int.Parse(parsedLine[0])];
                portraitLeft.material = portraits[int.Parse(parsedLine[1])];
                portraitRight.material = portraits[int.Parse(parsedLine[2])];

                //See what Side name to put, what color, and what name;
                if (parsedLine[3] == "L" || parsedLine[3] == "LW")
                {
                    dialogBox.enabled = true;
                    nameRight.enabled = false;
                    nameRightText.enabled = false;
                    nameLeft.enabled = true;
                    nameLeftText.enabled = true;
                    nameLeftText.text = parsedLine[4];
                    nameLeft.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialog.text = parsedLine[8];
                    dialogRight.text = "";

                    if (parsedLine[3] == "LW")
                    {
                        nameLeftText.color = Color.white;
                    }
                    else
                    {
                        nameLeftText.color = Color.black;
                    }
                }
                else if (parsedLine[3] == "R" || parsedLine[3] == "RW")
                {
                    dialogBox.enabled = true;
                    nameRight.enabled = true;
                    nameRightText.enabled = true;
                    nameLeft.enabled = false;
                    nameLeftText.enabled = false;
                    nameRightText.text = parsedLine[4];
                    nameRight.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialogRight.text = parsedLine[8];
                    dialog.text = "";

                    if (parsedLine[3] == "RW")
                    {
                        nameRightText.color = Color.white;
                    }
                    else
                    {
                        nameRightText.color = Color.black;
                    }
                }
                else
                {
                    //No dialog stuffs
                    dialogBox.enabled = false;
                    nameRight.enabled = false;
                    nameRightText.enabled = false;
                    nameLeft.enabled = false;
                    nameLeftText.enabled = false;
                    //nameLeftText.text = parsedLine[4];
                    //nameLeft.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialog.text = "";
                    dialogRight.text = "";
                }

                // dialog.text = parsedLine[8];
            }
        }
        else
        {
            CheckCurrentProgresses();
        }
    }

    public void PlayCutscene(int sceneNo)
    {
        currentLine = -1;

        Time.timeScale = 0;

        //Enable the cutscene objects
        cutsceneBackGround.enabled = true;
        portraitRight.enabled = true;
        portraitLeft.enabled = true;
        dialogBox.enabled = true;
        nameLeft.enabled = false;
        nameRight.enabled = false;
        nameLeftText.enabled = false;
        nameRightText.enabled = false;
        dialog.enabled = true;
        dialogRight.enabled = true;

        //Read the text file line by line and read its instructions
        instructionsAsString = instructions[sceneNo].text;

        eachLine = new List<string>();
        eachLine.AddRange(instructionsAsString.Split("\n"[0]));

        // you're done.

        //Debug.Log(eachLine[4]);
        //Debug.Log(eachLine[10]);
        //Debug.Log(eachLine[101]);
        //Debug.Log(eachLine[0]);
        //int kWords = eachLine.Count;
        //Debug.Log(eachLine[kWords - 1]);


        //Also Play the selected msuic

        playing = true;
    }
}
