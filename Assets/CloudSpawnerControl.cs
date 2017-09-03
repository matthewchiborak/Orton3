using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerControl : MonoBehaviour {

    public GameObject cloud;
    public Vector2 xBounds;
    public Vector2 zBounds;
    public Vector2 scaleRange;
    public float spawnHeight;
    public float timeBetweenClouds;

    private float timeOfLastCloud;
    private GameObject temp;
	
	// Update is called once per frame
	void Update ()
    {
	    if(Time.time - timeOfLastCloud > timeBetweenClouds)
        {
            timeOfLastCloud = Time.time;

            temp = Instantiate(cloud, new Vector3(Random.Range(xBounds.x, xBounds.y), spawnHeight, Random.Range(zBounds.x, zBounds.y)), Quaternion.identity);
            temp.transform.localScale = new Vector3(Random.Range(scaleRange.x, scaleRange.y), Random.Range(scaleRange.x, scaleRange.y), Random.Range(scaleRange.x, scaleRange.y));
        }	
	}
}
