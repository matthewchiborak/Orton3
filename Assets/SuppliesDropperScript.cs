using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesDropperScript : MonoBehaviour {

    public GameObject supplyToDrop;
    private GameObject supplyInstance;

    public float durationBetweenDrop;
    private float timeOfLastDrop;

    public GameObject undertaker;
    public float durationOfUndertaker;

	// Use this for initialization
	void Start ()
    {
        timeOfLastDrop = Time.time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if((Time.time - timeOfLastDrop) > durationOfUndertaker)
        {
            if(undertaker != null)
                undertaker.SetActive(false);
        }

		if((Time.time - timeOfLastDrop) > durationBetweenDrop)
        {
            if(supplyInstance != null)
            {
                Destroy(supplyInstance);
            }

            if(undertaker != null)
            {
                undertaker.SetActive(true);
            }

            supplyInstance = Instantiate(supplyToDrop, transform.position, Quaternion.identity);
            timeOfLastDrop = Time.time;
        }
	}
}
