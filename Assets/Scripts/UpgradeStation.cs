using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeStation : Interactable
{
    public GameObject skillTreeUI;
    private SkillTree skillTree;
    private PlayerMovement playerMovement;

    void Awake(){
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        skillTree = GameObject.FindWithTag("Player").GetComponent<SkillTree>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void Interact()
    {
        base.Interact();
        
        skillTreeUI.SetActive(true);

        skillTree.UpdateUpgradeTokens();

        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.playerControls.SwitchCurrentActionMap("UI");
        EventSystem.current.SetSelectedGameObject(playerMovement.skillTreeFirst);
    }
}
