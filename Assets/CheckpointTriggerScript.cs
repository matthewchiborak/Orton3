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
            checkpointReachedText.reachedCheckPoint();

            string data = ((int)levelID).ToString() + "~" + checkPointID.ToString();

            System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);
        }
    }
}
