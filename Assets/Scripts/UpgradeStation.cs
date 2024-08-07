using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeStation : Interactable
{
    public GameObject skillTreeUI;
    private SkillTree skillTree;
    private PlayerMovement playerMovement;

    public AudioSource openSound;

    void Start(){
        skillTreeUI = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().skillTreeUI;
        skillTree = GameObject.FindWithTag("Player").GetComponent<SkillTree>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void Interact()
    {
        base.Interact();
        
        skillTreeUI.SetActive(true);

        skillTree.UpdateUpgradeTokens();

        openSound.Play();

        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.playerControls.SwitchCurrentActionMap("UI");
        EventSystem.current.SetSelectedGameObject(playerMovement.skillTreeFirst);
    }
}
