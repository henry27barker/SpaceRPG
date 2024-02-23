using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float currentHealth;
    public float sideOffset;
    public float backOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update color based on health
        UpdateColor();
    }

    // Update sprite color based on current health
    void UpdateColor()
    {
        // Calculate interpolation factor based on current health (from 100 to 0)
        float t = 1f - currentHealth / 100f;


        // Interpolate color from green to yellow to red
        if (currentHealth >= 50)
        {
            // Interpolate from green to yellow for health values from 100 to 50
            t = 1f - (currentHealth - 50) / 50f;
            spriteRenderer.color = Color.Lerp(Color.green, Color.yellow, t);
        }
        else
        {
            // Interpolate from yellow to red for health values from 50 to 0
            t = 1f - currentHealth / 50f;
            spriteRenderer.color = Color.Lerp(Color.yellow, Color.red, t);
        }

    }
}
