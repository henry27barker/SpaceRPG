using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdrHeadController : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public Transform shootingPoint;
    public Transform raycastPoint;
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

    public float raycastOffsetMultiplier;

    private float counter, counter2;
    private bool reloading;
    public float fireRate, reloadTime;
    public int ammo;
    private int ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = ammo;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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
        shootingPoint.rotation = Quaternion.Euler(0, 0, lookRotation);
        raycastPoint.rotation = Quaternion.Euler(0, 0, lookRotation);

        UpdateSprite(lookRotation);

        if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= 12){
            Shoot(lookRotation);
        }
    }

    private void Shoot(int look)
    {
        if (ammoCount < 1)
        {
            reloading = true;
            ammoCount = ammo;
        }

        if (reloading)
        {
            if (counter2 > reloadTime)
            {
                reloading = false;
                counter2 = 0;
            }
            else
            {
                counter2 += Time.deltaTime;
            }
        }
        else
        {
            if (counter > fireRate)
            {
                //Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, look + 15));
                Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
                ammoCount--;
                //Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, look - 15));
                counter = 0;
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
        
    }

    private void UpdateSprite(int degrees)
    {
        if(degrees < 22 || degrees > 337)
        {
            spriteRenderer.sprite = angle_0;
            shootingPoint.position = transform.position + offSets_0;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(1,0,0);
        }
        else if(degrees < 67)
        {
            spriteRenderer.sprite = angle_45;
            shootingPoint.position = transform.position + offSets_45;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(1, 0, 0);
        }
        else if (degrees < 112)
        {
            spriteRenderer.sprite = angle_90;
            shootingPoint.position = transform.position + offSets_90;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(0, 1, 0);
        }
        else if (degrees < 157)
        {
            spriteRenderer.sprite = angle_135;
            shootingPoint.position = transform.position + offSets_135;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(0, 1, 0);
        }
        else if (degrees < 202)
        {
            spriteRenderer.sprite = angle_180;
            shootingPoint.position = transform.position + offSets_180;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(-1, 0, 0);
        }
        else if (degrees < 247)
        {
            spriteRenderer.sprite = angle_225;
            shootingPoint.position = transform.position + offSets_225;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(0, -1, 0);
        }
        else if (degrees < 292)
        {
            spriteRenderer.sprite = angle_270;
            shootingPoint.position = transform.position + offSets_270;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(0, -1, 0);
        }
        else if (degrees < 337)
        {
            spriteRenderer.sprite = angle_315;
            shootingPoint.position = transform.position + offSets_315;
            raycastPoint.position = gameObject.transform.parent.gameObject.transform.position + raycastOffsetMultiplier * new Vector3(1, 0, 0);
        }
    }
}
