using UnityEngine;
using System.Collections;

public class SkelliControlScript : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent nav;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update ()
    {
        nav.destination = (StoredInfoScript.persistantInfo.getPlayerTransform().position);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StoredInfoScript.persistantInfo.hitBySkeleton();
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
