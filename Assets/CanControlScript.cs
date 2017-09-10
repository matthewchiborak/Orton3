using UnityEngine;
using System.Collections;

public class CanControlScript : MonoBehaviour {

    private float lifetime;
    private Vector3 position;

    public bool isReady;
    public bool isForOrton;
    public NewStoredInfoScript storedShawn;
    public NewStoredInfoScriptOrton storedOrton;

    // Use this for initialization
    void Start ()
    {
        lifetime = 30f;
        position = GetComponent<Transform>().position;

        //GameObject temp = GameObject.FindWithTag("Player");
        //if(temp.GetComponentInParent<NewStoredInfoScript>())
        //{
        //    isForOrton = false;
        //    storedShawn = temp.GetComponentInParent<NewStoredInfoScript>();
        //}
        //else if(temp.GetComponentInParent<NewStoredInfoScriptOrton>())
        //{
        //    isForOrton = true;
        //    storedOrton = temp.GetComponentInParent<NewStoredInfoScriptOrton>();
        //}

        Destroy(gameObject, lifetime);
    }

    ~CanControlScript()
    {
        if (isForOrton)
        {
            storedOrton.ignorePlayer = false;
            storedOrton.cancelAlert();
        }
        else
        {
            storedShawn.ignorePlayer = false;
            storedShawn.cancelAlert();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isReady)
        {
            if (isForOrton)
            {
                storedOrton.ignorePlayer = true;
                //storedOrton.lastPosition = GetComponent<Transform>().position;//position;
                storedOrton.alert(GetComponent<Transform>().position);
            }
            else
            {
                storedShawn.ignorePlayer = true;
                //storedShawn.lastPosition = GetComponent<Transform>().position;// position;
                storedShawn.alert(GetComponent<Transform>().position);
            }
        }

        //StoredInfoScript.persistantInfo.ignorePlayer = true;
        //StoredInfoScript.persistantInfo.lastPosition = position;
	}
}
