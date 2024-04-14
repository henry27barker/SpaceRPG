using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public PlayerMovement playerMovement;

    Inventory inventory;

    public Transform itemsParent;

    InventorySlot[] slots;

    public GameObject inventoryUI;

    public PlayerInput playerControls;

    public TMP_Text moneyText;

    void Awake(){
        playerControls = playerMovement.playerControls;
        moneyText = GameObject.FindWithTag("MoneyText").GetComponent<TMP_Text>();
        inventoryUI.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerMovement.money.ToString());
        moneyText.text = "$" + playerMovement.money.ToString();
    }

    void UpdateUI(){
        for(int i = 0; i < slots.Length; i++){
            if(i < inventory.items.Count){
                slots[i].AddItem(inventory.items[i]);
            }
            else{
                slots[i].ClearSlot();
            }
        }
    }
}
