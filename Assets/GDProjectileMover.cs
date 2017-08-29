using UnityEngine;
using System.Collections;

public class GDProjectileMover : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public float lifetime;
    

    // Use this for initialization
    void Start()
    {
        lifetime = 30;
        speed = 20;

        rb = GetComponent<Rigidbody>();
        rb.velocity =  transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<ShawnMichaelsControl>().hitByDust();
            Destroy(gameObject);
        }
    }
}
