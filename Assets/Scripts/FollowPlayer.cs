using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    public Camera cam;

    private float distanceToPlayer = 25.0f;
    public float totalMouse;// = 180.0f;

    private void Update()
    {
        //Add the newest mouse movement to the total
        totalMouse += Input.GetAxis("Mouse X");
    }

    private void LateUpdate()
    {
        //Have the camera orbit the player based on the current position of the mouse
        Vector3 direction = new Vector3(0, 0, -distanceToPlayer);
        Quaternion rotation = Quaternion.Euler(-20, totalMouse * 2, 0); //The x was 10 before
        transform.position = player.transform.position + rotation * direction;
        transform.position = new Vector3(transform.position.x, transform.position.y + 26.09f, transform.position.z);

        //Rotate the player
        //player.transform.rotation = rotation;

        //Make sure the camera is looking at the player
        //transform.LookAt(player.transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y - 12.825f + 26.09f, player.transform.position.z));
    }
}
