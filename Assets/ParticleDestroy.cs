using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count > 0.162)
        {
            Destroy(gameObject);
        } else
        {
            count += Time.deltaTime;
        }
    }
}
