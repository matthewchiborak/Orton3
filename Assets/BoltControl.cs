using UnityEngine;
using System.Collections;

public class BoltControl : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public float lifetime;
    

    // Use this for initialization
    void Start()
    {
        lifetime = 2;
        speed = 80;

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SubGuard"))
        {
            other.gameObject.GetComponentInParent<GuardControllerScript>().TriggerStun();
            Destroy(gameObject);
            //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        }
    }
}
