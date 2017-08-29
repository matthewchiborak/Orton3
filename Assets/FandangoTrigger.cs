using UnityEngine;
using System.Collections;

public class FandangoTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             if(StoredInfoScript.persistantInfo.getProgressLevel() == 7)
            {
                StoredInfoScript.persistantInfo.IncreaseProgress();
            }
        }
    }
    
}
