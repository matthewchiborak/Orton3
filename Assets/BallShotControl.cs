using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShotControl : MonoBehaviour {

    public bool launch;

    public float speed;
    public float speedIncrement;
    private Rigidbody rb;
    public Vector3 direction;

    public Transform shawnTransform;
    public Transform flairTransform;

    public bool canDamageFlair;

    public AudioSource orbBounceSource;

    // Use this for initialization
    void Start()
    {
        //When an object with this is created, have it travel in a certain direction until it hits something. 
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(launch)
        {
            rb.velocity = direction * speed;
        }

        if(transform.position.x < -400 || transform.position.x > 0 || transform.position.z < -200 || transform.position.z > 200)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            orbBounceSource.Play();

            float yDirection = (new Vector3(flairTransform.position.x - shawnTransform.position.x, flairTransform.position.y - shawnTransform.position.y, flairTransform.position.z - shawnTransform.position.z).normalized).y;
            speed += speedIncrement;
            direction = new Vector3(shawnTransform.forward.x, yDirection, shawnTransform.forward.z).normalized;
            canDamageFlair = true;
        }
    }
}
