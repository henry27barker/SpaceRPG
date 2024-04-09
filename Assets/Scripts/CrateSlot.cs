using UnityEngine;
using UnityEngine.UI;

public class CrateSlot : MonoBehaviour
{
    public Image icon;

    Item item;

    public Button removeButton;

    public CrateUI crateUI;

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
        crateUI.Remove(item);
    }

    public void UseItem(){
        if(item != null){
            crateUI.TakeItem(item);
        }
    }
}
