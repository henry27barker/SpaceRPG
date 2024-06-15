using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    public Transform shootingPoint;

    //Projectile Prefabs
    public GameObject projectile;
    public GameObject explosiveProjectile;
    public GameObject freezeProjectile;
    public GameObject electricProjectile;
    public GameObject gravityProjectile;

    //Settings
    public float critChance;
    public float fireRate;
    public int maxAmmo;
    public int ammoCount;
    public int damage;

    public float explosiveRate;
    public float freezeRate;
    public float electricRate;
    public float gravityRate;

    private float fireRateCounter;

    private Inventory inventory;
    public GameObject inventoryUI;
    private GameObject skillTreeUI;

    void Awake(){
        inventoryUI = GameObject.FindWithTag("InventoryUI");
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        inventory = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
    }

    void Start(){
        fireRateCounter = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRateCounter < fireRate){
            fireRateCounter += Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if(inventoryUI.activeSelf == true){
            return;
        }
        if(skillTreeUI.activeSelf == true){
            return;
        }
        if(gameObject.GetComponent<PlayerMovement>().shopUI != null){
            return;
        }
        if(fireRateCounter < fireRate){
            return;
        }
        // if(ammoCount <= 0){
        //     return;
        // }
        bool ammoFound = false;
        foreach(Item item in inventory.items){
            if(item.name == "Ammo"){
                Ammo ammoItem = (Ammo)item;
                if(ammoItem.ammoAmount > 0){
                    ammoItem.ammoAmount--;
                    ammoFound = true;
                    if(ammoItem.ammoAmount <= 0){
                        inventory.Remove(item);
                    }
                    continue;
                }
            }
        }
        if(!ammoFound){
            return;
        }

        float rng = Random.Range(0f, 100f);

        if(rng >= critChance)
        {
            GameObject copy = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
            copy.GetComponent<Projectile>().damage = damage;
        }
        else
        {
            Instantiate(explosiveProjectile, shootingPoint.position, shootingPoint.rotation);
        }

        fireRateCounter = 0;
        // ammoCount--;
    }
}
