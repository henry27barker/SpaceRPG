using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LockerUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject lockerUI;
    public List<Item> items = new List<Item>();
    public List<Item> possibleItems = new List<Item>();
    public Transform itemsParent;
    CrateSlot[] slots;

    private PlayerMovement playerMovement;

    public GameObject crateFirst;

    public int maxPossibleItems;

    public int space = 4;

    void Awake()
    {
        lockerUI.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<CrateSlot>();
        UpdateUI();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        int size = Random.Range(0, maxPossibleItems);
        for (int i = 0; i <= size; i++)
        {
            int index = Random.Range(0, possibleItems.Count);
            items.Add(possibleItems[index]);
        }
        UpdateUI();
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.playerControls.SwitchCurrentActionMap("UI");
        EventSystem.current.SetSelectedGameObject(crateFirst);
    }

    void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.playerControls.SwitchCurrentActionMap("Player");
    }

    void Update()
    {
        if (inventoryUI.activeSelf == false)
        {
            lockerUI.SetActive(false);
        }
    }

    public void TakeItem(Item item)
    {
        Inventory.instance.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
