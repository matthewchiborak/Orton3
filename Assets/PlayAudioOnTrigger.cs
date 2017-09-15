using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour {

    public BoxCollider hitbox;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitbox.enabled = false;
            audioSource.Play();
        }
    }
}
