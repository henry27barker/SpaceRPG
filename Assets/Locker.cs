using Pathfinding.Ionic.Zip;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class Locker : Interactable
{
    public GameObject inventoryUI;
    public GameObject lockerUI;
    public GameObject codeUI;

    public GameObject firstLetter;
    public GameObject secondLetter;
    public GameObject thirdLetter;

    public PlayerMovement playerMovement;

    public Sprite open;

    public SpriteRenderer spriteRenderer;

    public string key = "ABCDEF";

    public string code;

    public bool isOpen;


    public void Start()
    {
        code = "";
        for (int i = 0; i < 3; i++)
        {
            code += key[Random.Range(0, 5)];
        }
        Debug.Log(code);
        codeUI.SetActive(false);
        inventoryUI.SetActive(false);
        lockerUI.SetActive(false);
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        gameObject.transform.Find("CodeCanvas/MainPanel").gameObject.GetComponent<LockerMenu>().code = code;
    }


    public override void Interact()
    {
        base.Interact();


        if (isOpen)
        {
            inventoryUI.SetActive(true);
            lockerUI.SetActive(true);
        }
        else
        {
            codeUI.SetActive(true);
        }

        //Implement CrateUI
    }

    public void Open()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = open;
        isOpen = true;
    }

    public string GetCode()
    {
        return code;
    }

}
