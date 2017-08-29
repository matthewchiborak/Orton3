using UnityEngine;
using System.Collections;

public class SteveDoorScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            GetComponentInParent<SteveCarControlScript>().GotToDoor();
        }
    }
}
