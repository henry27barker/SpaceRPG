using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{
    public GameObject inventoryUI;

    public override void Interact()
    {
        base.Interact();
        
        inventoryUI.SetActive(true);

        //Implement CrateUI
    }
}
