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


    public GameObject inventoryUI;

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
        if(fireRateCounter < fireRate){
            return;
        }
        if(ammoCount <= 0){
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
        ammoCount--;
    }
}
