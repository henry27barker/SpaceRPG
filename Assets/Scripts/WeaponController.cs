using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Vector2 offsets;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //transform.position = offsets;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateRotation(int lookRotationDegrees)
    {
        //Update Weapon rotation
        transform.rotation = Quaternion.Euler(0, 0, lookRotationDegrees);
    }
}
