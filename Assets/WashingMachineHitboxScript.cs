using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineHitboxScript : MonoBehaviour {

    public bool wasHit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Socko"))
        {
            wasHit = true;
        }
    }
}
