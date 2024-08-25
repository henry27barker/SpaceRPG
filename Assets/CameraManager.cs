using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;
    public CinemachineVirtualCamera virtualCamera;

    public bool oldSystem;
    public static CameraManager cameraManagerInstance;

    void Awake()
    {
        if (cameraManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        cameraManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        if(oldSystem)
            virtualCamera.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
