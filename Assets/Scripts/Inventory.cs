using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;
    private GameObject player;
    public int rowIncrementer = 3;
    public int startingRowIncrementer = 0;
    public int columnIncrementer = 3;
    public int startingColumnIncrementer = 0;
    public int level = 1;

    void Awake(){
        if(instance != null){
            instance.startingRowIncrementer += rowIncrementer;
            instance.startingColumnIncrementer += columnIncrementer;
            Destroy(gameObject);
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player");
    }

    #endregion

    public int space = 10;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    public bool Add(Item item){
        if(!item.isDefaultItem){
            if(items.Count >= space){
                Debug.Log("Not enough room.");
                return false;
            }
            Item newItem = Object.Instantiate(item);
            items.Add(newItem);
            if(item.name == "Ammo"){
                Ammo tempItem = (Ammo)newItem;
                tempItem.ammoAmount = player.GetComponent<SkillTree>().ammoCapacity;
            }

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item){
        items.Remove(item);

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
