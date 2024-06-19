using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Inventory inventory;

    public Item ammo;
    public Item medkit;

    public int ammoPrice;
    private TMP_Text ammoText;
    public int medkitPrice;
    private TMP_Text medkitText;

    void Awake(){
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        ammoText = gameObject.transform.Find("Ammo/AmmoPanel/AmmoBackgroundPanel/AmmoTextNumber").gameObject.GetComponent<TMP_Text>();
        medkitText = gameObject.transform.Find("Medkit/MedkitPanel/MedkitBackgroundPanel/MedkitTextNumber").gameObject.GetComponent<TMP_Text>();
        ammoText.text = "$" + ammoPrice.ToString();
        medkitText.text = "$" + medkitPrice.ToString();
    }

    public void SellAmmo(){
        foreach(Item item in inventory.items){
            if(item.name == "Ammo"){
                Ammo ammoItem = (Ammo)item;
                if(ammoItem.canSell == true){
                    inventory.Remove(item);
                    playerMovement.money += ammoPrice;
                    return;
                }
            }
        }
    }

    public void BuyAmmo(){
        if(inventory.items.Count < inventory.space){
            if(playerMovement.money >= ammoPrice){
                if(inventory.Add(ammo)){
                    playerMovement.money -= ammoPrice;
                }
            }
            else{
                Debug.Log("Not enough money");
            }
        }
        else{
            Debug.Log("Not enough inventory space");
        }
    }

    public void SellMedkit(){
        foreach(Item item in inventory.items){
            if(item.name == "Medkit"){
                inventory.Remove(item);
                playerMovement.money += medkitPrice;
                return;
            }
        }
    }

    public void BuyMedkit(){
        if(inventory.items.Count < inventory.space){
            if(playerMovement.money >= medkitPrice){
                if(inventory.Add(medkit)){
                    playerMovement.money -= medkitPrice;
                }
            }
            else{
                Debug.Log("Not enough money");
            }
        }
        else{
            Debug.Log("Not enough inventory space");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateShop(){
        playerMovement.shopUI = gameObject;
    }
}
