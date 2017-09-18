using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByReflectedShot : MonoBehaviour {

    public int numberOfReflects;
    public Transform shawnTransform;
    public FinalBossControllerScript controller;
    public AudioSource orbBounceSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallShot"))
        {
            if (other.gameObject.GetComponent<BallShotControl>().canDamageFlair)
            {
                if (numberOfReflects > 0)
                {
                    orbBounceSource.Play();
                    other.gameObject.GetComponent<BallShotControl>().speed += other.gameObject.GetComponent<BallShotControl>().speedIncrement;
                    numberOfReflects--;
                    other.gameObject.GetComponent<BallShotControl>().canDamageFlair = false;
                    other.gameObject.GetComponent<BallShotControl>().direction = new Vector3(shawnTransform.position.x - transform.position.x, shawnTransform.position.y - transform.position.y, shawnTransform.position.z - transform.position.z).normalized;
                }
                else
                {
                    Destroy(other.gameObject);
                    controller.damageFlair();
                }
            }
        }
    }
}
