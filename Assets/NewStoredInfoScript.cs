using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;

public class NewStoredInfoScript : MonoBehaviour {

    //For shawn
    public string nameOfScene;

    public float currentHealth = 100;
    public float maxHealth = 100;
    public int itemSelected = 0;
    public float speedMulitplier = 1.0f;

    public GameObject shawnMichaels;
    public Transform shawnMichaelsTransform;
    public ShawnMichaelsControl shawnMichaelsScript;
    public Animator shawnMichaelsAnim;

    public bool[] abilityEnabled = new bool[9];

    public Material[] itemMaterials = new Material[9];
    public Image itemImage;
    public Text itemAmount;
    public Text buttonNo;
    private int bandageAmount = 2;
    private int pillsAmount = 2;
    private int beerAmount = 3;
    private int maxItems = 6;
    private int c4Amount = 2;
    private int cannonBallAmount = 8;
    private int maxBalls = 16;

    //For block out screen when load new area
    public Image loadScreen;
    public Text loadText;
    
    //Last know position of player
    public Vector3 lastPosition = new Vector3(10000f, 10000f, 10000f);
    public Vector3 resetPosition = new Vector3(10000f, 10000f, 10000f);

    //Music Fading
    private float fadeSpeed = 7f;
    private float musicFadeSpeed = 1f;
    public AudioSource backgroundMusic;
    public AudioSource panicMusic;

    //Healthbar
    public Image healthBar;

    //Music Stuff
    public AudioClip[] BGTracks;
    public int currentTrack;

    public bool ignorePlayer = false;

    //Boss stuff
    private float maxBossHealth = 100f;
    private float currentBossHealth = 100f;
    public Image bossBarBack;
    public Image bossBarFront;
    public Text bossName;
    
    private bool paused = false;
    public GameObject pausedScreen;

    public CutscenePlayer theScenePlayer;

    public bool death = false;
    private float deathTimer = 0;
    private float TimeForDie = 6f;

    public Image GameOverScreen;
    public AudioSource GameOverSong;

    private Vector2 startVect;
    private Vector2 endVect;

    public Vector2 currentDestination;
    public Image playerIcon;
    public Image destinationIcon;
    public Vector2 levelBoundsMin;
    public Vector2 levelBoundsMax;
    public Vector2 borderOffset;

    public GameObject boot1;
    public GameObject boot2;

    private bool firedShot;

    private float timeOfAlert;
    public float durationOfAlert;

    private int enemiesKilled;
    private int alerts;
    private int deaths;

    void Start()
    {
        Cursor.visible = false;

        

        //READ THE SAVE FILE AND SET EVERYTHING IN THE CORRECT POSITIONS
        //progressLevel = Int32.Parse(assetText);

        //Enable item accordingly
        //enableItem(2);

        startVect = new Vector2(1, 1);
        endVect = new Vector2(0, 0);

        showScreen();
    }

    public void setInfoOnLoad(float health, int bandage, int pil, int beer, int bombs, int balls, int kills, int alerts, int deaths)
    {
        currentHealth = health;
        bandageAmount = bandage;
        pillsAmount = pil;
        beerAmount = beer;
        c4Amount = bombs;
        cannonBallAmount = balls;
        this.enemiesKilled = kills;
        this.alerts = alerts;
        this.deaths = deaths;

        itemAmount.text = bandageAmount.ToString() + "/" + maxItems.ToString();
        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }
    public void setItems(int bandage, int pil, int beer, int bombs, int balls, int kills, int alerts, int deaths)
    {
        bandageAmount = bandage;
        pillsAmount = pil;
        beerAmount = beer;
        c4Amount = bombs;
        cannonBallAmount = balls;
        this.enemiesKilled = kills;
        this.alerts = alerts;
        this.deaths = deaths;

        itemAmount.text = bandageAmount.ToString() + "/" + maxItems.ToString();
    }
    public string getStateString()
    {
        return currentHealth.ToString() + "~" + bandageAmount.ToString() + "~" + pillsAmount.ToString() + "~" + beerAmount.ToString() + "~" + c4Amount.ToString() + "~" + cannonBallAmount.ToString();
    }
    public string getRankStrin()
    {
        return enemiesKilled.ToString() + "~" + alerts.ToString() + "~" + deaths.ToString();
    }

    public void StartBoss(string name)
    {
        bossBarBack.enabled = true;
        bossBarFront.enabled = true;
        bossName.enabled = true;
        bossName.text = name;
        currentBossHealth = maxBossHealth;
        bossBarFront.transform.localScale = new Vector3(currentBossHealth / maxBossHealth, 1, 1);
    }

    public void alert(Vector3 alertPos)
    {
        timeOfAlert = Time.time;
        lastPosition = alertPos;
    }
    public void cancelAlert()
    {
        alerts++;
        lastPosition = resetPosition;
    }

    public void addAnotherGuardToTheKillCount()
    {
        enemiesKilled++;
    }

    public void EndBoss()
    {
        //if (progressLevel == 5)
        //{
        //    enableItem(2);
        //}
        //if (progressLevel == 15)
        //{
        //    enableItem(4);
        //}

        currentBossHealth = maxBossHealth;
        bossBarBack.enabled = false;
        bossBarFront.enabled = false;
        bossName.enabled = false;
        switchTracks();
        //progressLevel++;
        //IncreaseProgress();
    }

    public void PlayCutscene(int sceneToPlay)
    {
        //progressLevel++;
        //IncreaseProgress();

        //Turn off all audiosources
        backgroundMusic.Stop();
        panicMusic.Stop();
        shawnMichaelsScript.footSteps.volume = 0;
        shawnMichaelsScript.itemSource.volume = 0;

        theScenePlayer.PlayCutscene(sceneToPlay);
    }

    public void CutsceneJustEnded()
    {
        //Turn on all audiosources
        backgroundMusic.Play();
        panicMusic.Play();
        shawnMichaelsScript.footSteps.volume = 1;
        shawnMichaelsScript.itemSource.volume = 1;

        //Boss happens afterwards
        //if (progressLevel == 3)
        //{
        //    //progressLevel++;
        //    IncreaseProgress();
        //}
        //if (progressLevel == 12)
        //{
        //    //progressLevel++;
        //    IncreaseProgress();
        //}
        ////if (progressLevel == 15)
        ////{
        ////    progressLevel++;
        ////}
        //if (progressLevel == 15)
        //{
        //    enableItem(4);
        //}
        //if (progressLevel == 17)
        //{
        //    playBearMusic();
        //    //progressLevel++;
        //    IncreaseProgress();
        //}
        //if (progressLevel == 22)
        //{
        //    enableItem(2);
        //    //progressLevel++;
        //    IncreaseProgress();
        //}
        //if (progressLevel == 26)
        //{
        //    enableItem(3);
        //    //progressLevel++;
        //    //IncreaseProgress();
        //}
        //if (progressLevel == 29)
        //{
        //    //progressLevel++;
        //    IncreaseProgress();
        //}
        //if (progressLevel == 33)
        //{
        //    enableItem(5);
        //}
    }

    public void hitBoss(float damage)
    {
        currentBossHealth -= damage;
        if (currentBossHealth < 0)
        {
            currentBossHealth = 0;
        }
        bossBarFront.transform.localScale = new Vector3(currentBossHealth / maxBossHealth, 1, 1);
    }

    public Animator getPlayerAnim()
    {
        return shawnMichaelsScript.GetPlayerAnim();
    }
    public Transform getPlayerTransform()
    {
        return shawnMichaelsTransform;
    }
    public GameObject getPlayerGameObject()
    {
        return shawnMichaels;
    }
    public ShawnMichaelsControl getPlayerScript()
    {
        return shawnMichaelsScript;
    }

    //public string getCurrentScene()
    //{
    //    return currentScene;
    //}

    public float getBossPercentage()
    {
        return currentBossHealth / maxBossHealth;
    }



    public void hitByBullet()
    {
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        //Game over if game is over
        if (currentHealth == 0)
        {
            gameOver();
        }
    }

    public void hitByCactus()
    {
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        //Game over if game is over
        if (currentHealth == 0)
        {
            gameOver();
        }
    }

    
    public void hitByDust()
    {
        currentHealth -= 7;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        //Game over if game is over
        if (currentHealth == 0)
        {
            gameOver();
        }
    }

    public void hitBySkeleton()
    {
        currentHealth -= 0.1f;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        //Game over if game is over
        if (currentHealth == 0)
        {
            gameOver();
        }
    }

    public void MapToggle()
    {
        if (!paused)
        {
            paused = true;
            //Stop time
            Time.timeScale = 0;

            float xAmount = (-825 + borderOffset.x) + ((((325 - borderOffset.x) - ((-825 + borderOffset.x))) * ((shawnMichaels.transform.position.x - levelBoundsMin.x) / (levelBoundsMax.x - levelBoundsMin.x))));
            float yAmount = (-135 + borderOffset.y) + ((((385 - borderOffset.y) - ((-135 + borderOffset.y))) * ((shawnMichaels.transform.position.z - levelBoundsMin.y) / (levelBoundsMax.y - levelBoundsMin.y))));

            //playerIcon.rectTransform.localPosition = new Vector3(xAmount + borderOffset.x, yAmount + borderOffset.y, 0);
            playerIcon.rectTransform.localPosition = new Vector3(xAmount, yAmount, 0);

            xAmount = (-825 + borderOffset.x) + ((((325 - borderOffset.x) - ((-825 + borderOffset.x))) * ((currentDestination.x - levelBoundsMin.x) / (levelBoundsMax.x - levelBoundsMin.x))));
            yAmount = (-135 + borderOffset.y) + ((((385 - borderOffset.y) - ((-135 + borderOffset.y))) * ((currentDestination.y - levelBoundsMin.y) / (levelBoundsMax.y - levelBoundsMin.y))));

            destinationIcon.rectTransform.localPosition = new Vector3(xAmount, yAmount, 0);

            pausedScreen.SetActive(true);
        }
        else
        {
            paused = false;
            Time.timeScale = 1.0f;
            pausedScreen.SetActive(false);
        }
    }

    public void gameOver()
    {
        if (!death)
        {
            GameOverScreen.enabled = true;
            Time.timeScale = 0.2f;
            death = true;
            GameOverSong.Play();
            deaths++;
        }
    }

    public void ReloadFromGameOver()
    {
        Time.timeScale = 1.0f;

        //StoredInfoScript.persistantInfo.blockScreen();
        //blockScreen();

        //currentHealth = maxHealth;
        //healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        //currentBossHealth = maxBossHealth;
        //bossBarFront.transform.localScale = new Vector3(currentBossHealth / maxBossHealth, 1, 1);

        //if(progressLevel == 5)
        //{
        //    progressLevel = 2;
        //}

        //if (progressLevel == 24)
        //{
        //    progressLevel = 21;
        //}

        //other.gameObject.GetComponentInParent<Transform>().position = playerLocation;
        //shawnMichaels.transform.position = lastLoadLocation;
        //other.gameObject.GetComponentInParent<Transform>().rotation = Quaternion.Euler(playerRotation);
        //shawnMichaels.transform.rotation = Quaternion.Euler(lastLoadRotation);
        //SceneManager.LoadScene("fight", LoadSceneMode.Single);

        //StoredInfoScript.persistantInfo.lastPosition = StoredInfoScript.persistantInfo.resetPosition;

        //CHECK THE SAVE FILE FOR WHERE GAME CHECKPOINTED

        System.IO.File.WriteAllText("Assets/Resources/Rank.txt", getRankStrin());

        SceneManager.LoadScene(nameOfScene, LoadSceneMode.Single);
        //SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }

    public void reloadFromLastCheckpoint()
    {
        blockScreen();
        Time.timeScale = 1.0f;
        System.IO.File.WriteAllText("Assets/Resources/Rank.txt", getRankStrin());
        SceneManager.LoadScene(nameOfScene, LoadSceneMode.Single);
    }

    public void useHealthPack()
    {
        currentHealth += 50;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }

    public void resetMusic()
    {
        backgroundMusic.volume = 1f;
        panicMusic.volume = 0f;
    }

    public void PlayBossMusic()
    {
        currentTrack = 0;
        backgroundMusic.clip = BGTracks[0];
        backgroundMusic.Play();
    }

    public void playFinalBossMusic()
    {
        currentTrack = 2;
        backgroundMusic.clip = BGTracks[2];
        backgroundMusic.Play();
    }

    public void playBearMusic()
    {
        currentTrack = 3;
        backgroundMusic.clip = BGTracks[3];
        backgroundMusic.Play();
    }

    public void switchTracks()
    {
        if (currentTrack != 0)
        {
            currentTrack = 0;
            backgroundMusic.clip = BGTracks[0];
            backgroundMusic.Play();
        }
        //if (currentScene == "backstage" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "bar")
        //{
        //    backgroundMusic.clip = BGTracks[1];
        //    currentTrack = 1;
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "cubicles" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "entrance" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "food" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "forest")
        //{
        //    backgroundMusic.clip = BGTracks[5];
        //    currentTrack = 5;
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "garage" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "gym" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "hallway" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "lockerroom" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "office")
        //{
        //    backgroundMusic.clip = BGTracks[10];
        //    currentTrack = 10;
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "outside")
        //{
        //    backgroundMusic.clip = BGTracks[11];
        //}
        //else if (currentScene == "outsideDay")
        //{
        //    backgroundMusic.clip = BGTracks[12];
        //}
        //else if (currentScene == "shop" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "space")
        //{
        //    backgroundMusic.clip = BGTracks[14];
        //}
        //else if (currentScene == "stairway")
        //{
        //    backgroundMusic.clip = BGTracks[15];
        //    currentTrack = 15;
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "test" && currentTrack != 16)
        //{
        //    currentTrack = 16;
        //    backgroundMusic.clip = BGTracks[16];
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "void")
        //{
        //    backgroundMusic.clip = BGTracks[17];
        //    currentTrack = 17;
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "washroom")
        //{
        //    backgroundMusic.clip = BGTracks[10];
        //    currentTrack = 10;
        //    backgroundMusic.Play();
        //}
        //else if (currentScene == "testground")
        //{
        //    backgroundMusic.clip = BGTracks[0];
        //    currentTrack = 0;
        //    backgroundMusic.Play();
        //}
    }

    public void MusicFading()
    {
        if (lastPosition != resetPosition)
        {
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, 1f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, 1f, musicFadeSpeed * Time.deltaTime);
            panicMusic.volume = Mathf.Lerp(panicMusic.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }

    //public int getProgressLevel()
    //{
    //    return progressLevel;
    //}

    public bool checkIfPaused()
    {
        return paused;
    }

    public void blockScreen()
    {
        loadScreen.enabled = true;
        loadText.enabled = true;
    }

    public void showScreen()
    {
        loadScreen.enabled = false;
        loadText.enabled = false;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    void Awake()
    {
        //if (persistantInfo == null)
        {
            //DontDestroyOnLoad(gameObject);
            //persistantInfo = this;
            abilityEnabled[0] = true; //Only 1 and 0 will be true when done testing
            abilityEnabled[1] = true;
            abilityEnabled[2] = true;
            abilityEnabled[3] = true;
            abilityEnabled[4] = true;
            //abilityEnabled[5] = true;
            //abilityEnabled[6] = true;
            //abilityEnabled[7] = true;
            //abilityEnabled[8] = true;

            if(abilityEnabled[8])
            {
                boot1.SetActive(true);
                boot2.SetActive(true);
            }
            else
            {
                boot1.SetActive(false);
                boot2.SetActive(false);
            }

            UnityEngine.Random.InitState(System.DateTime.Now.Second);
            itemImage.material = itemMaterials[0];
            itemAmount.text = bandageAmount.ToString() + "/" + maxItems.ToString();
            buttonNo.text = "1";
        }
        //else if (persistantInfo != this)
        //{
        //    Destroy(persistantInfo);
        //}
    }
    
    public void enableItem(int itemNumber)
    {
        abilityEnabled[itemNumber] = true;
    }
    public bool checkIfItemEnabled(int itemNumber)
    {
        return abilityEnabled[itemNumber];
    }
    
    public void pickupItem(int itemId)
    {
        if (itemId == 0)
        {
            if (bandageAmount >= maxItems)
            {
                return;
            }

            bandageAmount++;
            if (itemSelected == 0)
            {
                itemAmount.text = bandageAmount.ToString() + "/" + maxItems.ToString();
            }
        }
        if (itemId == 1)
        {
            if (pillsAmount >= maxItems)
            {
                return;
            }

            pillsAmount++;
            if (itemSelected == 1)
            {
                itemAmount.text = pillsAmount.ToString() + "/" + maxItems.ToString();
            }
        }
        if (itemId == 4)
        {
            if (beerAmount >= maxItems)
            {
                return;
            }

            beerAmount++;
            if (itemSelected == 4)
            {
                itemAmount.text = beerAmount.ToString() + "/" + maxItems.ToString();
            }
        }
        if (itemId == 5)
        {
            if (c4Amount >= maxItems)
            {
                return;
            }
            
            c4Amount++;
            if (itemSelected == 5)
            {
                itemAmount.text = c4Amount.ToString() + "/" + maxItems.ToString();
            }
        }
        if (itemId == 7)
        {
            if (cannonBallAmount >= maxBalls)
            {
                return;
            }

            cannonBallAmount += 4;
            if (itemSelected == 7)
            {
                itemAmount.text = cannonBallAmount.ToString() + "/" + maxBalls.ToString();
            }
        }
    }

    public bool checkIfEnough()
    {
        int itemNo = itemSelected;

        if (itemNo == 0)
        {
            if (bandageAmount > 0)
            {
                bandageAmount--;
                itemAmount.text = bandageAmount.ToString() + "/" + maxItems.ToString();

                //Increase the health
                currentHealth += 50;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                return true;
            }
        }
        else if (itemNo == 1)
        {
            if (pillsAmount > 0)
            {
                pillsAmount--;
                itemAmount.text = pillsAmount.ToString() + "/" + maxItems.ToString();
                speedMulitplier = 2.0f;
                return true;
            }
        }
        else if (itemNo == 4)
        {
            if (beerAmount > 0)
            {
                beerAmount--;
                itemAmount.text = beerAmount.ToString() + "/" + maxItems.ToString();
                return true;
            }
        }
        else if (itemNo == 5)
        {
            if (c4Amount > 0)
            {
                c4Amount--;
                itemAmount.text = c4Amount.ToString() + "/" + maxItems.ToString();
                return true;
            }
        }
        else if (itemNo == 6)
        {
            if (beerAmount > 0)
            {
                beerAmount--;
                itemAmount.text = beerAmount.ToString() + "/" + maxItems.ToString();
                return true;
            }
        }
        else if (itemNo == 7)
        {
            if (cannonBallAmount > 0)
            {
                cannonBallAmount--;
                itemAmount.text = cannonBallAmount.ToString() + "/" + maxBalls.ToString();
                return true;
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public void selectItem(int item)
    {
        //Check if the item is enabled
        if (!abilityEnabled[item])
        //if (abilityEnabled[item])
        {
            return;
        }

        itemSelected = item;

        buttonNo.text = (item + 1).ToString();

        //Swap the UI
        itemImage.material = itemMaterials[item];

        //Swap the quantity if its a resource that can be used up
        if (item == 0)
        {
            itemAmount.text = bandageAmount.ToString() + "/" + maxItems.ToString();
        }
        else if (item == 1)
        {
            itemAmount.text = pillsAmount.ToString() + "/" + maxItems.ToString();
        }
        else if (item == 4)
        {
            itemAmount.text = beerAmount.ToString() + "/" + maxItems.ToString();
        }
        else if (item == 5)
        {
            itemAmount.text = c4Amount.ToString() + "/" + maxItems.ToString();
        }
        else if (item == 6)
        {
            itemAmount.text = beerAmount.ToString() + "/" + maxItems.ToString();
        }
        else if (item == 7)
        {
            itemAmount.text = cannonBallAmount.ToString() + "/" + maxBalls.ToString();
        }
        else
        {
            itemAmount.text = "";
        }
    }

    // Use this for initialization
    //   void Start () {

    //}
    public void setFiredShot(bool status)
    {
        firedShot = status;
    }
    public bool getFiredShot()
    {
        return firedShot;
    }

    public void loadMainMenu()
    {
        blockScreen();
        Time.timeScale = 1.0f;
        System.IO.File.WriteAllText("Assets/Resources/Rank.txt", getRankStrin());
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void tryIncreaseSelectedItem()
    {
        int targetItem = itemSelected + 1;

        while(true)
        {
            if (targetItem == abilityEnabled.Length - 1)
            {
                targetItem = 0;
            }

            if(abilityEnabled[targetItem])
            {
                break;
            }
            else
            {
                targetItem++;
            }
        }

        selectItem(targetItem);
    }
    public void tryDecreaseSelectedItem()
    {
        int targetItem = itemSelected - 1;

        while (true)
        {
            if (targetItem < 0)
            {
                targetItem = abilityEnabled.Length - 2;
            }

            if (abilityEnabled[targetItem])
            {
                break;
            }
            else
            {
                targetItem--;
            }
        }

        selectItem(targetItem);
    }

    public bool checkIfAlertCancelled()
    {
        return (Time.time - timeOfAlert) > durationOfAlert;
    }

    //// Update is called once per frame
    void Update()
    {
        MusicFading();

        if(lastPosition != resetPosition)
        {
            if ((Time.time) - timeOfAlert > durationOfAlert)
            {
                cancelAlert();
            }
        }

        if (death)
        {
            Time.timeScale = 0.2f;

            if (deathTimer < TimeForDie)
            {
                deathTimer += Time.deltaTime;
                GameOverScreen.color = new Color(1, 1, 1, deathTimer * 2 / TimeForDie);
                //backgroundMusic.volume = Vector2.Lerp(startVect, endVect, deathTimer * 2 / TimeForDie).x;
                //panicMusic.volume = Vector2.Lerp(startVect, endVect, deathTimer * 2 / TimeForDie).x;
                backgroundMusic.volume = 0;
                panicMusic.volume = 0;
            }
            else
            {
                GameOverScreen.color = new Color(1, 1, 1, 0);
                blockScreen();
                GameOverScreen.enabled = false;
                death = false;
                deathTimer = 0;
                ReloadFromGameOver();
                backgroundMusic.volume = 1;
                panicMusic.volume = 0;
            }
        }
    }
}
