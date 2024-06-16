using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Ammo")]
public class Ammo : Item
{
    public int ammoAmount;

    public override void Use(){
        //base.Use();
    }
}
