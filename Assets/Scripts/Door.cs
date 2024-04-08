using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool unlocked;

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
        
    }



    private void OpenDoor()
    {
        Debug.Log("Open Door");
    }
}
