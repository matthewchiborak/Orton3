using UnityEngine;
using System.Collections;

public class AdjustDialogToScreenWidth : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width - 100f);
    }
}
