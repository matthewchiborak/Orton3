using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverControllerScript : MonoBehaviour {

    public Vector3 resetPoint;
    public float zPointToPassToReset;

    public GameObject[] objects;
    
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].transform.position.z > zPointToPassToReset)
            {
                objects[i].transform.position = new Vector3(resetPoint.x, resetPoint.y, resetPoint.z + (objects[i].transform.position.z - zPointToPassToReset));
            }
        }
    }
}
