using UnityEngine;
using System.Collections;

public class TrumpStompScript : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public float lifetime;
    private float initTimer = 1f;

    // Use this for initialization
    void Start()
    {
        lifetime = 10;
        speed = 80;

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if(initTimer > 0)
        {
            initTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && initTimer <= 0)
        {
            //other.gameObject.GetComponent<ShawnMichaelsControl>().hitByBullet();
            StoredInfoScript.persistantInfo.getPlayerScript().hitByBullet();
           
            Destroy(gameObject);
            //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        }
    }
}
