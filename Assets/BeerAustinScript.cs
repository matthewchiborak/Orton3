using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerAustinScript : MonoBehaviour {

    public bool wasHit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            wasHit = true;
        }
    }
}
