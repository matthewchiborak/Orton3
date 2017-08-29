using UnityEngine;
using System.Collections;

public class VoidRotator : MonoBehaviour {

    private Transform theTransform;
    public float rotationValue;

	// Use this for initialization
	void Start () {
        theTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        theTransform.Rotate(new Vector3(0, 0, rotationValue));
	}
}
