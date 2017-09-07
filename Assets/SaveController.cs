using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public enum LevelID
{
    Desert,
    Temple,
    Town,
    Mountain,
    Castle,
    Forest,
    River,
    Battle,
    Outside,
    Lobby,
    Arena,
    Sky
}
public enum SceneNames
{
    Level1,
    Temple,
    level2,
    Level3,
    Castle,
    Level4,
    Sorrow,
    OrtonBattle,
    Level5,
    Level5Interior,
    Level5Stadium,
    FinalBoss
}

public class SaveController : MonoBehaviour {

    public GameObject orton;
    public GameObject shawn;
    public Transform[] checkPointPositions;

    public Collider[] triggersThatCanBeEnabledOnLoad;
    public Vector2[] leftCheckPointRightTriggerToEnable;

    private List<string> parsedLine;

    // Use this for initialization
    void Start ()
    {
        string assetText;
        parsedLine = new List<string>();

        using (var streamReader = new StreamReader("Assets/Resources/Save.txt", Encoding.UTF8))
        {
            assetText = streamReader.ReadToEnd();
        }

        //Parse
        //progressLevel = Int32.Parse(assetText);
        parsedLine.AddRange(assetText.Split("~"[0]));
        
        //Set everything to the correct position
        if (int.Parse(parsedLine[0]) == (int)LevelID.Mountain)
        {
            //Mountain is a unique case
        }
        else if(int.Parse(parsedLine[0]) < (int)LevelID.Mountain)
        {
            //Orton Level
            orton.transform.position = checkPointPositions[int.Parse(parsedLine[1])].position;
            orton.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.x = int.Parse(parsedLine[2]);
            orton.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.y = int.Parse(parsedLine[3]);
        }
        else if (int.Parse(parsedLine[0]) > (int)LevelID.Mountain)
        {
            //Shawn level
            shawn.transform.position = checkPointPositions[int.Parse(parsedLine[1])].position;
            shawn.GetComponentInParent<NewStoredInfoScript>().currentDestination.x = int.Parse(parsedLine[2]);
            shawn.GetComponentInParent<NewStoredInfoScript>().currentDestination.y = int.Parse(parsedLine[3]);
        }

        //Enable Triggers
        for(int i = 0; i < leftCheckPointRightTriggerToEnable.Length; i++)
        {
            if(int.Parse(parsedLine[1]) == leftCheckPointRightTriggerToEnable[i].x)
            {
                triggersThatCanBeEnabledOnLoad[(int)leftCheckPointRightTriggerToEnable[i].y].enabled = true;
            }
        }

        //Find the correct level
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Desert)
        //{
        //    for()
        //    {

        //    }
        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Temple)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Town)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Mountain)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Castle)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Forest)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.River)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Battle)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Outside)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Lobby)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Arena)
        //{

        //}
        //if ((LevelID)int.Parse(parsedLine[0]) == LevelID.Sky)
        //{

        //}

        //FORMAT
        //Scene ID
        //CheckPointId
    }

    //public void saveTheGame(int level, int checkPoint)
    //{
    //    string data = level.ToString() + "~" + checkPoint.ToString();

    //    System.IO.File.WriteAllText("Assets/Resources/Save.txt", data);
    //}

    //   // Update is called once per frame
    //   void Update () {

    //}
}
