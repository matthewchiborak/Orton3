using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CutscenePlayer : MonoBehaviour {

    public bool isForOrton;
    public NewStoredInfoScript shawn;
    public NewStoredInfoScriptOrton orton;

    private bool playing = false;

    public AudioSource soundEffect;
    public AudioSource musicTrack;

    public Image cutsceneBackGround;
    public Image portraitRight;
    public Image portraitLeft;
    public Image dialogBox;
    public Image nameLeft;
    public Image nameRight;
    public Text nameLeftText;
    public Text nameRightText;
    public Text dialog;
    public Text dialogRight;
    public Text continueText;

    public Material[] portraits;
    public Material[] backgrounds;

    public TextAsset[] instructions;

    public AudioClip[] music;
    public AudioClip[] soundEffects;

    public Material[] movies;
    public AudioClip[] movieAudio;
    //public MovieTexture[] movies;

    private string instructionsAsString;
    private List<string> eachLine;
    private int currentLine = 0;

    private List<string> parsedLine;

    private bool movieStarted = false;
    //private bool movieFinished = false;
    //private float movieLength = 0;

    // Update is called once per frame
    void Update ()
    {
        if (playing)
        {
            //if (movieLength > 0)
            //{
            //    //Movie is playing
            //    movieLength -= Time.deltaTime;
            //    return;
            //}
            //else if (movieStarted)
            //{
            //    movieLength = 0;
            //    movieFinished = true;
            //    movieStarted = false;
            //}

            //If movie playing and it ended prompt the player to advance
            if(movieStarted)
            {
                if(!soundEffect.isPlaying)
                {
                    continueText.color = new Color(1, 1, 1);
                    continueText.enabled = true;
                }
            }

            //Pressing enter should advance to the next instruction and put it to the screen
            //if (Input.GetKeyDown(KeyCode.Return) || currentLine == -1 || movieFinished)
            if (Input.GetKeyDown(KeyCode.Return) || currentLine == -1)
            {
                //movieFinished = false;
                currentLine++;
                soundEffect.Stop();

                if(movieStarted)
                {
                    ((MovieTexture)cutsceneBackGround.material.mainTexture).Stop();
                    movieStarted = false;
                }

                if (currentLine >= eachLine.Count)
                {
                    //End the cutscene
                    playing = false;
                    musicTrack.Stop();
                    cutsceneBackGround.enabled = false;
                    portraitRight.enabled = false;
                    portraitLeft.enabled = false;
                    dialogBox.enabled = false;
                    nameLeft.enabled = false;
                    nameRight.enabled = false;
                    nameLeftText.enabled = false;
                    nameRightText.enabled = false;
                    dialog.enabled = false;
                    dialogRight.enabled = false;
                    continueText.enabled = false;
                    continueText.color = new Color(0, 0, 0);
                    Time.timeScale = 1.0f;

                    if(isForOrton)
                    {
                        orton.CutsceneJustEnded();
                    }
                    else
                    {
                        shawn.CutsceneJustEnded();
                    }

                    return;
                }

                parsedLine = new List<string>();
                parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));

                //Check if change music or play sound effect. Music will always proceed sound effect
                if(int.Parse(parsedLine[0]) < 0 && int.Parse(parsedLine[0]) > -100)
                {
                    musicTrack.clip = music[-1 * int.Parse(parsedLine[0]) - 1];
                    musicTrack.Play();

                    currentLine++;
                    parsedLine = new List<string>();
                    parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));
                }

                //Sound effect
                if (int.Parse(parsedLine[0]) <= -100)
                {
                    soundEffect.clip = soundEffects[(-1 * int.Parse(parsedLine[0])) - 100];
                    soundEffect.Play();

                    currentLine++;
                    parsedLine = new List<string>();
                    parsedLine.AddRange(eachLine[currentLine].Split("~"[0]));
                }

                //Movie
                if (int.Parse(parsedLine[0]) >= 1000)
                {
                    //movieLength = float.Parse(parsedLine[1]);
                    dialogBox.enabled = false;
                    continueText.enabled = false;
                    continueText.color = new Color(0, 0, 0);
                    nameRight.enabled = false;
                    nameRightText.enabled = false;
                    nameLeft.enabled = false;
                    nameLeftText.enabled = false;
                    portraitLeft.material = portraits[0];
                    portraitRight.material = portraits[0];
                    cutsceneBackGround.material = movies[int.Parse(parsedLine[0]) - 1000];
                    //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
                    //(MovieTexture)(cutsceneBackGround.material.mainTexture).play();
                    ((MovieTexture)cutsceneBackGround.material.mainTexture).Play();

                    soundEffect.clip = movieAudio[int.Parse(parsedLine[0]) - 1000];
                    soundEffect.Play();
                    //nameLeftText.text = parsedLine[4];
                    //nameLeft.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialog.text = "";
                    dialogRight.text = "";
                    movieStarted = true;

                    return;
                }

                //FORMAT: BG. LEFTPORTRAIT. RIGHTPORTRAIT. WHATSIDENAME. NAME. COLOR. DIALOG LINE //MAYBE MUSIC BUT ILL ADD THAT LATER
                cutsceneBackGround.material = backgrounds[int.Parse(parsedLine[0])];
                portraitLeft.material = portraits[int.Parse(parsedLine[1])];
                portraitRight.material = portraits[int.Parse(parsedLine[2])];
                
                //See what Side name to put, what color, and what name;
                if(parsedLine[3] == "L" || parsedLine[3] == "LW")
                {
                    dialogBox.enabled = true;
                    continueText.enabled = true;
                    continueText.color = new Color(0, 0, 0);
                    nameRight.enabled = false;
                    nameRightText.enabled = false;
                    nameLeft.enabled = true;
                    nameLeftText.enabled = true;
                    nameLeftText.text = parsedLine[4];
                    nameLeft.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialog.text = parsedLine[8];
                    dialogRight.text = "";

                    if(parsedLine[3] == "LW")
                    {
                        nameLeftText.color = Color.white;
                    }
                    else
                    {
                        nameLeftText.color = Color.black;
                    }
                }
                else if(parsedLine[3] == "R" || parsedLine[3] == "RW")
                {
                    dialogBox.enabled = true;
                    continueText.enabled = true;
                    continueText.color = new Color(0, 0, 0);
                    nameRight.enabled = true;
                    nameRightText.enabled = true;
                    nameLeft.enabled = false;
                    nameLeftText.enabled = false;
                    nameRightText.text = parsedLine[4];
                    nameRight.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialogRight.text = parsedLine[8];
                    dialog.text = "";

                    if (parsedLine[3] == "RW")
                    {
                        nameRightText.color = Color.white;
                    }
                    else
                    {
                        nameRightText.color = Color.black;
                    }
                }
                else
                {
                    //No dialog stuffs
                    dialogBox.enabled = false;
                    continueText.enabled = false;
                    continueText.color = new Color(0, 0, 0);
                    nameRight.enabled = false;
                    nameRightText.enabled = false;
                    nameLeft.enabled = false;
                    nameLeftText.enabled = false;
                    //nameLeftText.text = parsedLine[4];
                    //nameLeft.color = new Color(float.Parse(parsedLine[5]), float.Parse(parsedLine[6]), float.Parse(parsedLine[7]));
                    dialog.text = "";
                    dialogRight.text = "";
                }

               // dialog.text = parsedLine[8];
            }
        }
	}

    public void PlayCutscene(int sceneNo)
    {
        currentLine = -1;

        Time.timeScale = 0;

        //Enable the cutscene objects
        cutsceneBackGround.enabled = true;
        portraitRight.enabled = true;
        portraitLeft.enabled = true;
        dialogBox.enabled = true;
        continueText.enabled = true;
        continueText.color = new Color(0, 0, 0);
        nameLeft.enabled = false;
        nameRight.enabled = false;
        nameLeftText.enabled = false;
        nameRightText.enabled = false;
        dialog.enabled = true;
        dialogRight.enabled = true;

        //Read the text file line by line and read its instructions
        instructionsAsString = instructions[sceneNo].text;

        eachLine = new List<string>();
        eachLine.AddRange(instructionsAsString.Split("\n"[0]));

        // you're done.

        //Debug.Log(eachLine[4]);
        //Debug.Log(eachLine[10]);
        //Debug.Log(eachLine[101]);
        //Debug.Log(eachLine[0]);
        //int kWords = eachLine.Count;
        //Debug.Log(eachLine[kWords - 1]);


        //Also Play the selected msuic

        playing = true;
    }
}
