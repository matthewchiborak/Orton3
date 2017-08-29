using UnityEngine;
using System.Collections;

public class TrumpAttackScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Hit player
            //StoredInfoScript.persistantInfo.hitByBullet();
            other.gameObject.GetComponentInParent<ShawnMichaelsControl>().hitByTrump();
            GetComponentInParent<TrumpControlScript>().hitPlayerEffects();
            GetComponentInParent<TrumpControlScript>().nextJump = true;
        }
    }
}
