using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePromptInstance : MonoBehaviour
{
    public static InteractablePromptInstance interactablePromptInstance;
    public Sprite xbox;
    public Sprite keyboard;
    public SpriteRenderer spriteRenderer;

    void Awake(){
        if (interactablePromptInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        interactablePromptInstance = this;
        DontDestroyOnLoad(gameObject);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeControlScheme(string controlScheme)
    {
        if (controlScheme == "Gamepad")
        {
            spriteRenderer.sprite = xbox;
        } else
        {
            spriteRenderer.sprite = keyboard;
        }
    }
}
