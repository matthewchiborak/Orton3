﻿using System.Collections;
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

            if (changesCurrentDestination)
            {
                if (other.GetComponentInParent<NewStoredInfoScriptOrton>())
                {
                    other.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination = cordsOfDestination;
                }
                else
                {
                    other.GetComponentInParent<NewStoredInfoScript>().currentDestination = cordsOfDestination;
                }
            }

            string data = "0~0";
            string rankData = "0~0~0";

            if (other.GetComponentInParent<NewStoredInfoScriptOrton>())
            {
                data = ((int)levelID).ToString() + "~" + checkPointID.ToString() + "~" + other.GetComponentInParent<NewStoredInfoScriptOrton>().getStateString() + "~" + other.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.x.ToString() + "~" + other.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.y.ToString();
                rankData = other.GetComponentInParent<NewStoredInfoScriptOrton>().getRankStrin();
            }
            else
            {
                data = ((int)levelID).ToString() + "~" + checkPointID.ToString() + "~" + other.GetComponentInParent<NewStoredInfoScript>().getStateString() + "~" + other.GetComponentInParent<NewStoredInfoScript>().currentDestination.x.ToString() + "~" + other.GetComponentInParent<NewStoredInfoScript>().currentDestination.y.ToString();
                rankData = other.GetComponentInParent<NewStoredInfoScript>().getRankStrin();
            }

            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);
            System.IO.File.WriteAllText("Assets/Resources/Rank.txt", rankData);
            

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
