using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class LaserController : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform origin;
    public int duration;

    private float counter = 0;
    private bool right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > duration) 
        {
            float temp = 0; 
            float temp2 = transform.position.x;
            if(right)
                temp2 *= -1;
            for(int i = 0; i < 5; i++)
            {
                Instantiate(laserPrefab, new Vector3(temp2, transform.position.y - temp, transform.position.z), transform.rotation);
                temp++;
            }
            if (right)
                right = false;
            else 
                right = true;
            counter = 0;
        } else
        {
            counter += Time.deltaTime;
        }
    }
}
