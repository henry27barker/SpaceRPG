using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int health;
    public SpriteRenderer spriteRenderer;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private GameObject player;
    public GameObject moneyItem;
    public int moneyAmount;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);
    }

    void Update()
    {
        if (whiteFlashCounter > 0)
        {
            spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            whiteFlashCounter -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.material.SetFloat("_FlashAmount", 0);
        }
    }

    public void decreaseHealth(int damage)
    {
        whiteFlashCounter = whiteFlashTime;
        health -= damage;
        if (health <= 0)
        {
            GameObject tempMoney = Instantiate(moneyItem, gameObject.transform.position, Quaternion.identity);
            tempMoney.GetComponent<MoneyPickup>().amount = moneyAmount;
            Destroy(gameObject);
        }
    }
}
