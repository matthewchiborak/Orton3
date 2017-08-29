using UnityEngine;
using System.Collections;

public class CanControlScript : MonoBehaviour {

    private float lifetime;
    private Vector3 position;

    // Use this for initialization
    void Start ()
    {
        lifetime = 10f;
        position = GetComponent<Transform>().position;

        Destroy(gameObject, lifetime);
    }

    ~CanControlScript()
    {
        StoredInfoScript.persistantInfo.ignorePlayer = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        StoredInfoScript.persistantInfo.ignorePlayer = true;
        StoredInfoScript.persistantInfo.lastPosition = position;
	}
}
