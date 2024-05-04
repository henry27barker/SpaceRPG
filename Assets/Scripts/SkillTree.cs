using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Inventory inventory;

    public int maxHealth;
    public float speed;
    public float lifeSteal;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.maxHealth = maxHealth;
        playerMovement.speed = speed;
        playerMovement.lifeSteal = lifeSteal;
    }

    public void IncrementMaxHealth(int incrementAmount){
        foreach(Item item in inventory.items){
            if(item.name == "UpgradeToken"){
                inventory.Remove(item);
                maxHealth += incrementAmount;
            }
        }
    }

    public void IncrementSpeed(float incrementAmount){
        speed += incrementAmount;
    }

    public void IncrementLifeSteal(float incrementAmount){
        lifeSteal += incrementAmount;
    }
}
