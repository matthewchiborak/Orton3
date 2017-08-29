using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OpeningControllerScript : MonoBehaviour {

    public Camera mainCam;
    public int numberOfCameraPoints;
    public Transform[] cameraPositions;
    public float[] timeBeforeSwitchPoints;
    private float switchCameraTimer = 0;

    public GameObject options;
    public Image cursor;
    public bool newGameSelected = false;

    public Animator anim;
    public Transform shawnTransform;

    public Image blackScreen;
    private float timeForBlackScreen = 6f;
    private float blackScreenTimer = 0;

    private float timeForBetweenSlide = 5f;
    private float betweenSlideTimer = -1;

    private float timeForNameSlide = 0.5f;
    private float timeForNameStay = 2f;
    private float nameSlideTimer = 1f;

    private int namesPast = 0;
    private int lastNameToPass = 3;
    private float namePoint1 = 500;
    private float namePoint2 = 50;
    private float namePoint3 = -50;
    private float namePoint4 = -500;

    public string[] names;
    public Vector3[] namePoints;
    public Text nameText;
    
    private bool atOptions = false;

    public Image loadingScreen;

    public RectTransform nameRectTrans;

    public AudioSource theme;
    public AudioSource openignSong;
    private bool themePlaying = false;
    private float themeTimer = 0;
    private float timeForTheme = 33f;

    public Image title;

    private bool triggerWalkAndTitleTimers = false;
    private float reachBuildingTimer = 0;
    private float timeForReachBuilding = 45f;
    private float timeForTriggerTitle = 20f;
    private float timeForTriggerFadeToBlack = 30f;
    private bool triggerBlackFade = false;

    private bool triggerTitle = false;
    private float titleAppearTimer = 0;
    private float timeForTitleAppear = 6f;
    private float secondFadeTimer = 0;

    private bool triggerBlackScreen = false;

    public Vector3 shawnStart;
    public Vector3 shawnEnd;

    public float enableSelctTimer = 0;
    public float timeForEnableSelect = 70f;

    private int currentCameraLocation = 0;


    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        mainCam.transform.position = cameraPositions[0].position;
        mainCam.transform.rotation = cameraPositions[0].rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Create a way for the player of quitting the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!atOptions)
        {
            //Force title screen
            if (Input.GetKeyDown(KeyCode.Return))
            {
                nameText.text = "";
                atOptions = true;
                blackScreen.color = Color.black;
                blackScreenTimer = timeForBlackScreen;
                theme.Play();
                namesPast = 5;
                lastNameToPass = 3;
                openignSong.Stop();
                title.color = new Vector4(1, 1, 1, 1);
                options.SetActive(true);
                newGameSelected = false;
                cursor.rectTransform.localPosition = new Vector3(75f, 100f, 0f);
                return;
            }

            if(enableSelctTimer > timeForEnableSelect)
            {
                atOptions = true;
                options.SetActive(true);
                newGameSelected = false;
                cursor.rectTransform.localPosition = new Vector3(75f, 100f, 0f);
            }
            else
            {
                enableSelctTimer += Time.deltaTime;
            }

            //Move the camera
            if(currentCameraLocation < (numberOfCameraPoints - 1))
            {
                if (switchCameraTimer < timeBeforeSwitchPoints[currentCameraLocation])
                {
                    switchCameraTimer += Time.deltaTime;

                    if (switchCameraTimer > timeBeforeSwitchPoints[currentCameraLocation])
                    {
                        currentCameraLocation++;
                        mainCam.transform.position = cameraPositions[currentCameraLocation].position;
                        mainCam.transform.rotation = cameraPositions[currentCameraLocation].rotation;
                        switchCameraTimer = 0;
                    }
                }
            }

            if (triggerWalkAndTitleTimers && reachBuildingTimer < timeForReachBuilding)
            {
                reachBuildingTimer += Time.deltaTime;
                shawnTransform.position = Vector3.Lerp(shawnStart, shawnEnd, reachBuildingTimer/ timeForReachBuilding);

                if(reachBuildingTimer > timeForTriggerTitle)
                {
                    triggerTitle = true;
                }
                if (reachBuildingTimer > timeForTriggerFadeToBlack)
                {
                    triggerBlackFade = true;
                }
            }

            if(triggerTitle && titleAppearTimer < timeForTitleAppear)
            {
                titleAppearTimer += Time.deltaTime;
                title.color = new Vector4(1, 1, 1, titleAppearTimer / timeForTitleAppear);
            }

                //Play main theme
            if (themeTimer < timeForTheme)
            {
                themeTimer += Time.deltaTime;
            }
            else if(!themePlaying)
            {
                themePlaying = true;
                theme.Play();
                triggerWalkAndTitleTimers = true;
                anim.Play("Armature|Walk", -1, 0f);
            }

            //Make Options black screen appear
            if (triggerBlackFade && secondFadeTimer < timeForBlackScreen)
            {
                //print("Got here");
                secondFadeTimer += Time.deltaTime;
                blackScreen.color = new Vector4(0, 0, 0, (secondFadeTimer/ timeForBlackScreen));
            }

            //Name slides. Sends starts the slide timer
            if (betweenSlideTimer < timeForBetweenSlide && namesPast < 5)
            {
                betweenSlideTimer += Time.deltaTime;
            }
            else if(namesPast < 5)
            {
                nameSlideTimer = 0;
                betweenSlideTimer = 0;
                lastNameToPass = 0;
                
                nameText.text = names[namesPast];
                namesPast++;
            }

            //Slide the timer from its current point to the next
            if(lastNameToPass < 3)
            {
                nameRectTrans.localPosition = Vector3.Lerp(namePoints[lastNameToPass], namePoints[lastNameToPass + 1], nameSlideTimer / timeForNameSlide);
                nameSlideTimer += Time.deltaTime;

                if(nameSlideTimer > timeForNameSlide)
                {
                    if(lastNameToPass != 0)
                    {
                        timeForNameSlide = 0.5f;
                    }
                    else
                    {
                        timeForNameSlide = 2f;
                    }

                    nameSlideTimer = 0;
                    lastNameToPass++;

                    if(lastNameToPass == 3)
                    {
                        nameText.text = "";
                    }
                }
            }

            if(namesPast > 5 && lastNameToPass > 2 && !theme.isPlaying)
            {
                theme.Play();
            }

            //if(blackScreenTimer >= timeForBlackScreen && !triggerBlackFade)
            //{
            //    blackScreen.color = new Vector4(0, 0, 0, 0);
            //}

            //Fade the black screen
            if (blackScreenTimer < timeForBlackScreen)
            {
                blackScreenTimer += Time.deltaTime;
                blackScreen.color = new Vector4(0, 0, 0, 1 - (blackScreenTimer / timeForBlackScreen));
            }
        }
        else
        {
            //Can select new game or continue
            

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(newGameSelected)
                {
                    //Set the save file to 0 progress
                    //print("New");
                    System.IO.File.WriteAllText("Assets/Resources/Save.txt", "-1");
                }
                else
                {
                    //Keep the one thats there
                    //print("Load");
                }

                loadingScreen.enabled = true;

                //Load start
                SceneManager.LoadScene("entrancestart", LoadSceneMode.Single);
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if(newGameSelected)
                {
                    newGameSelected = false;
                    cursor.rectTransform.localPosition = new Vector3(75f, 100f, 0f);
                }
                else
                {
                    newGameSelected = true;
                    cursor.rectTransform.localPosition = new Vector3(-425f, 100f, 0f);
                }
            }
        }
	}
}
