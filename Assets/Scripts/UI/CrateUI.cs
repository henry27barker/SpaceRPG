using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject crateUI;
    public List<Item> items = new List<Item>();
    public List<Item> possibleItems = new List<Item>();
    public Transform itemsParent;
    CrateSlot[] slots;

    public int maxPossibleItems;

    public int space = 10;

    void Awake(){
        crateUI.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<CrateSlot>();
        UpdateUI();
    }

    void Start(){
        Random.seed = System.DateTime.Now.Millisecond;
        int size = Random.Range(0, maxPossibleItems);
        for(int i = 0; i <= size; i++){
            int index = Random.Range(0, possibleItems.Count);
            items.Add(possibleItems[index]);
        }
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
