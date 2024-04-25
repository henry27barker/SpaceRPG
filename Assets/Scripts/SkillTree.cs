using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.maxHealth = maxHealth;
    }
}
