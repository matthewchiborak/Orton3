using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineProximityScript : MonoBehaviour
{
    public GameObject explosion;

    public void setOff()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
