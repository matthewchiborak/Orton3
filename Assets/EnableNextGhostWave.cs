using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNextGhostWave : MonoBehaviour
{

    public InfiniteMover[] guardsToSetASpeed;
    public BoxCollider hitbox;
    public float speedToSet;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitbox.enabled = false;

            for(int i = 0; i < guardsToSetASpeed.Length; i++)
            {
                guardsToSetASpeed[i].speed = speedToSet;
            }
        }
    }
}
