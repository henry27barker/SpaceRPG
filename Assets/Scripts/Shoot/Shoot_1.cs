using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot_1 : MonoBehaviour
{
    public GameObject projectile;
    public float rate;
    public int damage;
    public Transform groupLeader;

    private float counter;
    public LayerMask layerMask; 

    void Start()
    {
        int layer4 = 8;
        int layer5 = 3;

        layerMask = (1 << layer4) | (1 << layer5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(Mathf.Cos(groupLeader.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(groupLeader.eulerAngles.z * Mathf.Deg2Rad));
        RaycastHit2D hit = Physics2D.CircleCast(groupLeader.position, 0.25f, direction, 50, layerMask);
        Debug.DrawLine(groupLeader.position, (Vector2)groupLeader.position + direction * 10, Color.red);
        if (counter > rate)
        {
            if (hit.collider != null)
            {
                // Check if the hit object is tagged as "Player"
                if (hit.collider.gameObject.tag == "Player")
                {
                    var copy = Instantiate(projectile, transform.position, transform.rotation);
                    if(copy.GetComponent<SpdrProjectile>() != null)
                        copy.GetComponent<SpdrProjectile>().damage = damage;
                }
            }
            counter = 0;
        } else
        {
            counter += Time.deltaTime;
        }
    }
}
