using UnityEngine;
using System.Collections;

public class MankindItemHitBoxScript : MonoBehaviour {

    public bool isCactus;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isCactus)
            {
                other.gameObject.GetComponentInParent<ShawnMichaelsControl>().hitByCactus();
            }
            else
            {
                StoredInfoScript.persistantInfo.hitByPillow();
            }
            
            Destroy(gameObject);
        }
    }
}
