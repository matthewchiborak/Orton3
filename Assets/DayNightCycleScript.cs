using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleScript : MonoBehaviour {

    public Light sun;

    public Vector3 sunAngles;

    public Vector2 intensities;
    public Color midnightColor;
    public Color noonColor;

    public float durationOfDay;
    private float timeDayStarted;

    private float durationOfPhase;

    private int phase;

	// Use this for initialization
	void Start ()
    {
        timeDayStarted = Time.time;
        
        durationOfPhase = durationOfDay / 3;
        phase = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(phase == 0)
        {
            sun.intensity = Mathf.Lerp(intensities.x, intensities.y, (Time.time - timeDayStarted) / durationOfPhase);
            sun.color = Color.Lerp(midnightColor, noonColor, (Time.time - timeDayStarted) / durationOfPhase);
            sun.transform.rotation = Quaternion.Euler(Mathf.Lerp(sunAngles.x, sunAngles.y, (Time.time - timeDayStarted) / durationOfPhase), 0, 0);

            if((Time.time - timeDayStarted) > durationOfPhase)
            {
                timeDayStarted = Time.time;
                phase++;
            }
        }
        else if(phase == 1)
        {
            sun.intensity = Mathf.Lerp(intensities.y, intensities.x, (Time.time - timeDayStarted) / durationOfPhase);
            sun.color = Color.Lerp(noonColor, midnightColor, (Time.time - timeDayStarted) / durationOfPhase);
            sun.transform.rotation = Quaternion.Euler(Mathf.Lerp(sunAngles.y, sunAngles.z, (Time.time - timeDayStarted) / durationOfPhase), 0, 0);

            if ((Time.time - timeDayStarted) > durationOfPhase)
            {
                timeDayStarted = Time.time;
                phase++;
            }
        }
        else
        {
            if ((Time.time - timeDayStarted) > durationOfPhase)
            {
                timeDayStarted = Time.time;
                phase = 0;
            }
        }
	}
}
