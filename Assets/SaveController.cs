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
    Hallway,
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
    MicrowaveHallway,
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

    public GameObject[] objectsToEnable;
    public Vector2[] leftCheckPointRightObjectToEnable;
    public GameObject[] objectsToDisable;
    public Vector2[] leftCheckPointRightObjectToDisable;

    private List<string> parsedLine;
    private List<string> parsedRankLine;

    //public GameObject storedShawn;

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
        parsedLine.AddRange(assetText.Split("~"[0]));

        //Rank
        string assetTextRank;
        parsedRankLine = new List<string>();

        using (var streamReader = new StreamReader("Assets/Resources/Rank.txt", Encoding.UTF8))
        {
            assetTextRank = streamReader.ReadToEnd();
        }

        //Parse
        parsedRankLine.AddRange(assetTextRank.Split("~"[0]));

        //Enable Object
        for (int i = 0; i < leftCheckPointRightObjectToEnable.Length; i++)
        {
            if (int.Parse(parsedLine[1]) == leftCheckPointRightObjectToEnable[i].x)
            {
                objectsToEnable[(int)leftCheckPointRightObjectToEnable[i].y].SetActive(true);
            }
        }

        //Set everything to the correct position
        if (int.Parse(parsedLine[0]) == (int)LevelID.Mountain)
        {
            //Mountain is a unique case
            if(int.Parse(parsedLine[1]) < 3)
            {
                orton.transform.position = checkPointPositions[int.Parse(parsedLine[1])].position;

                if (parsedLine.Count > 2)
                {
                    orton.GetComponentInParent<NewStoredInfoScriptOrton>().setInfoOnLoad(float.Parse(parsedLine[2]), int.Parse(parsedLine[3]), int.Parse(parsedLine[4]), int.Parse(parsedLine[5]), int.Parse(parsedLine[6]), int.Parse(parsedRankLine[0]), int.Parse(parsedRankLine[1]), int.Parse(parsedRankLine[2]));
                }

                if (parsedLine.Count > 7)
                {
                    orton.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.x = int.Parse(parsedLine[7]);
                    orton.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.y = int.Parse(parsedLine[8]);
                }
            }
            else
            {
                //storedShawn.SetActive(true);
                shawn.transform.position = checkPointPositions[int.Parse(parsedLine[1])].position;

                if (parsedLine.Count > 2)
                {
                    shawn.GetComponentInParent<NewStoredInfoScript>().setInfoOnLoad(float.Parse(parsedLine[2]), int.Parse(parsedLine[3]), int.Parse(parsedLine[4]), int.Parse(parsedLine[5]), int.Parse(parsedLine[6]), 8, int.Parse(parsedRankLine[0]), int.Parse(parsedRankLine[1]), int.Parse(parsedRankLine[2]));
                }

                if (parsedLine.Count > 8)
                {
                    shawn.GetComponentInParent<NewStoredInfoScript>().currentDestination.x = int.Parse(parsedLine[7]);
                    shawn.GetComponentInParent<NewStoredInfoScript>().currentDestination.y = int.Parse(parsedLine[8]);
                }
            }
        }
        else if(int.Parse(parsedLine[0]) < (int)LevelID.Mountain)
        {
            //Orton Level
            orton.transform.position = checkPointPositions[int.Parse(parsedLine[1])].position;

            if(parsedLine.Count > 2)
            {
                orton.GetComponentInParent<NewStoredInfoScriptOrton>().setInfoOnLoad(float.Parse(parsedLine[2]), int.Parse(parsedLine[3]), int.Parse(parsedLine[4]), int.Parse(parsedLine[5]), int.Parse(parsedLine[6]), int.Parse(parsedRankLine[0]), int.Parse(parsedRankLine[1]), int.Parse(parsedRankLine[2]));
            }

            if (parsedLine.Count > 7)
            {
                orton.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.x = int.Parse(parsedLine[7]);
                orton.GetComponentInParent<NewStoredInfoScriptOrton>().currentDestination.y = int.Parse(parsedLine[8]);
            }
        }
        else if (int.Parse(parsedLine[0]) > (int)LevelID.Mountain)
        {
            //Shawn level
            shawn.transform.position = checkPointPositions[int.Parse(parsedLine[1])].position;

            if (parsedLine.Count > 2)
            {
                shawn.GetComponentInParent<NewStoredInfoScript>().setInfoOnLoad(float.Parse(parsedLine[2]), int.Parse(parsedLine[3]), int.Parse(parsedLine[4]), int.Parse(parsedLine[5]), int.Parse(parsedLine[6]), int.Parse(parsedLine[7]), int.Parse(parsedRankLine[0]), int.Parse(parsedRankLine[1]), int.Parse(parsedRankLine[2]));
            }

            if (parsedLine.Count > 8)
            {
                shawn.GetComponentInParent<NewStoredInfoScript>().currentDestination.x = int.Parse(parsedLine[8]);
                shawn.GetComponentInParent<NewStoredInfoScript>().currentDestination.y = int.Parse(parsedLine[9]);
            }
        }

        

        //Disable Objects
        for (int i = 0; i < leftCheckPointRightObjectToDisable.Length; i++)
        {
            if (int.Parse(parsedLine[1]) == leftCheckPointRightObjectToDisable[i].x)
            {
                objectsToDisable[(int)leftCheckPointRightObjectToDisable[i].y].SetActive(false);
            }
        }

        //Enable Triggers
        for (int i = 0; i < leftCheckPointRightTriggerToEnable.Length; i++)
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
