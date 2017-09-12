using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanResetHitbox : MonoBehaviour {

    public AudioSource splashSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            splashSource.Play();

            if(other.gameObject.GetComponentInParent<NewStoredInfoScriptOrton>())
            {
                other.gameObject.GetComponentInParent<NewStoredInfoScriptOrton>().reloadFromLastCheckpoint();
            }
            else
            {
                //other.gameObject.GetComponent<NewStoredInfoScript>().reloadFromLastCheckpoint();
            }
        }
    }
}
