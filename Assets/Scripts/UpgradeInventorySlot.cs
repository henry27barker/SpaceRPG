using UnityEngine;
using UnityEngine.UI;

public class UpgradeInventorySlot : MonoBehaviour
{
    public Image icon;

    Item item;

    public Button removeButton;

    public SkillTree skillTree;

    void Awake(){
        skillTree = GameObject.FindWithTag("Player").GetComponent<SkillTree>();
    }

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
        skillTree.Remove(item);
    }

    public void UseItem(){
        if(item != null){
            skillTree.TakeItem(item);
        }
        skillTree.Remove(item);
    }
}
