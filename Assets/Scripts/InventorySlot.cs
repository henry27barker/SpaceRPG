using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public Item item;

    //public Button removeButton;

    private TMP_Text slotText;

    private InteractMenu interactMenu;

    void Awake(){
        slotText = gameObject.transform.Find("InventorySlotText").gameObject.GetComponent<TMP_Text>();
        slotText.text = "";
        interactMenu = GameObject.FindObjectOfType<InteractMenu>().GetComponent<InteractMenu>();
    }

    void Update(){
        if(item != null){
            if(item.name == "Ammo"){
                Ammo ammoItem = (Ammo)item;
                slotText.text = ammoItem.ammoAmount.ToString();
            }
            else{
                slotText.text = "";
            }
        }
        else{
            slotText.text = "";
        }
    }

    public void AddItem(Item newItem){
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        //removeButton.interactable = true;
    }

    public void ClearSlot(){
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        //removeButton.interactable = false;
    }

    public void OnRemoveButton(){
        Inventory.instance.Remove(item);
    }

    public void UseItem(){
        //if(item != null){
            interactMenu.gameObject.transform.parent.gameObject.SetActive(true);
            interactMenu.SetInventorySlot(gameObject.GetComponent<InventorySlot>());
            interactMenu.MoveMenu(gameObject.transform.position.x, gameObject.transform.position.y);
        //}
    }

    public void Use(){
        if(item != null){
            if(item.name != "UpgradeToken"){
                if(item.name != "Ammo"){
                    item.Use();
                    Inventory.instance.Remove(item);
                }
            }  
        }
    }
}
