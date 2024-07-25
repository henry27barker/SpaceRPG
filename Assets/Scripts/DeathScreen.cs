using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameObject player;
    public GameObject deathScreen;
    public GameObject deathScreenFirst;
    public GameObject globalVolume;

    void Awake(){
        player = GameObject.FindWithTag("Player");
        deathScreen = GameObject.FindWithTag("DeathScreen");
        globalVolume = GameObject.FindWithTag("GlobalVolume");
        deathScreenFirst = deathScreen.transform.Find("DeathScreenPanel/DeathScreenBackgroundPanel/MainMenuPanel").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        globalVolume.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovement>().health <= 0){
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(deathScreenFirst);
            globalVolume.SetActive(true);
        }
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void Restart(){
        SceneManager.LoadScene(1);
    }
}
