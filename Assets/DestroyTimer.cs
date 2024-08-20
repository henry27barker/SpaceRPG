using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timer;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > timer)
        {
            Destroy(gameObject);
        } else
        {
            counter += Time.deltaTime;
        }
    }
}
