using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class CrateUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject crateUI;
    public List<Item> items = new List<Item>();
    public List<Item> possibleItems = new List<Item>();
    public int[] possibleItemsChances;
    public Transform itemsParent;
    CrateSlot[] slots;
    public Material emissiveMaterial;
    public Color green, red;

    private PlayerMovement playerMovement;

    public GameObject crateFirst;

    public int maxPossibleItems;

    public int space = 10;

    void Awake(){
        crateUI.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<CrateSlot>();
        UpdateUI();
        
    }

    void Start(){
        inventoryUI = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inventoryUI;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        emissiveMaterial = gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().material;
        Random.seed = System.DateTime.Now.Millisecond;
        int size = Random.Range(0, maxPossibleItems + 1);
        for(int i = 0; i < size; i++){
            int rand = Random.Range(1, 101);
            if(rand <= possibleItemsChances[0]){
                items.Add(possibleItems[0]);
            }
            for(int j = 1; j < possibleItems.Count; j++){
                if(rand > possibleItemsChances[j - 1] && rand <= possibleItemsChances[j])
                    items.Add(possibleItems[j]);
            }
        }
        UpdateUI();
    }

    void OnEnable(){
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.playerControls.SwitchCurrentActionMap("UI");
        EventSystem.current.SetSelectedGameObject(crateFirst);
    }

    void OnDisable(){
        //EventSystem.current.SetSelectedGameObject(null);
        //playerMovement.playerControls.SwitchCurrentActionMap("Player");
    }

    void Update(){
        if(inventoryUI.activeSelf == false){
            crateUI.SetActive(false);
        }
        if(items.Count > 0){
            emissiveMaterial.SetColor("_Color", green * 3);
        }
        else{
            emissiveMaterial.SetColor("_Color", red * 3);
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
