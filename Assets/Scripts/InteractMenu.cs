using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractMenu : MonoBehaviour
{
    public static InteractMenu interactMenuInstance;

    public GameObject useButton;
    private GameObject previousSelection;
    private InventorySlot inventorySlot;

    void Awake(){
        if (interactMenuInstance != null)
        {
            Destroy(gameObject.transform.parent.gameObject);
            return;
        }

        interactMenuInstance = this;
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInventorySlot(InventorySlot i){
        inventorySlot = i;
    }

    public void Use(){
        inventorySlot.Use();
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Discard(){
        inventorySlot.OnRemoveButton();
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    void OnEnable(){
        previousSelection = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(useButton);
    }

    void OnDisable(){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(previousSelection);
    }

    public void MoveMenu(float x, float y){
        gameObject.transform.position = new Vector3(x + 120f, y, 0f);
    }
}
