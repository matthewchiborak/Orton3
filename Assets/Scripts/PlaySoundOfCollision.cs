using UnityEngine;
using System.Collections;

public class PlaySoundOfCollision : MonoBehaviour {

    private AudioSource audioSource;
    public string tagName;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagName))
        {
            audioSource.Play();
        }
    }
}
