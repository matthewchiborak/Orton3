using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayCrawlControl : MonoBehaviour
{
    public Transform shawnTransform;

    private bool keyPressed;

    private int timesPressed;

    public int pressesBeforeMove;
    private bool oneArm;

    public Animator anim;

    private bool moving;
    public float durationOfMove;
    private float timeMoveBegan;
    private Vector3 startPos;
    private Vector3 endPos;
    public float distanceInEachCrawl;

    private int numberOfPresses2;
    private int numberOfPresses3;

    private bool exp1;
    private bool exp2;
    public ParticleSystem explosion1;
    public ParticleSystem explosion2;

    public AudioSource explosionSource;

    // Use this for initialization
    void Start()
    {
        numberOfPresses2 = (int)(1.5 * pressesBeforeMove);
        numberOfPresses3 = 2 * pressesBeforeMove;
    }

    // Update is called once per frame
    void Update()
    {
        if(shawnTransform.position.z < -250 && shawnTransform.position.z > -425)
        {
            if(!exp1)
            {
                exp1 = true;
                explosion1.Play();
                explosionSource.Play();
            }

            pressesBeforeMove = numberOfPresses2;
        }
        else if(shawnTransform.position.z < -425)
        {
            if (!exp2)
            {
                exp2 = true;
                explosion2.Play();
                explosionSource.Play();
            }

            pressesBeforeMove = numberOfPresses3;
        }

        if(moving)
        {
            shawnTransform.position = Vector3.Lerp(startPos, endPos, (Time.time - timeMoveBegan) / durationOfMove);
            if ((Time.time - timeMoveBegan) > durationOfMove)
            {
                moving = false;
            }
        }

        if(timesPressed > pressesBeforeMove && !moving)
        {
            if(oneArm)
            {
                oneArm = false;
                timesPressed = 0;
                anim.Play("Armature|Crawl1", -1, 0);
                moving = true;
                startPos = shawnTransform.position;
                timeMoveBegan = Time.time;
                endPos = new Vector3(startPos.x, startPos.y, startPos.z - distanceInEachCrawl);
            }
            else
            {
                oneArm = true;
                timesPressed = 0;
                anim.Play("Armature|Crawl2", -1, 0);
                moving = true;
                startPos = shawnTransform.position;
                timeMoveBegan = Time.time;
                endPos = new Vector3(startPos.x, startPos.y, startPos.z - distanceInEachCrawl);
            }
        }
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxisRaw("Vertical");

        if(!keyPressed && moveVertical > 0)
        {
            keyPressed = true;
            timesPressed++;
        }

        if (keyPressed && moveVertical == 0)
        {
            keyPressed = false;
        }
    }
}
