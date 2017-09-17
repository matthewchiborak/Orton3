using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinBossFireLighterScript : MonoBehaviour
{
    public GameObject fireComponents;
    public AudioSource fireSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StompBolt"))
        {
            fireSource.Play();
            fireComponents.SetActive(true);
        }
    }
}
