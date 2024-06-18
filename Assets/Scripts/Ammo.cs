using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Ammo")]
public class Ammo : Item
{
    public int ammoAmount;
    public bool canSell;

    void Awake(){
        canSell = true;
    }

    public override void Use(){
        //base.Use();
    }
}
