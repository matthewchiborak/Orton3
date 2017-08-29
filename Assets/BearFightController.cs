using UnityEngine;
using System.Collections;

public class BearFightController : MonoBehaviour {

    public BearControllerScript[] bears;
    public int deadCount = 0;
    private bool bearsTriggered = false;

	// Use this for initialization
	void Start () {
	    
	}

    public void upDeathCount()
    {
        deadCount++;
    }
	
	// Update is called once per frame
	void Update ()
    {


	    if(StoredInfoScript.persistantInfo.getProgressLevel() == 18)
        {
            //deadCount = 0;
            if (!bearsTriggered)
            {
                for (int i = 0; i < 6; i++)
                {

                    bears[i].isFighting = true;

                }
                bearsTriggered = true;
            }

            if(deadCount == 6)
            {
                StoredInfoScript.persistantInfo.IncreaseProgress();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(StoredInfoScript.persistantInfo.getProgressLevel() == 15)
            {
                StoredInfoScript.persistantInfo.IncreaseProgress();
            }
        }
    }
}
