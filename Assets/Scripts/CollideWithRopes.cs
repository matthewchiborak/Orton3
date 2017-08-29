using UnityEngine;
using System.Collections;

public class CollideWithRopes : MonoBehaviour {

    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public Vector3 inRingPosition;
    public Vector3 outRingPosition;
    public float middleY;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<ShawnMichaelsControl>().enterOrExitRing(middleY, inRingPosition, outRingPosition);
        }
    }
}
