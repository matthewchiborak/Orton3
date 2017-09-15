using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControl : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
