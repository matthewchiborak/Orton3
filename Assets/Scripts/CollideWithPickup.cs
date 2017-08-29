using UnityEngine;
using System.Collections;

public class CollideWithPickup : MonoBehaviour {

    //Audio
    public AudioSource itemSource;
    public AudioClip pickupSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BandagePickup"))
        {
            StoredInfoScript.persistantInfo.pickupItem(0);
            Destroy(other.gameObject);
            itemSource.clip = pickupSFX;
            itemSource.Play();
        }
        if (other.gameObject.CompareTag("PillsPickup"))
        {
            StoredInfoScript.persistantInfo.pickupItem(1);
            Destroy(other.gameObject);
            itemSource.clip = pickupSFX;
            itemSource.Play();
        }
        if (other.gameObject.CompareTag("BeerPickup"))
        {
            StoredInfoScript.persistantInfo.pickupItem(4);
            Destroy(other.gameObject);
            itemSource.clip = pickupSFX;
            itemSource.Play();
        }
    }
}
