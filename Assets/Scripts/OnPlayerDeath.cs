using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeath : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    private bool playerIsDeath = false;
    private Vector3 initPosition = new Vector3(-15.7f, 25f, 48.7f);

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(playerIsDeath){
            
            StartCoroutine("tele");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            playerIsDeath = true;
        }
    }

    IEnumerator tele()
    {
        yield return new WaitForSeconds(0.1f);
        playerController.transform.position = initPosition;
        yield return new WaitForSeconds(0.1f);
        playerIsDeath = false;
    }
}
