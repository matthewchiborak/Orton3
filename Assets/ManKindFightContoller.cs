using UnityEngine;
using System.Collections;

public class ManKindFightContoller : MonoBehaviour {

    public GameObject mankind;
    private bool fightOver = false;

    // Use this for initialization
    void Start()
    {
        mankind.SetActive(true);
        StoredInfoScript.persistantInfo.PlayBossMusic();
        StoredInfoScript.persistantInfo.StartBoss("MICK FOLEY");
    }

    // Update is called once per frame
    void Update()
    {
        if (StoredInfoScript.persistantInfo.currentTrack != 0 && !fightOver)
        {
            StoredInfoScript.persistantInfo.PlayBossMusic();
        }

        if (StoredInfoScript.persistantInfo.getBossPercentage() == 0 && !fightOver)
        {
            fightOver = true;
            mankind.SetActive(false);
            StoredInfoScript.persistantInfo.EndBoss();
        }
    }
}
