using UnityEngine;
using System.Collections;

public class BackgroundManagementScript : MonoBehaviour {

    public AudioSource BGAudioSource;
    public AudioClip[] BGTracks;

    // Update is called once per frame
    //void Update ()
    //   {

    //}

    public void switchTracks()
    {
        string theScene = StoredInfoScript.persistantInfo.getCurrentScene();

        if (theScene == "backstage")
        {
            BGAudioSource.clip = BGTracks[0];
        }
        else if (theScene == "bar")
        {
            BGAudioSource.clip = BGTracks[1];
        }
        else if (theScene == "cubicles")
        {
            BGAudioSource.clip = BGTracks[2];
        }
    }
}
