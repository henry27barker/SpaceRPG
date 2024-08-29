using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameObject player;
    public GameObject deathScreen;
    public GameObject deathScreenFirst;
    public Volume globalVolume;
    private GameObject skillTree;
    private GameObject interactMenu;
    private GameObject interactablePrompt;
    private GameObject inventoryUI;
    public float deathSlowSpeed = 0.5f;

    void Awake(){
        player = GameObject.FindWithTag("Player");
        deathScreen = GameObject.FindWithTag("DeathScreen");
        globalVolume = GameObject.FindWithTag("GlobalVolume").GetComponent<Volume>();
        deathScreenFirst = deathScreen.transform.Find("DeathScreenPanel/DeathScreenBackgroundPanel/MainMenuPanel").gameObject;
        skillTree = GameObject.FindWithTag("SkillTree");
        interactMenu = FindObjectOfType<InteractMenu>().gameObject.transform.parent.gameObject;
        interactablePrompt = GameObject.FindWithTag("InteractablePrompt");
        inventoryUI = GameObject.FindWithTag("InventoryUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        if (globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovement>().health <= 0){
            if(Time.timeScale > 0)
                Time.timeScale -= Time.deltaTime * deathSlowSpeed;
            deathScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(deathScreenFirst);
            if (globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.saturation.value = -100;
            }
        }
    }

    public void MainMenu(){
        Destroy(player);
        Destroy(deathScreen);
        Destroy(globalVolume);
        Destroy(skillTree);
        Destroy(interactMenu);
        Destroy(interactablePrompt);
        Destroy(inventoryUI);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void Restart(){
        Destroy(player);
        Destroy(deathScreen);
        Destroy(globalVolume);
        Destroy(skillTree);
        Destroy(interactMenu);
        Destroy(interactablePrompt);
        Destroy(inventoryUI);
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }
}
