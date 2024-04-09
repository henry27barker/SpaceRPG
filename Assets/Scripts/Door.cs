using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool unlocked;
    public Sprite doorOpen;
    public Sprite doorClosed;
    SpriteRenderer spriteRenderer;
    public GameObject openCollisionBox;
    private BoxCollider2D closedCollisionBox;

    public override void Interact()
    {
        base.Interact();
        if(unlocked)
        {
            OpenDoor();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedCollisionBox = GetComponent<BoxCollider2D>();
    }



    private void OpenDoor()
    {
        spriteRenderer.sprite = doorOpen;
        closedCollisionBox.enabled = false;
        openCollisionBox.SetActive(true);
        Debug.Log("Open Door");
    }
}
