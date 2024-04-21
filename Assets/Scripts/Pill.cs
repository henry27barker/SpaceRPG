using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Pill")]
public class Pill : Item
{
    public GameObject player;

    public int healthAmount = 70;
    public int time = 10;

    public override void Use(){
        base.Use();
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>().StartPill(healthAmount, time);
    }
}