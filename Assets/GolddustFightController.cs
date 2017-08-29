using UnityEngine;
using System.Collections;

public class GolddustFightController : MonoBehaviour {

    public GameObject golddust;
    private bool fightOver = false;
    public GameObject exit;

	// Use this for initialization
	void Start ()
    {
        exit.SetActive(false);
        golddust.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    if(StoredInfoScript.persistantInfo.currentTrack != 0 && !fightOver)
        {
            StoredInfoScript.persistantInfo.PlayBossMusic();
        }

        if(StoredInfoScript.persistantInfo.getBossPercentage() == 0 && !fightOver)
        {
            fightOver = true;
            exit.SetActive(true);
            golddust.SetActive(false);
            StoredInfoScript.persistantInfo.EndBoss();
        }
	}
}
