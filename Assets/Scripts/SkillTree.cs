using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
    private TMP_Text medkitAmountText;
    private TMP_Text inventorySizeText;
    private TMP_Text syringeAmountText;
    private TMP_Text pillAmountText;
    private TMP_Text stompDistanceText;
    private TMP_Text stompDamageText;
    private TMP_Text messageText;
    private GameObject messagePanel;

    private GameObject basicTab;
    private GameObject weaponTab;
    private GameObject healthTab;
    private GameObject critTab;
    private GameObject itemsTab;

    private InventoryUI inventoryUI;

    public List<Item> items = new List<Item>();
    private UpgradeInventorySlot[] slots;

    private float messageTimer = 0f;

    public int upgradeTokens;
    private int totalUpgradeTokens = 0;
    private int space = 25;
    public int maxHealth;
    public int minHealth;
    public int maxMaxHealth;
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float lifeSteal;
    public float minLifeSteal;
    public float maxLifeSteal;
    public float fireRate;
    public float maxFireRate;
    public float minFireRate;
    public int damage;
    public int minDamage;
    public int maxDamage;
    public int ammoCapacity;
    public int maxAmmoCapacity;
    public int minAmmoCapacity;
    public int medkitAmount;
    public int minMedkitAmount;
    public int maxMedkitAmount;
    public int inventorySize;
    public int minInventorySize;
    public int maxInventorySize;
    public int syringeAmount;
    public int minSyringeAmount;
    public int maxSyringeAmount;
    public int pillAmount;
    public int minPillAmount;
    public int maxPillAmount;
    public float stompDistance;
    public float minStompDistance;
    public float maxStompDistance;
    public int stompDamage;
    public int minStompDamage;
    public int maxStompDamage;

    void Awake(){
        inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        slots = skillTreeUI.transform.Find("UpgradeInventory/UpgradeInventoryUI/ItemsParent").gameObject.GetComponentsInChildren<UpgradeInventorySlot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerShoot = GameObject.FindWithTag("Player").GetComponent<PlayerShoot>();
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        basicTab = skillTreeUI.transform.Find("Basic/BasicUpgrades").gameObject;
        weaponTab = skillTreeUI.transform.Find("Weapon/WeaponUpgrades").gameObject;
        healthTab = skillTreeUI.transform.Find("Health/HealthUpgrades").gameObject;
        critTab = skillTreeUI.transform.Find("Crit/CritUpgrades").gameObject;
        itemsTab = skillTreeUI.transform.Find("UpgradeInventory/UpgradeInventoryUI").gameObject;
        maxHealthText = skillTreeUI.transform.Find("Health/HealthUpgrades/MaxHealth/MaxHealthPanel/MaxHealthBackgroundPanel/MaxHealthTextNumber").gameObject.GetComponent<TMP_Text>();
        speedText = skillTreeUI.transform.Find("Basic/BasicUpgrades/Speed/SpeedPanel/SpeedBackgroundPanel/SpeedTextNumber").gameObject.GetComponent<TMP_Text>();
        inventorySizeText = skillTreeUI.transform.Find("Basic/BasicUpgrades/InventorySize/InventorySizePanel/InventorySizeBackgroundPanel/InventorySizeTextNumber").gameObject.GetComponent<TMP_Text>();
        lifeStealText = skillTreeUI.transform.Find("Health/HealthUpgrades/LifeSteal/LifeStealPanel/LifeStealBackgroundPanel/LifeStealTextNumber").gameObject.GetComponent<TMP_Text>();
        fireRateText =  skillTreeUI.transform.Find("Weapon/WeaponUpgrades/FireRate/FireRatePanel/FireRateBackgroundPanel/FireRateTextNumber").gameObject.GetComponent<TMP_Text>();
        damageText =  skillTreeUI.transform.Find("Weapon/WeaponUpgrades/Damage/DamagePanel/DamageBackgroundPanel/DamageTextNumber").gameObject.GetComponent<TMP_Text>();
        ammoCapacityText = skillTreeUI.transform.Find("Weapon/WeaponUpgrades/AmmoCapacity/AmmoCapacityPanel/AmmoCapacityBackgroundPanel/AmmoCapacityTextNumber").gameObject.GetComponent<TMP_Text>();
        medkitAmountText = skillTreeUI.transform.Find("Health/HealthUpgrades/MedkitAmount/MedkitAmountPanel/MedkitAmountBackgroundPanel/MedkitAmountTextNumber").gameObject.GetComponent<TMP_Text>();
        syringeAmountText = skillTreeUI.transform.Find("Health/HealthUpgrades/SyringeAmount/SyringeAmountPanel/SyringeAmountBackgroundPanel/SyringeAmountTextNumber").gameObject.GetComponent<TMP_Text>();
        pillAmountText = skillTreeUI.transform.Find("Health/HealthUpgrades/PillAmount/PillAmountPanel/PillAmountBackgroundPanel/PillAmountTextNumber").gameObject.GetComponent<TMP_Text>();
        stompDistanceText = skillTreeUI.transform.Find("Basic/BasicUpgrades/StompDistance/StompDistancePanel/StompDistanceBackgroundPanel/StompDistanceTextNumber").gameObject.GetComponent<TMP_Text>();
        stompDamageText = skillTreeUI.transform.Find("Basic/BasicUpgrades/StompDamage/StompDamagePanel/StompDamageBackgroundPanel/StompDamageTextNumber").gameObject.GetComponent<TMP_Text>();
        upgradeTokensText = skillTreeUI.transform.Find("UpgradeTokensPanel/UpgradeTokensBackgroundPanel/UpgradeTokensTextNumber").gameObject.GetComponent<TMP_Text>();
        messageText = skillTreeUI.transform.Find("MessagePanel/MessageBackgroundPanel/MessageText").gameObject.GetComponent<TMP_Text>();
        messagePanel = skillTreeUI.transform.Find("MessagePanel").gameObject;
        weaponTab.SetActive(false);
        healthTab.SetActive(false);
        critTab.SetActive(false);
        itemsTab.SetActive(false);
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
        medkitAmountText.text = medkitAmount.ToString();
        inventory.space = inventorySize;
        inventorySizeText.text = inventorySize.ToString();
        syringeAmountText.text = syringeAmount.ToString();
        pillAmountText.text = pillAmount.ToString();
        playerMovement.stompRadius = stompDistance;
        stompDistanceText.text = stompDistance.ToString();
        playerMovement.stompDamage = stompDamage;
        stompDamageText.text = stompDamage.ToString();

        if(messageTimer > 0){
            messageTimer -= Time.unscaledDeltaTime;
            messagePanel.SetActive(true);
        }
        else{
            messagePanel.SetActive(false);
        }
    }

    public void EnableTab(string tab){
        if(tab == "Basic"){
            weaponTab.SetActive(false);
            healthTab.SetActive(false);
            critTab.SetActive(false);
            itemsTab.SetActive(false);
            basicTab.SetActive(true);
        }
        if(tab == "Weapon"){
            basicTab.SetActive(false);
            healthTab.SetActive(false);
            critTab.SetActive(false);
            itemsTab.SetActive(false);
            weaponTab.SetActive(true);
        }
        if(tab == "Health"){
            weaponTab.SetActive(false);
            basicTab.SetActive(false);
            critTab.SetActive(false);
            itemsTab.SetActive(false);
            healthTab.SetActive(true);
        }
        if(tab == "Crit"){
            weaponTab.SetActive(false);
            healthTab.SetActive(false);
            basicTab.SetActive(false);
            itemsTab.SetActive(false);
            critTab.SetActive(true);
        }
        if(tab == "Items"){
            weaponTab.SetActive(false);
            healthTab.SetActive(false);
            basicTab.SetActive(false);
            critTab.SetActive(false);
            itemsTab.SetActive(true);
        }
    }

    public void UpdateUpgradeTokens(){
        foreach(Item item in inventory.items){
            if(item.name == "UpgradeToken"){
                inventory.Remove(item);
                upgradeTokens++;
                totalUpgradeTokens++;
            }
        }
    }

    public void ResetUpgrades(){
        inventoryUI.gameObject.SetActive(true);
        if(inventoryUI.inventory.items.Count - minInventorySize <= space - items.Count){
            int amount = inventoryUI.inventory.items.Count;
            for(int i = minInventorySize; i < amount; i++){
                items.Add(inventoryUI.slots[minInventorySize].item);
                Debug.Log(items.Count);
                slots[items.Count - 1].AddItem(inventoryUI.slots[minInventorySize].item);
                inventoryUI.slots[minInventorySize].OnRemoveButton();
                UpdateUI();
                inventoryUI.UpdateUI();
            }
            for(int i = minInventorySize; i < inventorySize; i++){
                inventoryUI.slots[i].gameObject.SetActive(false);
            }
        }
        else{
            messageText.text = "Upgrade station inventory too full to reset. Empty it, or your own inventory to proceed.";
            messageTimer = 5f;
            inventoryUI.gameObject.SetActive(false);
            return;
        }
        inventoryUI.inventory.space = minInventorySize;
        inventoryUI.gameObject.SetActive(false);
        inventorySize = minInventorySize;
        maxHealth = minHealth;
        playerMovement.health = minHealth;
        speed = minSpeed;
        lifeSteal = minLifeSteal;
        fireRate = maxFireRate;
        damage = minDamage;
        ammoCapacity = minAmmoCapacity;
        medkitAmount = minMedkitAmount;
        syringeAmount = minSyringeAmount;
        pillAmount = minPillAmount;
        stompDistance = minStompDistance;
        stompDamage = minStompDamage;
        upgradeTokens = totalUpgradeTokens;
    }

    public void UpdateUI(){
        for(int i = 0; i < slots.Length; i++){
            if(i < items.Count){
                slots[i].AddItem(items[i]);
            }
            else{
                slots[i].ClearSlot();
            }
        }
    }

    public void TakeItem(Item item){
        Inventory.instance.Add(item);
    }

    public void Remove(Item item){
        items.Remove(item);
        UpdateUI();
    }


    public void IncrementMaxHealth(int incrementAmount){
        if(upgradeTokens > 0){
            if(maxHealth < maxMaxHealth){
                maxHealth += incrementAmount;
                playerMovement.health += incrementAmount;
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
            if(speed < maxSpeed){
                speed += incrementAmount;
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
            if(lifeSteal < maxLifeSteal){
                lifeSteal += incrementAmount;
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

    public void IncrementMedkitAmount(int incrementAmount){
        if(upgradeTokens > 0){
            if(medkitAmount < maxMedkitAmount){
                medkitAmount += incrementAmount;
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

    public void DecrementMedkitAmount(int decrementAmount){
        if(medkitAmount > minMedkitAmount){
            medkitAmount -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    } 

    public void IncrementInventorySize(int incrementAmount){
        if(upgradeTokens > 0){
            if(inventorySize < maxInventorySize){
                inventorySize += incrementAmount;
                inventoryUI.gameObject.SetActive(true);
                inventoryUI.slots[inventorySize - 1].gameObject.SetActive(true);
                inventoryUI.inventory.space++;
                inventoryUI.gameObject.SetActive(false);
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

    public void DecrementInventorySize(int decrementAmount){
        if(inventorySize > minInventorySize){
            if(inventory.items.Count < inventory.space){
                inventorySize -= decrementAmount;
                inventoryUI.gameObject.SetActive(true);
                inventoryUI.slots[inventorySize].gameObject.SetActive(false);
                inventoryUI.gameObject.SetActive(false);
                upgradeTokens++;
            }
            else{
                messageText.text = "Inventory full. Remove items to decrease inventory further.";
                messageTimer = 5f;
            }
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementSyringeAmount(int incrementAmount){
        if(upgradeTokens > 0){
            if(syringeAmount < maxSyringeAmount){
                syringeAmount += incrementAmount;
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

    public void DecrementSyringeAmount(int decrementAmount){
        if(syringeAmount > minSyringeAmount){
            syringeAmount -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementPillAmount(int incrementAmount){
        if(upgradeTokens > 0){
            if(pillAmount < maxPillAmount){
                pillAmount += incrementAmount;
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

    public void DecrementPillAmount(int decrementAmount){
        if(pillAmount > minPillAmount){
            pillAmount -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    } 

    public void IncrementStompDistance(float incrementAmount){
        if(upgradeTokens > 0){
            if(stompDistance < maxStompDistance){
                stompDistance += incrementAmount;
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

    public void DecrementStompDistance(float decrementAmount){
        if(stompDistance > minStompDistance){
            stompDistance -= decrementAmount;
            upgradeTokens++;
        }
        else{
            messageText.text = "Cannot decrease past minimum value.";
            messageTimer = 5f;
        }
    }

    public void IncrementStompDamage(int incrementAmount){
        if(upgradeTokens > 0){
            if(stompDamage < maxStompDamage){
                stompDamage += incrementAmount;
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
}
