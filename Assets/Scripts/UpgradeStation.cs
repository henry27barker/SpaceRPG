using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : Interactable
{
    public GameObject skillTreeUI;
    private SkillTree skillTree;

    void Awake(){
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        skillTree = GameObject.FindWithTag("Player").GetComponent<SkillTree>();
    }

    public override void Interact()
    {
        base.Interact();
        
        skillTreeUI.SetActive(true);

        skillTree.UpdateUpgradeTokens();
    }
}
