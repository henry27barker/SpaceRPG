using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMusicManager : MonoBehaviour
{
    public AudioSource bossTheme;
    public AudioSource victoryTheme;
    public GameObject bossReference;

    private bool death;

    // Start is called before the first frame update
    void Start()
    {
        bossReference = GameObject.Find("TITAN Bot");
    }

    // Update is called once per frame
    void Update()
    {
        if(bossReference != null && !death)
        {
            if(bossReference.GetComponent<TITANBotController>().isDead())
            {
                death = true;
                victoryTheme.Play();

            }
        }
        if (death)
        {
            if (bossTheme.volume > 0)
            {
                bossTheme.volume -= Time.deltaTime * 1;
                return;
            }
            bossTheme.Stop();
        }
    }
}
