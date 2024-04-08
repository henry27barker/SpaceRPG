using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool isFocus = false;
    Transform player;

    public GameObject playerObject;

    bool hasInteracted = false;

    public float radius = 5f;
    public GameObject promptIcon;
    private GameObject currentIcon;
    public PlayerMovement playerMovement;

    private bool promptActive = false;

    void Start(){
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        radius = playerMovement.pickupRadius;
    }

    public virtual void Interact(){
        Debug.Log("interacting with " + transform.name);
    }

    void Update(){
        if(isFocus && !hasInteracted){
            //float distance = Vector3.Distance(player.position, transform.position);
            //if(distance <= radius){
                Interact();
                hasInteracted = true;
            //}
        }
        else if(hasInteracted || Vector2.Distance(new Vector2(playerObject.transform.position.x, playerObject.transform.position.y), new Vector2(transform.position.x, transform.position.y)) > radius){
            DeactivatePrompt();
        }
    }

    public void OnFocused(Transform playerTransform){
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused(){
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    public void Prompt()
    {
        if (!hasInteracted)
        {
            //Debug.Log("Press X to Interact");
        }
        if(promptActive == false){
            Debug.Log("SPRITE ENABLED FOR " + transform.name);
            currentIcon = Instantiate(promptIcon, transform);
            promptActive = true;
        }
        //ENABLE A PROMPT SPRITE
    }

    public void DeactivatePrompt(){
        if(promptActive == true){
            Destroy(currentIcon);
            promptActive = false;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}