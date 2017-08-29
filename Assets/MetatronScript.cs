using UnityEngine;
using System.Collections;

public class MetatronScript : MonoBehaviour {

    public GameObject foleyModel;
	
    void Awake()
    {
        if (StoredInfoScript.persistantInfo.getProgressLevel() > 25)
        {
            transform.localPosition = new Vector3(0, 75.6f, 0);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            foleyModel.SetActive(true);
        }
    }

	
}
