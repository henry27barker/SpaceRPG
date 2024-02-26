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
        if (Mouse.current.leftButton.wasPressedThisFrame || Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
