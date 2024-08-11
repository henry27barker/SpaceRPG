using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPickupUI : MonoBehaviour
{
    private GameObject child;
    private PlayerMovement playerMovement;
    private int tempMoney;
    public float messageTimer;
    private float messageTimerCounter = 0; 
    private TMP_Text moneyText;
    private int currentMoneyDisplayed = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        tempMoney = playerMovement.money;
        child = gameObject.transform.Find("MoneyText").gameObject;
        moneyText = child.GetComponent<TMP_Text>();
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tempMoney != playerMovement.money){
            child.SetActive(true);
            moneyText.color = Color.white;
            moneyText.text = "+$" + (playerMovement.money - tempMoney + currentMoneyDisplayed).ToString();
            currentMoneyDisplayed += playerMovement.money - tempMoney;
            messageTimerCounter = messageTimer;
        }
        if(messageTimerCounter > 0){
            messageTimerCounter -= Time.deltaTime;
            moneyText.color = new Color(255, 255, 255, moneyText.color.a - (Time.deltaTime * (1 / messageTimer)));
        }
        else{
            currentMoneyDisplayed = 0;
            child.SetActive(false);
        }
        tempMoney = playerMovement.money;
    }
}
