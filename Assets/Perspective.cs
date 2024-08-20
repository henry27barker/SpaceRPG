using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour
{
    public Transform pivot;
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        pivot = transform.Find("Pivot");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < pivot.position.y)
        {
            spriteRenderer.sortingLayerName = "Foreground";
        } else
        {
            spriteRenderer.sortingLayerName = "LilBro";
            spriteRenderer.sortingOrder = 20;
        }
    }
}
