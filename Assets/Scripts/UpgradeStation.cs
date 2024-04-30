using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : Interactable
{
    public GameObject skillTreeUI;

    void Awake(){
        skillTreeUI = GameObject.FindWithTag("SkillTree");
    }

    public override void Interact()
    {
        base.Interact();
        
        skillTreeUI.SetActive(true);
    }
}
