using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinScreen : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    private GameObject winComponents;
    private EnemyMovement bossScript;
    public GameObject winScreenFirst;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");
        winComponents = gameObject.transform.Find("WinComponents").gameObject;
        bossScript = GameObject.FindWithTag("Enemy").GetComponent<EnemyMovement>();
        winComponents.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(bossScript.health <= 0){
            winComponents.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            player.GetComponent<PlayerMovement>().playerControls.SwitchCurrentActionMap("UI");
            EventSystem.current.SetSelectedGameObject(winScreenFirst);
        }
    }

    public void MainMenu(){
        gameManager.GetComponent<DeathScreen>().MainMenu();
    }
}
