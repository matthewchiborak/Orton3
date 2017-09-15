using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarControlScript : MonoBehaviour {

    private int cooldown;
    public Animator anim;

    public GameObject grenade;
    private bool fireShot;

    public Transform shotPoint;
    public GameObject orton;

    private GameObject tempGren;

    public AudioSource grenadeLauncherSource;

    private float totalMouseX;
    private float totalMouseY;

    // Use this for initialization
    void Start ()
    {
        cooldown = 0;
        totalMouseX = 0;
        totalMouseY = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        totalMouseX += Input.GetAxis("Mouse X");
        totalMouseY += Input.GetAxis("Mouse Y");
        totalMouseX = Mathf.Clamp(totalMouseX, -130f / 4, 130f / 4);
        totalMouseY = Mathf.Clamp(totalMouseY, -7.3f / 2, 22f / 2);

        orton.transform.localRotation = Quaternion.Euler(totalMouseY * 2, totalMouseX * 2 + 180, 0);

        if (cooldown <= 0 && Input.GetButtonDown("Fire1"))
        {
            anim.Play("Armature|Shoot", -1, 0f);
            fireShot = true;
            cooldown = 60;
        }

        if(cooldown < 30 && fireShot)
        {
            grenadeLauncherSource.Play();
            tempGren = Instantiate(grenade, shotPoint.position, Quaternion.Euler(orton.transform.localRotation.eulerAngles.x * -1, orton.transform.localRotation.eulerAngles.y, orton.transform.localRotation.eulerAngles.z));// orton.transform.localRotation);
            fireShot = false;
        }

        if(cooldown > 0)
        {
            cooldown--;
        }
    }
}
