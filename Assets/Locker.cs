using System.Collections;
using System.Collections.Generic;
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

    public string code;

    public bool isOpen;


    public void Start()
    {
        code = "";
        for (int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, 5);
            switch(num)
            {
                case 0:
                    code += 'A';
                    break;
                case 1:
                    code += 'B';
                    break;
                case 2:
                    code += 'D';
                    break;
                case 3:
                    code += 'P';
                    break;
                case 4:
                    code += 'E';
                    break;
                default:
                    code += 'C';
                    break;
            }
        }
        codeUI.SetActive(false);
        inventoryUI.SetActive(false);
        lockerUI.SetActive(false);
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log(code);
    }


    public override void Interact()
    {
        base.Interact();


        if (isOpen)
        {
            inventoryUI.SetActive(true);
            lockerUI.SetActive(true);
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = open;
        }
        else
        {
            codeUI.SetActive(true);
            playerMovement.lockerUI = gameObject;
        }

        //Implement CrateUI
    }

}
