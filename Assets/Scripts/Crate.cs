using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{
    public GameObject inventoryUI;

    public GameObject crateUI;

    public AudioSource openSound;

    void Start(){
        inventoryUI = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inventoryUI;
    }


    public override void Interact()
    {
        base.Interact();
        
        inventoryUI.SetActive(true);

        crateUI.SetActive(true);

        openSound.Play();

        //Implement CrateUI
    }
}
