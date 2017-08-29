using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingZoneScript : MonoBehaviour {

    public string sceneToLoad;
    public Vector3 playerLocation;
    public Vector3 playerRotation;
    private Transform playerTransform;

    public int specialInstruction;

    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    //Locked Door Info
    public int requiredProgressLevel;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(specialInstruction == 1)
            {
                if(StoredInfoScript.persistantInfo.getProgressLevel() == 1)
                {
                    StoredInfoScript.persistantInfo.IncreaseProgress();
                }
            }

            if (specialInstruction == 2)
            {
                if (StoredInfoScript.persistantInfo.getProgressLevel() == 20)
                {
                    StoredInfoScript.persistantInfo.IncreaseProgress();
                }
            }

            if (StoredInfoScript.persistantInfo.getProgressLevel() >= requiredProgressLevel)
            {
                StoredInfoScript.persistantInfo.blockScreen();

                playerTransform = other.gameObject.GetComponentInParent<ShawnMichaelsControl>().getTransform();
                //other.gameObject.GetComponentInParent<Transform>().position = playerLocation;
                playerTransform.position = playerLocation;
                //other.gameObject.GetComponentInParent<Transform>().rotation = Quaternion.Euler(playerRotation);
                playerTransform.rotation = Quaternion.Euler(playerRotation);
                //SceneManager.LoadScene("fight", LoadSceneMode.Single);

                StoredInfoScript.persistantInfo.lastPosition = StoredInfoScript.persistantInfo.resetPosition;

                StoredInfoScript.persistantInfo.currentScene = sceneToLoad;
                StoredInfoScript.persistantInfo.lastLoadLocation = playerLocation;
                StoredInfoScript.persistantInfo.lastLoadRotation = playerRotation;

                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
            else
            {
                audioSource.time = 0.1f;
                audioSource.Play();
            }
        }
    }
}
