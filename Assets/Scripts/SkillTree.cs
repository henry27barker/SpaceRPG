using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTree : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Inventory inventory;
    private GameObject skillTreeUI;
    private TMP_Text maxHealthText;
    private TMP_Text upgradeTokensText;
    private TMP_Text speedText;
    private TMP_Text lifeStealText;

    public int upgradeTokens;
    public int maxHealth;
    public int minHealth;
    public float speed;
    public float minSpeed;
    public float lifeSteal;
    public float minLifeSteal;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        maxHealthText = skillTreeUI.transform.Find("MaxHealth/MaxHealthPanel/MaxHealthBackgroundPanel/MaxHealthTextNumber").gameObject.GetComponent<TMP_Text>();
        speedText = skillTreeUI.transform.Find("Speed/SpeedPanel/SpeedBackgroundPanel/SpeedTextNumber").gameObject.GetComponent<TMP_Text>();
        lifeStealText = skillTreeUI.transform.Find("LifeSteal/LifeStealPanel/LifeStealBackgroundPanel/LifeStealTextNumber").gameObject.GetComponent<TMP_Text>();
        upgradeTokensText = skillTreeUI.transform.Find("UpgradeTokensPanel/UpgradeTokensBackgroundPanel/UpgradeTokensTextNumber").gameObject.GetComponent<TMP_Text>();
        skillTreeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        upgradeTokensText.text = upgradeTokens.ToString();
        playerMovement.maxHealth = maxHealth;
        maxHealthText.text = maxHealth.ToString();
        speedText.text = speed.ToString();
        lifeStealText.text = lifeSteal.ToString();
        playerMovement.speed = speed;
        playerMovement.lifeSteal = lifeSteal;
    }

    public void UpdateUpgradeTokens(){
        foreach(Item item in inventory.items){
            if(item.name == "UpgradeToken"){
                inventory.Remove(item);
                upgradeTokens++;
            }
        }
    }

    public void IncrementMaxHealth(int incrementAmount){
        if(upgradeTokens > 0){
            maxHealth += incrementAmount;
            upgradeTokens--;
        }
    }

    public void DecrementMaxHealth(int decrementAmount){
        if(maxHealth > minHealth){
            maxHealth -= decrementAmount;
            upgradeTokens++;
        }
    }

    public void IncrementSpeed(float incrementAmount){
        if(upgradeTokens > 0){
            speed += incrementAmount;
            upgradeTokens--;
        }
    }

    public void DecrementSpeed(float decrementAmount){
        if(speed > minSpeed){
            speed -= decrementAmount;
            upgradeTokens++;
        }
    }

    public void IncrementLifeSteal(float incrementAmount){
        if(upgradeTokens > 0){
            lifeSteal += incrementAmount;
            upgradeTokens--;
        }
    }

    public void DecrementLifeSteal(float decrementAmount){
        if(lifeSteal > minLifeSteal){
            lifeSteal -= decrementAmount;
            upgradeTokens++;
        }
    }
}
