using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPromptScheme : MonoBehaviour
{
    public Sprite xbox;
    public Sprite keyboard;

    private GameObject player;
    private PlayerMovement playerMovement;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if(player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.GetControlScheme() == "Gamepad")
        {
            image.sprite = xbox;
        } else
        {
            image.sprite = keyboard;
        }
    }
}
