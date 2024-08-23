using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCurrentRoom : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
            playerMovement.SetCurrentRoom(gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
            playerMovement.SetCurrentRoom(null);
    }
}
