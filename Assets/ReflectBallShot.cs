using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBallShot : MonoBehaviour
{
    public Transform shawnTransform;
    public Transform ortonTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallShot"))
        {
            float yDirection = (new Vector3(ortonTransform.position.x - shawnTransform.position.x, ortonTransform.position.y - shawnTransform.position.y, ortonTransform.position.z - shawnTransform.position.z).normalized).y;

            other.gameObject.GetComponent<BallShotControl>().direction = new Vector3(shawnTransform.forward.x, yDirection, shawnTransform.forward.z).normalized;
        }
    }
}
