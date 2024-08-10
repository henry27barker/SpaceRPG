using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    BoxCollider2D trigger;
    public string sceneName;
    public int restRoomInterval = 5;
    private Inventory gameManager;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        trigger = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag != "Player")
            return;
        gameManager.level++;
        Debug.Log("Went through door");
        if(gameManager.level == gameManager.bossLevel){
            //SceneManager.LoadScene(3);
            player.transform.position = new Vector3(0, -1.5f, 0);
            gameManager.LoadSceneIndex(3);
        }
        else if(gameManager.level % restRoomInterval == 0){
            //SceneManager.LoadScene(2);
            player.transform.position = new Vector3(4, -1.5f, 0);
            gameManager.LoadSceneIndex(2);
        }
        else{
            //SceneManager.LoadScene(1);
            gameManager.LoadSceneIndex(1);
        }
        // GameObject[] moneyItems = GameObject.FindGameObjectsWithTag("Money");
        // foreach(GameObject temp in moneyItems){
        //     Destroy(temp);
        // }
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
