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
    public Item pill;
    public Item syringe;

    private TMP_Text moneyText;

    private TMP_Text messageText;
    private GameObject messagePanel;
    private float messageTimer = 0f;

    public int ammoPrice;
    private TMP_Text ammoText;
    public int medkitPrice;
    private TMP_Text medkitText;
    public int pillPrice;
    private TMP_Text pillText;
    public int syringePrice;
    private TMP_Text syringeText;

    void Awake(){
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        moneyText = gameObject.transform.Find("Money/MoneyPanel/MoneyText").gameObject.GetComponent<TMP_Text>();
        messageText = gameObject.transform.Find("MessagePanel/MessageBackgroundPanel/MessageText").gameObject.GetComponent<TMP_Text>();
        messagePanel = gameObject.transform.Find("MessagePanel").gameObject;
        ammoText = gameObject.transform.Find("Ammo/AmmoPanel/AmmoBackgroundPanel/AmmoTextNumber").gameObject.GetComponent<TMP_Text>();
        medkitText = gameObject.transform.Find("Medkit/MedkitPanel/MedkitBackgroundPanel/MedkitTextNumber").gameObject.GetComponent<TMP_Text>();
        pillText = gameObject.transform.Find("Pill/PillPanel/PillBackgroundPanel/PillTextNumber").gameObject.GetComponent<TMP_Text>();
        syringeText = gameObject.transform.Find("Syringe/SyringePanel/SyringeBackgroundPanel/SyringeTextNumber").gameObject.GetComponent<TMP_Text>();
        ammoText.text = "$" + ammoPrice.ToString();
        medkitText.text = "$" + medkitPrice.ToString();
        pillText.text = "$" + pillPrice.ToString();
        syringeText.text = "$" + syringePrice.ToString();
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
                messageText.text = "Not enough money.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Not enough inventory space.";
            messageTimer = 5f;
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
                messageText.text = "Not enough money.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Not enough inventory space.";
            messageTimer = 5f;
        }
    }

    public void SellPill(){
        foreach(Item item in inventory.items){
            if(item.name == "Pill"){
                inventory.Remove(item);
                playerMovement.money += pillPrice;
                return;
            }
        }
    }

    public void BuyPill(){
        if(inventory.items.Count < inventory.space){
            if(playerMovement.money >= pillPrice){
                if(inventory.Add(pill)){
                    playerMovement.money -= pillPrice;
                }
            }
            else{
                messageText.text = "Not enough money.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Not enough inventory space.";
            messageTimer = 5f;
        }
    }

    public void SellSyringe(){
        foreach(Item item in inventory.items){
            if(item.name == "Syringe"){
                inventory.Remove(item);
                playerMovement.money += syringePrice;
                return;
            }
        }
    }

    public void BuySyringe(){
        if(inventory.items.Count < inventory.space){
            if(playerMovement.money >= syringePrice){
                if(inventory.Add(syringe)){
                    playerMovement.money -= syringePrice;
                }
            }
            else{
                messageText.text = "Not enough money.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Not enough inventory space.";
            messageTimer = 5f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + playerMovement.money.ToString();

        if(messageTimer > 0){
            messageTimer -= Time.unscaledDeltaTime;
            messagePanel.SetActive(true);
        }
        else{
            messagePanel.SetActive(false);
        }
    }

    public void ActivateShop(){
        playerMovement.shopUI = gameObject;
    }
}
