using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Medkit")]
public class Medkit : Item
{
    public GameObject player;

    public int healthAmount = 50;

    public override void Use(){
        base.Use();
        player = GameObject.FindWithTag("Player");
        healthAmount = player.GetComponent<SkillTree>().medkitAmount;
        player.GetComponent<PlayerMovement>().IncreaseHealth(healthAmount);
    }
}
