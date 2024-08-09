using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelMusic : MonoBehaviour
{
    public static FirstLevelMusic firstLevelMusicInstance;
    private Inventory gameManager;
    private AudioSource music;
    private int tempLevel;
    private float startingVolume;
    public float musicFadeSpeed;

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
        startingVolume = music.volume;
    }

    void Update(){
        if(gameManager.level == 1 && tempLevel != gameManager.level){
            music.volume = startingVolume;
            music.Play();
        }
        else if(gameManager.level >= 5){
            if(music.volume > 0){
                music.volume -= Time.deltaTime * musicFadeSpeed;
                tempLevel = gameManager.level;
                return;
            }
            music.Stop();
        }
        tempLevel = gameManager.level;
        if(gameManager == null){
            Destroy(gameObject);
        }
    }
}
