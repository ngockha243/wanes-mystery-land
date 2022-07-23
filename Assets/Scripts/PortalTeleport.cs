using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] Transform receiver;

    private bool playerIsOverlapping = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        if(playerIsOverlapping){
            
            StartCoroutine("tele");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            playerIsOverlapping = true;
        }
    }

    IEnumerator tele()
    {
        yield return new WaitForSeconds(0.1f);
        playerController.transform.position = receiver.transform.position + new Vector3(0f, 7f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerIsOverlapping = false;
    }
}
