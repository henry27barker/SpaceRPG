using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    BoxCollider2D trigger;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag != "Player")
            return;
        Debug.Log("Went through door");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
