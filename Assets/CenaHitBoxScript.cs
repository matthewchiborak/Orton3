using UnityEngine;
using System.Collections;

public class CenaHitBoxScript : MonoBehaviour {

    public CenaFightControllerScript myBossScript;

    // Use this for initialization
    //void Start() {

    //}

    //// Update is called once per frame
    //void Update() {

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            myBossScript.NextCena();
            StoredInfoScript.persistantInfo.hitBoss(17);
        }    
    }
}
