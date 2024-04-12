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

    public float explosiveRate;
    public float freezeRate;
    public float electricRate;
    public float gravityRate;


    public GameObject inventoryUI;


    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot()
    {
        if(inventoryUI.activeSelf == true){
            return;
        }

        float rng = Random.Range(0f, 100f);

        if(rng >= critChance)
        {
            Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        }
        else
        {
            Instantiate(explosiveProjectile, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
