using UnityEngine;
using System.Collections;

public class SteveFlyScript : MonoBehaviour {

    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public float speed;
    public Rigidbody rb;
    public float lifetime;

    void Start()
    {
        lifetime = 0.7f;
        speed = 240;

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
        //rb.velocity = SteveCar.transform.forward * speed;
        //transform.rotation = Quaternion.Euler(90, SteveCar.transform.rotation.eulerAngles.y, 0);//new Vector3(90, SteveCar.transform.rotation.y, 0);

        Destroy(gameObject, lifetime);
    }
}
