using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class CheckpointTriggerScript : MonoBehaviour {

    public LevelID levelID;
    public int checkPointID;

    public CheckpointTextControl checkpointReachedText;

    public Collider hitbox;
    public Collider[] triggersToEnable;

    public bool changesCurrentDestination;
    public Vector2 cordsOfDestination;

    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;

    // Use this for initialization
    //void Start ()
    //   {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitbox.enabled = false;
            checkpointReachedText.reachedCheckPoint();

            string data = ((int)levelID).ToString() + "~" + checkPointID.ToString() + "~" + cordsOfDestination.x.ToString() + "~" + cordsOfDestination.y.ToString();

            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);

            if(changesCurrentDestination)
            {
                if(other.GetComponentInParent<NewStoredInfoScriptOrton>())
                {
                    other.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination = cordsOfDestination;
                }
                else
                {
                    other.GetComponentInParent<NewStoredInfoScript>().currentDestination = cordsOfDestination;
                }
            }

            for(int i = 0; i < objectsToEnable.Length; i++)
            {
                objectsToEnable[i].SetActive(true);
            }
            for (int i = 0; i < objectsToDisable.Length; i++)
            {
                objectsToDisable[i].SetActive(false);
            }

            for (int i = 0; i < triggersToEnable.Length; i++)
            {
                triggersToEnable[i].enabled = true;
            }

        }
    }
}
