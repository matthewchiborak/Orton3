using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGrowShrinkIntensity : MonoBehaviour {

    public Light light;
    public float duration;
    private float timeTurnOn;
    public float intensity;

	// Use this for initialization
	void Start () {
        timeTurnOn = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if((Time.time - timeTurnOn) < (duration / 2))
        {
            light.intensity = Mathf.Lerp(0, intensity, ((Time.time - timeTurnOn) / (duration / 2)));
        }
        else
        {
            //(Time.time - timeGrowthBegan - (durationOfGunGrowth / 3)) / (durationOfGunGrowth / 3)
            light.intensity = Mathf.Lerp(intensity, 0, ((Time.time - timeTurnOn - ((duration / 2))) / (duration / 2)));
        }
    }
}
