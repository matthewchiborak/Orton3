using UnityEngine;
using System.Collections;

public class CollideWithPickup : MonoBehaviour {

    //Audio
    public AudioSource itemSource;
    public AudioClip pickupSFX;

    public bool isForOrton;
    public NewStoredInfoScript storedInfoShawn;
    public NewStoredInfoScriptOrton storedInfoOrton;

    void OnTriggerEnter(Collider other)
    {
        if(isForOrton)
        {
            if (other.gameObject.CompareTag("BandagePickup"))
            {
                storedInfoOrton.pickupItem(0);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("PillsPickup"))
            {
                storedInfoOrton.pickupItem(1);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("BeerPickup"))
            {
                storedInfoOrton.pickupItem(4);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("MinePickup"))
            {
                storedInfoOrton.pickupItem(5);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
        }
        else
        {
            if (other.gameObject.CompareTag("BandagePickup"))
            {
                storedInfoShawn.pickupItem(0);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("PillsPickup"))
            {
                storedInfoShawn.pickupItem(1);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("BeerPickup"))
            {
                storedInfoShawn.pickupItem(4);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("MinePickup"))
            {
                storedInfoShawn.pickupItem(5);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
            if (other.gameObject.CompareTag("CannonballPickup"))
            {
                storedInfoShawn.pickupItem(7);
                Destroy(other.gameObject);
                itemSource.clip = pickupSFX;
                itemSource.Play();
            }
        }
    }
}
