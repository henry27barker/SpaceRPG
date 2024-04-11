using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject crateUI;
    public List<Item> items = new List<Item>();
    public Transform itemsParent;
    CrateSlot[] slots;

    public int space = 10;

    void Awake(){
        crateUI.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<CrateSlot>();
        UpdateUI();
    }

    void Update(){
        if(inventoryUI.activeSelf == false){
            crateUI.SetActive(false);
        }
    }
    
    public void TakeItem(Item item){
        Inventory.instance.Add(item);
    }

    public void Remove(Item item){
        items.Remove(item);
        UpdateUI();
    }

    public void UpdateUI(){
        for(int i = 0; i < slots.Length; i++){
            if(i < items.Count){
                slots[i].AddItem(items[i]);
            }
            else{
                slots[i].ClearSlot();
            }
        }
    }
}
