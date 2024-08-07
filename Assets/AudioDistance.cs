using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDistance : MonoBehaviour
{
    private GameObject player;
    public AudioSource audioSource;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist > distance)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1 - (dist / distance);
        }
    }
}
