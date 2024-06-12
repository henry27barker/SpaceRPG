using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public GameObject shopUI;

    void Awake(){
        shopUI = gameObject.transform.Find("ShopCanvas").gameObject;
    }

    void Start(){
        shopUI.SetActive(false);
    }

    public override void Interact()
    {
        base.Interact();
        
        shopUI.SetActive(true);

        shopUI.GetComponent<ShopUI>().ActivateShop();

        //Implement CrateUI
    }
}
