using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        float angleRadians = 0;

        float x, y;

        x = player.transform.position.x - transform.position.x;

        y = player.transform.position.y - transform.position.y;

        // Calculate the angle in radians
        angleRadians = Mathf.Atan2(y, x);
        // Convert radians to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        // Convert negative angles to positive equivalent
        if (angleDegrees < 0)
        {
            angleDegrees += 360f;
        }
        int lookRotation = Mathf.RoundToInt(angleDegrees);

        transform.rotation = Quaternion.Euler(0, 0, lookRotation);
    }
}
