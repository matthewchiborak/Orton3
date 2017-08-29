using UnityEngine;
using System.Collections;

public class BigShowTriggerScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (StoredInfoScript.persistantInfo.getProgressLevel() == 26)
            {
                StoredInfoScript.persistantInfo.IncreaseProgress();
            }
        }
    }
}
