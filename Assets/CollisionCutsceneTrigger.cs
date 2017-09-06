using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCutsceneTrigger : MonoBehaviour {

    public int cutsceneToTrigger;

    public bool isForOrton;
    public NewStoredInfoScript shawn;
    public NewStoredInfoScriptOrton orton;

    public Collider hitbox;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitbox.enabled = false;

            if (!isForOrton)
            {
                shawn.PlayCutscene(cutsceneToTrigger);
            }
            else
            {
                orton.PlayCutscene(cutsceneToTrigger);
            }
        }
    }
}
