using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleport;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentTeleport != null)
            {
                audioManager.PlaySFX(audioManager.portalIn);
                transform.position = currentTeleport.GetComponent<Teleporter>().GetDestination().position;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleport = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == currentTeleport)
            {
                currentTeleport = null;
            }
            
        }
    }
}
