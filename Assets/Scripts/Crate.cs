using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{
    public GameObject inventoryUI;

    public GameObject crateUI;

    public override void Interact()
    {
        base.Interact();
        
        inventoryUI.SetActive(true);

        crateUI.SetActive(true);

        //Implement CrateUI
    }
}
