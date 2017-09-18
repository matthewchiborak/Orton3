using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour {

    public Vector3 pointToRotateAround;
    public float radius;
    public float startingAngle;
    public float speed;

    public bool disabled;

    private float currentAngle;

    void Start()
    {
        currentAngle = startingAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            transform.LookAt(pointToRotateAround);
            transform.position = new Vector3(pointToRotateAround.x + radius * Mathf.Sin(currentAngle * (3.1415f / 180)), pointToRotateAround.y, pointToRotateAround.z + radius * Mathf.Cos(currentAngle * (3.1415f / 180)));
            currentAngle += speed * Time.deltaTime;
        }
    }
}
