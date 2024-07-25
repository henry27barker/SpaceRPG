using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePromptInstance : MonoBehaviour
{
    public static InteractablePromptInstance interactablePromptInstance;

    void Awake(){
        if (interactablePromptInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        interactablePromptInstance = this;
        DontDestroyOnLoad(gameObject);
    }
}
