using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_2 : MonoBehaviour
{
    public GameObject projectile;
    public float rate;

    private float counter;
    public LayerMask layerMask;
    public float timeDelay;
    public int damage;

    public float lineWidth;
    private LineRenderer lineRenderer;

    private Queue<PlayerPosition> playerPositions; // Queue to store the player's past positions
    private Transform playerTransform;

    void Start()
    {
        int layer4 = 8;
        int layer5 = 3;

        layerMask = (1 << layer4) | (1 << layer5);
        
        // Add and configure the LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Default material for the line
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.sortingLayerName = "LilBro";

        // Initialize the queue to store player positions
        playerPositions = new Queue<PlayerPosition>();
        // Find the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }
    void Update()
    {
        if (playerTransform != null)
        {
            // Store the player's current position with the current time
            playerPositions.Enqueue(new PlayerPosition(playerTransform.position, Time.time));

            // Remove positions that are too old
            while (playerPositions.Count > 0 && Time.time - playerPositions.Peek().Time > timeDelay)
            {
                playerPositions.Dequeue();
            }

            // Determine the direction of the CircleCast based on rotation
            Vector2 direction = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.25f, direction, 50, layerMask);

            if (counter > rate)
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    // Check if the hit object is tagged as "Player"
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        var copy = Instantiate(projectile, transform.position, transform.rotation);
                        copy.GetComponent<SpdrProjectile>().damage = damage;
                    }
                }
                counter = 0;
            }
            else
            {
                counter += Time.deltaTime;

                if (hit.collider.tag == "Player" || hit.collider.tag == "Obstacle")
                {
                    Vector3 delayedPosition = playerPositions.Peek().Position;

                    // Extend the line beyond the delayed position
                    Vector2 lineDirection = (delayedPosition - transform.position).normalized;
                    float maxDistance = 100f; // Maximum distance the line can extend
                    RaycastHit2D extendedHit = Physics2D.Raycast(delayedPosition, lineDirection, maxDistance, layerMask);

                    Vector3 lineEndPosition;
                    if (extendedHit.collider != null)
                    {
                        // Line hit an obstacle or the player
                        lineEndPosition = extendedHit.point;
                    }
                    else
                    {
                        // No hit, draw the line to the maximum distance
                        lineEndPosition = delayedPosition + (Vector3)lineDirection * maxDistance;
                    }

                    // Draw the line from the enemy to the extended position
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, lineEndPosition);
                }
                else
                {
                    // Hide the line when not shooting
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, transform.position);
                }
            }
        }
    }
}

// Custom class to store player positions with a timestamp
public class PlayerPosition
{
    public Vector3 Position { get; }
    public float Time { get; }

    public PlayerPosition(Vector3 position, float time)
    {
        Position = position;
        Time = time;
    }
}
