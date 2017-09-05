using UnityEngine;
using System.Collections;

public class ShawnMichaelsControl : MonoBehaviour {

    public NewStoredInfoScript storedInfo;

    public Animator anim;
    public Animator anim2; //ForCyborg

    private bool isHuman;
    //public GameObject humanModel; use bodymodel
    public GameObject cyborgModel;
    public GameObject cyborgHair;
    public GameObject humanHair;

    public float walkSpeed;

    public float runSpeed;

    private Transform transform;
    private Rigidbody rb;
    private Vector3 movement;
    private Quaternion rotation;

    //private int itemSelected;

    //Make rotation smooth
    Quaternion target;
    public float smooth = 2.0f;

    private float currentRotation;

    private bool turnKeyPressed;

    private bool controlsEnabled;

    public FollowPlayer followplayer;
    public Camera cam;

    private int cooldown;
    private int invincibility;
    private bool sockoing;

    public AudioSource itemSource;
    public AudioClip itemUse;
    public AudioClip kickSwish;

    public AudioSource footSteps;
   // public AudioClip walkSteps;
    public AudioClip runSteps;

    private float crouchCount = 0.0f;

    private bool enterExitRing = false;
    private int enterExitRingCount = 0;
    private Vector3 startRing;
    private Vector3 ringTarget;
    private float enterRingDemon = 30.0f;

    private float maxRopeHeight = 15.0f + 26.09f;

    public BoxCollider sweetChinHitbox;
    public BoxCollider cyborgChitHitbox;

    public AudioSource stompSource;

    private float spawnBoltTimer = -1f;
    public GameObject bolt;
    public GameObject usedCan;

    private bool invisibleOn = false;
    public GameObject bodyModel;
    private float CMETimer = -1f;

    //Socko Stuff
    public GameObject sockoObject;

    public AudioSource hurtSound;

    private bool onePress = false;

    public BoxCollider playerHitBox;
   

    // Use this for initialization
    void Start ()
    {
        transform = GetComponent<Transform>();
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rotation = Quaternion.Euler(0, 0, 0); //current Rotation
        currentRotation = 0;
        turnKeyPressed = false;
        controlsEnabled = true;
        target = Quaternion.Euler(0, 0, 0);
        cooldown = 0;
        sockoing = false;
        invincibility = 0;

        isHuman = true;
        //cyborgModel.SetActive(false);
        cyborgModel.SetActive(false);
        cyborgHair.SetActive(false);
    }

    public void SwitchToCyborg()
    {
        isHuman = false;
        bodyModel.SetActive(false);
        humanHair.SetActive(false);
        cyborgModel.SetActive(true);
        cyborgHair.SetActive(true);
    }

    public Transform getTransform()
    {
        return transform;
    }

    public Animator GetPlayerAnim()
    {
        if(isHuman)
        {
            return anim;
        }
        else
        {
            return anim2;
        }
    }

    void useItem()
    {
        //CHeck if enough of an item to use
        if(!storedInfo.checkIfEnough())
        {
            return;
        }

        if(storedInfo.itemSelected == 0)
        {
            //Heal
            if (isHuman)
            {
                anim.Play("Armature|Idle", -1, 0f);
            }
            else
            {
                anim2.Play("Armature|Idle", -1, 0f);
            }
            controlsEnabled = false;
            cooldown = 5;
            itemSource.clip = itemUse;
            itemSource.Play();
            storedInfo.useHealthPack();
        }
        else if (storedInfo.itemSelected == 1)
        {
            //HGH
            if (isHuman)
                anim.Play("Armature|Idle", -1, 0f);
            else
                anim2.Play("Armature|Idle", -1, 0f);
            controlsEnabled = false;
            cooldown = 5;
            itemSource.clip = itemUse;
            itemSource.Play();
        }
        else if (storedInfo.itemSelected == 2)
        {
            if (isHuman)
                anim.Play("Armature|Stomp", -1, 0f);
            else
                anim2.Play("Armature|Stomp", -1, 0f);
            controlsEnabled = false;
            stompSource.Play();
            cooldown = 60;
            spawnBoltTimer = 0.75f;
        }
        else if (storedInfo.itemSelected == 3)
        {
            if (isHuman)
                anim.Play("Armature|Socko", -1, 0f);
            else
                anim2.Play("Armature|Socko", -1, 0f);
            controlsEnabled = false;
            sockoing = true;
        }
        else if (storedInfo.itemSelected == 4)
        {
            if (isHuman)
                anim.Play("Armature|Putdown", -1, 0f);
            else
                anim2.Play("Armature|Putdown", -1, 0f);
            controlsEnabled = false;
            cooldown = 60;
            Instantiate(usedCan, new Vector3((float)(transform.position.x), (float)(transform.position.y ), (float)(transform.position.z)), transform.rotation);
        }
        else if (storedInfo.itemSelected == 5)
        {
            if (isHuman)
                anim.Play("Armature|UCantCMe", -1, 0f);
            else
                anim2.Play("Armature|UCantCMe", -1, 0f);
            controlsEnabled = false;
            cooldown = 180;
            CMETimer = 3f;
        }
    }

    void selectItem()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            storedInfo.selectItem(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            storedInfo.selectItem(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            storedInfo.selectItem(2);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            storedInfo.selectItem(3);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            storedInfo.selectItem(4);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            storedInfo.selectItem(5);
        }
    }

    public void enterOrExitRing(float middleY, Vector3 inRing, Vector3 outRing)
    {
        

        if(anim.GetBool("IsRunning") && cooldown <= 0)
        {
            if(transform.position.y < middleY && transform.position.y < maxRopeHeight)
            {
                //Enter the ring
                cooldown = 60;
                enterRingDemon = 30.0f;
                controlsEnabled = false;
                anim.SetBool("IsRunning", false);
                anim.Play("Armature|SlideIn", -1, 0f);
                footSteps.Stop();
                if(inRing.x > 1000)
                {
                    inRing.x = transform.position.x;
                }
                if (inRing.z > 1000)
                {
                    inRing.z = transform.position.z;
                }
                //transform.position = inRing;
                startRing = new Vector3(transform.position.x, inRing.y - 2, transform.position.z);
                ringTarget = inRing;
                enterExitRing = true;
            }
            else if (transform.position.y > middleY && transform.position.y < maxRopeHeight)
            {
                //Exit the ring
                cooldown = 30;
                enterRingDemon = 45.0f;
                controlsEnabled = false;
                anim.SetBool("IsRunning", false);
                anim.Play("Armature|JumpOut", -1, 0f);
                footSteps.Stop();
                if (outRing.x > 1000)
                {
                    outRing.x = transform.position.x;
                }
                if (outRing.z > 1000)
                {
                    outRing.z = transform.position.z;
                }
                startRing = transform.position;
                ringTarget = outRing;
                enterExitRing = true;
                //transform.position = outRing;
            }
        }
    }

    public void hitByBullet()
    {
        if (invincibility <= 0)
        {
            if (isHuman)
            {
                anim.SetBool("IsCrouching", false);
                storedInfo.hitByBullet();
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
                hurtSound.Play();
                footSteps.Stop();
                invincibility = 360;
                cooldown = 220;
                controlsEnabled = false;
                anim.Play("Armature|Hit", -1, 0f);
            }
            else
            {
                anim2.SetBool("IsCrouching", false);
                storedInfo.hitByBullet();
                anim2.SetBool("IsWalking", false);
                anim2.SetBool("IsRunning", false);
                hurtSound.Play();
                footSteps.Stop();
                invincibility = 360;
                cooldown = 220;
                controlsEnabled = false;
                anim2.Play("Armature|Hit", -1, 0f);
            }
        }
    }

    public void hitByTrump()
    {
        if (invincibility <= 0)
        {
            if (isHuman)
            {
                anim.SetBool("IsCrouching", false);
                storedInfo.hitByBullet();
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
                hurtSound.Play();
                footSteps.Stop();
                invincibility = 300;
                cooldown = 220;
                controlsEnabled = false;
                anim.Play("Armature|Hit", -1, 0f);
            }
            else
            {
                anim2.SetBool("IsCrouching", false);
                storedInfo.hitByBullet();
                anim2.SetBool("IsWalking", false);
                anim2.SetBool("IsRunning", false);
                hurtSound.Play();
                footSteps.Stop();
                invincibility = 300;
                cooldown = 220;
                controlsEnabled = false;
                anim2.Play("Armature|Hit", -1, 0f);
            }
        }
    }

    public void hitByDust()
    {
        if (invincibility <= 0)
        {
            if (isHuman)
            {
                anim.SetBool("IsCrouching", false);
                storedInfo.hitByDust();
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);

                hurtSound.Play();

                footSteps.Stop();
                invincibility = 360;
                cooldown = 220;
                controlsEnabled = false;
                anim.Play("Armature|Hit", -1, 0f);
            }
            else
            {
                anim2.SetBool("IsCrouching", false);
                storedInfo.hitByDust();
                anim2.SetBool("IsWalking", false);
                anim2.SetBool("IsRunning", false);

                hurtSound.Play();

                footSteps.Stop();
                invincibility = 360;
                cooldown = 220;
                controlsEnabled = false;
                anim2.Play("Armature|Hit", -1, 0f);
            }
        }
    }

    public void hitByCactus()
    {
        if (invincibility <= 0)
        {
            storedInfo.hitByCactus();

            hurtSound.Play();
        }
    }

    public void KnockDown()
    {
        if (isHuman)
        {
            anim.SetBool("IsCrouching", false);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            hurtSound.Play();
            footSteps.Stop();
            invincibility = 360;
            cooldown = 220;
            controlsEnabled = false;
            anim.Play("Armature|Hit", -1, 0f);
        }
        else
        {
            anim2.SetBool("IsCrouching", false);
            anim2.SetBool("IsWalking", false);
            anim2.SetBool("IsRunning", false);
            hurtSound.Play();
            footSteps.Stop();
            invincibility = 360;
            cooldown = 220;
            controlsEnabled = false;
            anim2.Play("Armature|Hit", -1, 0f);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.y < -1000)
        {
            //transform.position = storedInfo.lastLoadLocation;
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetButtonDown("Jump") && !onePress)
        {
            onePress = true;
            storedInfo.MapToggle();
        }

        if(Input.GetButtonUp("Jump"))
        {
            onePress = false;
        }

        if(anim.GetBool("IsCrouching"))
        {
            playerHitBox.center = new Vector3(playerHitBox.center.x, 0.41f, playerHitBox.center.z);
            playerHitBox.size = new Vector3(playerHitBox.size.x, 0.8f, playerHitBox.size.z);
        }
        else
        {
            playerHitBox.center = new Vector3(playerHitBox.center.x, 0.74f, playerHitBox.center.z);
            playerHitBox.size = new Vector3(playerHitBox.size.x, 1.47f, playerHitBox.size.z);
        }

        if(enterExitRing)
        {
            transform.position = Vector3.Lerp(startRing, ringTarget, enterExitRingCount / enterRingDemon);
            enterExitRingCount++;
            if(enterExitRingCount == enterRingDemon)
            {
                enterExitRingCount = 0;
                enterExitRing = false;
                //anim.Play("Armature|Idle", -1, 0f);
            }
        }

        //Socko stuff
        if(sockoing && isHuman)
        {
            sockoObject.SetActive(true);
        }
        else
        {
            sockoObject.SetActive(false);
        }

        //Make sure stay invisible if load new scene
        if(invisibleOn)
        {
            storedInfo.ignorePlayer = true;
        }

        if(CMETimer > 0)
        {
            CMETimer -= Time.deltaTime;
            if(CMETimer <= 0)
            {
                if(invisibleOn)
                {
                    invisibleOn = false;
                    if (isHuman)
                    {
                        bodyModel.SetActive(true);
                    }
                    else
                    {
                        cyborgModel.SetActive(true);
                    }
                    storedInfo.ignorePlayer = false;
                }
                else
                {
                    invisibleOn = true;
                    if (isHuman)
                    {
                        bodyModel.SetActive(false);
                    }
                    else
                    {
                        cyborgModel.SetActive(false);
                    }
                    storedInfo.ignorePlayer = true;
                }
            }
        }

        if(spawnBoltTimer > 0)
        {
            spawnBoltTimer -= Time.deltaTime;
            if(spawnBoltTimer <= 0)
            {
                //Spawn bolt
                Instantiate(bolt, new Vector3((float)(transform.position.x), (float)(transform.position.y + 0.5f), (float)(transform.position.z)), transform.rotation);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        
        if (cooldown <= 0)
        {
            if (Input.GetButtonDown("Fire1") && !anim.GetBool("IsCrouching"))
            {
                if (isHuman)
                {
                    sweetChinHitbox.enabled = true;
                    anim.Play("Armature|SweetChinMusic", -1, 0f);
                    controlsEnabled = false;
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsRunning", false);
                    footSteps.Stop();
                    itemSource.clip = kickSwish;
                    itemSource.Play();
                    cooldown = 60;
                }
                else
                {

                    cyborgChitHitbox.enabled = true;
                    anim2.Play("Armature|SweetChinMusic", -1, 0f);
                    controlsEnabled = false;
                    anim2.SetBool("IsWalking", false);
                    anim2.SetBool("IsRunning", false);
                    footSteps.Stop();
                    itemSource.clip = kickSwish;
                    itemSource.Play();
                    cooldown = 60;
                }
            }
            if (Input.GetButtonDown("Fire2") && !anim.GetBool("IsCrouching"))
            {
                //Use selected item
                useItem();
            }
        }

        //Reduce speed powerup
        if(storedInfo.speedMulitplier > 1.0)
        {
            storedInfo.speedMulitplier -= 0.005f;
        }
        if (storedInfo.speedMulitplier < 1.0)
        {
            storedInfo.speedMulitplier += 0.005f;
        }

        if (sockoing)
        {
            if(!Input.GetButton("Fire2"))
            {
                sockoing = false;
                controlsEnabled = true;
                if(isHuman)
                    anim.Play("Armature|Idle", -1, 0f);
                else
                    anim2.Play("Armature|Idle", -1, 0f);
            }
            else
            {
                return;
            }
        }

        if(invincibility > 0)
        {
            invincibility--;
        }

        //Decrement the cooldown counter
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            if (isHuman)
            {
                sweetChinHitbox.enabled = false;
            }
            else
            {
                cyborgChitHitbox.enabled = false;
            }
            controlsEnabled = true;
        }
    }

    //Movement with camera rotation doesn't rotate the player
    void Move2(float moveHorizontal, float moveVertical)
    {
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            //Play the walking animation
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isHuman)
                {
                    if (!anim.GetBool("IsRunning"))
                    {
                        footSteps.clip = runSteps;
                        footSteps.Play();
                    }
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsRunning", true);
                }
                else
                {
                    if (!anim2.GetBool("IsRunning"))
                    {
                        footSteps.clip = runSteps;
                        footSteps.Play();
                    }
                    anim2.SetBool("IsWalking", false);
                    anim2.SetBool("IsRunning", true);
                }
            }
            else
            {
                if (isHuman)
                {
                    if (!anim.GetBool("IsWalking"))
                    {
                        //footSteps.clip = walkSteps;
                        footSteps.Stop();
                    }
                    anim.SetBool("IsRunning", false);
                    anim.SetBool("IsWalking", true);
                }
                else
                {
                    if (!anim2.GetBool("IsWalking"))
                    {
                        //footSteps.clip = walkSteps;
                        footSteps.Stop();
                    }
                    anim2.SetBool("IsRunning", false);
                    anim2.SetBool("IsWalking", true);
                }
            }

            float cameraAngle = cam.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, 0, 0);

            //Rotate the player based on the playre input and camera rotation
            if (moveVertical > 0)
            {
                if (moveHorizontal > 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle + 45.0f, 0);
                }
                else if (moveHorizontal < 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle - 45.0f, 0);
                }
                else if (moveHorizontal == 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle, 0);
                }
            }
            else if (moveVertical < 0)
            {
                if (moveHorizontal > 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle + 135.0f, 0);
                }
                else if (moveHorizontal < 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle + 225.0f, 0);
                }
                else if (moveHorizontal == 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle + 180.0f, 0);
                }
            }
            else if (moveVertical == 0)
            {
                if (moveHorizontal > 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle + 90.0f, 0);
                }
                else if (moveHorizontal < 0)
                {
                    //rotation 
                    target = Quaternion.Euler(0, cameraAngle - 90.0f, 0);
                }
            }

            //Rotate the player
            //gameObject.transform.rotation = rotation;

            //Move the player
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement = rb.transform.forward * storedInfo.speedMulitplier * runSpeed * Time.deltaTime;
            }
            else
            {
                movement = rb.transform.forward * storedInfo.speedMulitplier * walkSpeed * Time.deltaTime;
            }
            rb.MovePosition(transform.position + movement);
        }
        else if((isHuman && (anim.GetBool("IsWalking") || anim.GetBool("IsRunning"))) || (!isHuman && (anim2.GetBool("IsWalking") || anim2.GetBool("IsRunning"))))
        {
            //Play the idle animation
            if (isHuman)
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
                footSteps.Stop();
                anim.Play("Armature|Idle", -1, 0f);
            }
            else
            {
                anim2.SetBool("IsWalking", false);
                anim2.SetBool("IsRunning", false);
                footSteps.Stop();
                anim2.Play("Armature|Idle", -1, 0f);
            }
        }
    }

    void FixedUpdate()
    {
        //Get user input and move the player if the game is still in progess
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Check if user is selecting an item
        selectItem();

        //Check if crouching
        if (Input.GetKey(KeyCode.LeftControl) && cooldown <=0)
        {
            if (isHuman)
            {
                controlsEnabled = false;
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsCrouching", true);
                sockoing = false;
                footSteps.Stop();
                anim.Play("Armature|Crouch", -1, crouchCount);
                crouchCount += 0.00833f;
                if (crouchCount >= 1.05)
                {
                    crouchCount = 0;
                }
            }
            else
            {
                controlsEnabled = false;
                anim2.SetBool("IsWalking", false);
                anim2.SetBool("IsRunning", false);
                anim2.SetBool("IsCrouching", true);
                sockoing = false;
                footSteps.Stop();
                anim2.Play("Armature|Crouch", -1, crouchCount);
                crouchCount += 0.00833f;
                if (crouchCount >= 1.05)
                {
                    crouchCount = 0;
                }
            }
        }
        else if((isHuman && anim.GetBool("IsCrouching")) || (!isHuman && anim2.GetBool("IsCrouching")))
        {
            if (isHuman)
            {
                anim.SetBool("IsCrouching", false);
                anim.Play("Armature|Idle", -1, 0f);
                controlsEnabled = true;
            }
            else
            {
                anim2.SetBool("IsCrouching", false);
                anim2.Play("Armature|Idle", -1, 0f);
                controlsEnabled = true;
            }
        }

        if (controlsEnabled)
        {
            //Move(moveHorizontal, moveVertical);
            Move2(moveHorizontal, moveVertical);
        }
    }
}
