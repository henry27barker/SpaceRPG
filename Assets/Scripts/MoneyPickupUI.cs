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

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        tempMoney = playerMovement.money;
        child = gameObject.transform.Find("Money").gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tempMoney != playerMovement.money){
            messageTimerCounter = messageTimer;
            child.SetActive(true);
            child.transform.Find("MoneyPanel/MoneyText").gameObject.GetComponent<TMP_Text>().text = "+$" + (playerMovement.money - tempMoney).ToString();
        }
        if(messageTimerCounter > 0){
            messageTimerCounter -= Time.deltaTime;
        }
        else{
            child.SetActive(false);
        }
        tempMoney = playerMovement.money;
    }
}
