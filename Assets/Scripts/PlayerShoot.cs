using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    public Transform shootingPoint;
    public GameObject projectilePrefab;


    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot()
    {
        Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
