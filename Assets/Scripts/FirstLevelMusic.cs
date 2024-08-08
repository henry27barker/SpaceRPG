using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelMusic : MonoBehaviour
{
    public static FirstLevelMusic firstLevelMusicInstance;
    private Inventory gameManager;
    private AudioSource music;
    private int tempLevel;

    void Awake(){
        if (firstLevelMusicInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        firstLevelMusicInstance = this;
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        music = gameObject.GetComponent<AudioSource>();
        tempLevel = gameManager.level;
    }

    void Update(){
        if(gameManager.level == 1 && tempLevel != gameManager.level){
            music.Play();
        }
        else if(gameManager.level >= 5){
            music.Stop();
        }
        tempLevel = gameManager.level;
        if(gameManager == null){
            Destroy(gameObject);
        }
    }
}
