using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public Item item;

    public Button removeButton;

    public void AddItem(Item newItem){
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot(){
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton(){
        Inventory.instance.Remove(item);
    }

    public void UseItem(){
        if(item.name != "UpgradeToken"){
            if(item != null){
                item.Use();
            }
            Inventory.instance.Remove(item);
        }
    }
}
