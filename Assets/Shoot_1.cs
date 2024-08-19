using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Shoot_1 : MonoBehaviour
{
    public GameObject projectile;
    public float rate;

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
        Vector2 direction = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.25f, direction, 50, layerMask);
        Debug.DrawLine(transform.position, (Vector2)transform.position + direction * 10, Color.red);
        if (counter > rate)
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag);
                // Check if the hit object is tagged as "Player"
                if (hit.collider.gameObject.tag == "Player")
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                }
            }
            counter = 0;
        } else
        {
            counter += Time.deltaTime;
        }
    }
}
