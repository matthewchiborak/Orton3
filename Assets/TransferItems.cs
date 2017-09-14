using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferItems : MonoBehaviour {

    public NewStoredInfoScript shawn;
    public GameObject shawnObject;
    public NewStoredInfoScriptOrton orton;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shawnObject.SetActive(true);
            shawn.setItems(orton.getBandageValue(), orton.getPillsValue(), orton.getCanValue(), orton.getMineValue(), 8, orton.getkills(), orton.getAlerts(), orton.getDeaths());
            shawnObject.SetActive(false);
        }
    }
}
