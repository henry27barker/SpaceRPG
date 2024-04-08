using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public float radius = 5f;

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

        //ENABLE A PROMPT SPRITE
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}