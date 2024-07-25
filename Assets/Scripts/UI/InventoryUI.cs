using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI inventoryInstance;

    public PlayerMovement playerMovement;

    public Inventory inventory;

    public Transform itemsParent;

    public InventorySlot[] slots;

    public GameObject inventoryUI;

    public TMP_Text moneyText;

    void Awake(){
        if (inventoryInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        inventoryInstance = this;
        DontDestroyOnLoad(gameObject);

        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        moneyText = GameObject.FindWithTag("MoneyText").GetComponent<TMP_Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        for(int i = inventory.space; i < slots.Length; i++){
            slots[i].gameObject.SetActive(false);
        }
        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerMovement.money.ToString());
        moneyText.text = "$" + playerMovement.money.ToString();
    }

    public void UpdateUI(){
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
