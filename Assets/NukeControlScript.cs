using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeControlScript : MonoBehaviour {

    public SphereCollider hitbox;
    public float durationBeforeHitboxEnable;
    private float timeNukeSpawn;

	// Use this for initialization
	void Start ()
    {
        timeNukeSpawn = Time.time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if((Time.time - timeNukeSpawn) > durationBeforeHitboxEnable)
        {
            hitbox.enabled = true;
        }	
	}
}
