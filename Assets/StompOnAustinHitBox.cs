using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompOnAustinHitBox : MonoBehaviour {

    public bool wasHit;
    private bool playerIn;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StompBolt"))
        {
            if(playerIn)
                wasHit = true;
        }
    }
}
