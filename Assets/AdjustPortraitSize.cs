using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdjustPortraitSize : MonoBehaviour {

    //public RectTransform portrait;
    public bool onRight;

	// Update is called once per frame
	void Update ()
    {
        // GetComponent<RectTransform>().rect.width = Screen.width / 3;
        //portrait.wi
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width * 0.5f);

        if(onRight)
        {
            //GetComponent<RectTransform>().set
            
            GetComponent<RectTransform>().localPosition = new Vector3((Screen.width * 0.5f), GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().localPosition.z);
        }
    }
}
