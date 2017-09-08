using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventRotate : MonoBehaviour {
    
	// Update is called once per frame
	void Update ()
    {
        Vector3 v = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(90, v.y, v.z);
    }
}
