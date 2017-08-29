using UnityEngine;
using System.Collections;

public class CenaFightControllerScript : MonoBehaviour {

    public GameObject[] Cenas;
    public int currentCena = 0;
    public GameObject fakeCenas;
    public AudioSource damageSound;

	// Use this for initialization
	void Start ()
    {
        StoredInfoScript.persistantInfo.PlayBossMusic();
        Cenas[0].SetActive(true);
        fakeCenas.SetActive(true);
        StoredInfoScript.persistantInfo.StartBoss("JOHN & CENA");
	}

    public void NextCena()
    {
        Cenas[currentCena].SetActive(false);

        //Play a sound?
        damageSound.Play();

        if(currentCena != 5)
        {
            currentCena++;
            Cenas[currentCena].SetActive(true);
        }
        else
        {
            StoredInfoScript.persistantInfo.EndBoss();
            fakeCenas.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
