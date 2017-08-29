using UnityEngine;
using System.Collections;

public class EmptyRoomIfFarEnough : MonoBehaviour {

    public GameObject[] objectsToVanish;
    public int progressToBeGreaterThan;

	void Awake()
    {
        if(StoredInfoScript.persistantInfo.getProgressLevel() > progressToBeGreaterThan)
        {
            for(int i = 0; i < objectsToVanish.Length; i++)
            {
                objectsToVanish[i].SetActive(false);
            }
        }
    }
}
