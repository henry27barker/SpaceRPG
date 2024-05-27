using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTree : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private Inventory inventory;
    private GameObject skillTreeUI;
    private TMP_Text maxHealthText;
    private TMP_Text upgradeTokensText;
    private TMP_Text speedText;
    private TMP_Text lifeStealText;
    private TMP_Text fireRateText;
    private TMP_Text damageText;
    private TMP_Text ammoCapacityText;
    private TMP_Text messageText;
    private GameObject messagePanel;

    private float messageTimer = 0f;

    public int upgradeTokens;
    public int maxHealth;
    public int minHealth;
    public float speed;
    public float minSpeed;
    public float lifeSteal;
    public float minLifeSteal;
    public float fireRate;
    public float maxFireRate;
    public float minFireRate;
    public int damage;
    public int minDamage;
    public int maxDamage;
    public int ammoCapacity;
    public int maxAmmoCapacity;
    public int minAmmoCapacity;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerShoot = GameObject.FindWithTag("Player").GetComponent<PlayerShoot>();
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        maxHealthText = skillTreeUI.transform.Find("MaxHealth/MaxHealthPanel/MaxHealthBackgroundPanel/MaxHealthTextNumber").gameObject.GetComponent<TMP_Text>();
        speedText = skillTreeUI.transform.Find("Speed/SpeedPanel/SpeedBackgroundPanel/SpeedTextNumber").gameObject.GetComponent<TMP_Text>();
        lifeStealText = skillTreeUI.transform.Find("LifeSteal/LifeStealPanel/LifeStealBackgroundPanel/LifeStealTextNumber").gameObject.GetComponent<TMP_Text>();
        fireRateText =  skillTreeUI.transform.Find("FireRate/FireRatePanel/FireRateBackgroundPanel/FireRateTextNumber").gameObject.GetComponent<TMP_Text>();
        damageText =  skillTreeUI.transform.Find("Damage/DamagePanel/DamageBackgroundPanel/DamageTextNumber").gameObject.GetComponent<TMP_Text>();
        ammoCapacityText = skillTreeUI.transform.Find("AmmoCapacity/AmmoCapacityPanel/AmmoCapacityBackgroundPanel/AmmoCapacityTextNumber").gameObject.GetComponent<TMP_Text>();
        upgradeTokensText = skillTreeUI.transform.Find("UpgradeTokensPanel/UpgradeTokensBackgroundPanel/UpgradeTokensTextNumber").gameObject.GetComponent<TMP_Text>();
        messageText = skillTreeUI.transform.Find("MessagePanel/MessageBackgroundPanel/MessageText").gameObject.GetComponent<TMP_Text>();
        messagePanel = skillTreeUI.transform.Find("MessagePanel").gameObject;
        messagePanel.SetActive(false);
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
        playerShoot.fireRate = fireRate;
        fireRateText.text = fireRate.ToString();
        playerShoot.damage = damage;
        damageText.text = damage.ToString();
        playerShoot.maxAmmo = ammoCapacity;
        ammoCapacityText.text = ammoCapacity.ToString();

        if(messageTimer > 0){
            messageTimer -= Time.unscaledDeltaTime;
            messagePanel.SetActive(true);
        }
        else{
            messagePanel.SetActive(false);
        }
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
            playerMovement.health += incrementAmount;
            upgradeTokens--;
        }
        else{
            messageText.text = "Out of upgrade tokens.";
            messageTimer = 5f;
        }
    }

    public void DecrementMaxHealth(int decrementAmount){
        if(maxHealth > minHealth){
            if(playerMovement.health >= 1 + decrementAmount){
                maxHealth -= decrementAmount;
                playerMovement.decreaseHealth(decrementAmount);
                upgradeTokens++;
            }
            else{
                messageText.text = "Health is too low to reduce further. Heal to decrease max health more.";
                messageTimer = 5f;
                //Debug.Log("Health too low to change");
            }
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementSpeed(float incrementAmount){
        if(upgradeTokens > 0){
            speed += incrementAmount;
            upgradeTokens--;
        }
        else{
            messageText.text = "Out of upgrade tokens.";
            messageTimer = 5f;
        }
    }

    public void DecrementSpeed(float decrementAmount){
        if(speed > minSpeed){
            speed -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementLifeSteal(float incrementAmount){
        if(upgradeTokens > 0){
            lifeSteal += incrementAmount;
            upgradeTokens--;
        }
        else{
            messageText.text = "Out of upgrade tokens.";
            messageTimer = 5f;
        }
    }

    public void DecrementLifeSteal(float decrementAmount){
        if(lifeSteal > minLifeSteal){
            lifeSteal -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementFireRate(float incrementAmount){
        if(fireRate < maxFireRate){
            fireRate = (float)System.Math.Round(fireRate + incrementAmount, 2);
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot increase past maximum value.";
            messageTimer = 5f;
        }
    }

    public void DecrementFireRate(float decrementAmount){
        if(upgradeTokens > 0){
            if(fireRate > minFireRate){
                fireRate = (float)System.Math.Round(fireRate - decrementAmount, 2);
                upgradeTokens--;
            }
            else{
                messageText.text = "Reached minimum fire rate.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Out of upgrade tokens.";
            messageTimer = 5f;
        }
    }

    public void IncrementDamage(int incrementAmount){
        if(upgradeTokens > 0){
            if(damage < maxDamage){
                damage += incrementAmount;
                upgradeTokens--;
            }
            else{
                messageText.text = "Cannot increase past maximum value.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Out of upgrade tokens.";
            messageTimer = 5f;
        }
    }

    public void DecrementDamage(int decrementAmount){
        if(damage > minDamage){
            damage -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementAmmoCapacity(int incrementAmount){
        if(ammoCapacity < maxAmmoCapacity){
            ammoCapacity += incrementAmount;
            playerShoot.ammoCount += incrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot increase past maximum value.";
            messageTimer = 5f;
        }
    }

    public void DecrementAmmoCapacity(int decrementAmount){
        if(upgradeTokens > 0){
            if(ammoCapacity > minAmmoCapacity){
                if(playerShoot.ammoCount >= decrementAmount){
                    ammoCapacity -= decrementAmount;
                    playerShoot.ammoCount -= decrementAmount;
                    upgradeTokens++;
                }
                else{
                    messageText.text = "Ammo is too low to reduce further. Get bullets to decrease ammo capacity more.";
                    messageTimer = 5f;
                    //Debug.Log("Health too low to change");
                }
            }
            else{
                messageText.text = "Reached minimum ammo capacity.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Out of upgrade tokens.";
            messageTimer = 5f;
        }
    }
}
