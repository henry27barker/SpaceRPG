using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdrHeadController : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public Transform shootingPoint;
    public SpriteRenderer spriteRenderer;

    //Head Sprites
    public Sprite angle_0;
    public Sprite angle_45;
    public Sprite angle_90;
    public Sprite angle_135;
    public Sprite angle_180;
    public Sprite angle_225;
    public Sprite angle_270;
    public Sprite angle_315;
    //Shooting Points
    public Vector3 offSets_0;
    public Vector3 offSets_45;
    public Vector3 offSets_90;
    public Vector3 offSets_135;
    public Vector3 offSets_180;
    public Vector3 offSets_225;
    public Vector3 offSets_270;
    public Vector3 offSets_315;


    private float counter;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log(lookRotation);
        shootingPoint.rotation = Quaternion.Euler(0, 0, lookRotation);

        UpdateSprite(lookRotation);

        Shoot();
    }

    private void Shoot()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
            counter = fireRate;
        }
        
    }

    private void UpdateSprite(int degrees)
    {
        if(degrees < 22 || degrees > 337)
        {
            spriteRenderer.sprite = angle_0;
            shootingPoint.position = transform.position + offSets_0;
        }
        else if(degrees < 67)
        {
            spriteRenderer.sprite = angle_45;
            shootingPoint.position = transform.position + offSets_45;
        }
        else if (degrees < 112)
        {
            spriteRenderer.sprite = angle_90;
            shootingPoint.position = transform.position + offSets_90;
        }
        else if (degrees < 157)
        {
            spriteRenderer.sprite = angle_135;
            shootingPoint.position = transform.position + offSets_135;
        }
        else if (degrees < 202)
        {
            spriteRenderer.sprite = angle_180;
            shootingPoint.position = transform.position + offSets_180;
        }
        else if (degrees < 247)
        {
            spriteRenderer.sprite = angle_225;
            shootingPoint.position = transform.position + offSets_225;
        }
        else if (degrees < 292)
        {
            spriteRenderer.sprite = angle_270;
            shootingPoint.position = transform.position + offSets_270;
        }
        else if (degrees < 337)
        {
            spriteRenderer.sprite = angle_315;
            shootingPoint.position = transform.position + offSets_315;
        }
    }
}
