using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject player;
    public PlayerMovement playerMovement;

    public Transform roomCenter;
    public float speed = 1.0f; // The speed of interpolation

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        if (playerMovement.currentRoom == null)
        {
            roomCenter = player.transform;
        }
        else
        {
            roomCenter = playerMovement.currentRoom.transform;
        }

        Vector2 midpoint = (player.transform.position + roomCenter.position) / 2;
        transform.position = midpoint; // Start the camera at the initial midpoint
        targetPosition = transform.position; // Initialize target position
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.currentRoom == null)
        {
            roomCenter = player.transform;
        }
        else
        {
            roomCenter = playerMovement.currentRoom.transform;
        }

        // Calculate the midpoint between the player and the room center
        Vector2 midpoint = (player.transform.position + roomCenter.position) / 2;

        // Set the target position to the new midpoint
        targetPosition = new Vector3(midpoint.x, midpoint.y, transform.position.z);

        // Interpolate the camera position towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
