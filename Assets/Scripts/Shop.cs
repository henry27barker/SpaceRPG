using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public GameObject shopUI;

    void Awake(){
        shopUI = gameObject.transform.Find("ShopCanvas").gameObject;
    }

    public override void Interact()
    {
        base.Interact();
        
        shopUI.SetActive(true);

        //Implement CrateUI
    }
}
