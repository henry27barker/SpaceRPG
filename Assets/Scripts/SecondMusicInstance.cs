using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMusicInstance : MonoBehaviour
{
    public static SecondMusicInstance secondMusicInstance;
    private Inventory gameManager;
    private AudioSource music;
    private int tempLevel;
    private float startingVolume;
    public float musicFadeSpeed;

    void Awake(){
        if (secondMusicInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        secondMusicInstance = this;
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        music = gameObject.GetComponent<AudioSource>();
        tempLevel = gameManager.level;
        startingVolume = music.volume;
    }

    void Update(){
        if(gameManager.level == 11 && tempLevel != gameManager.level){
            music.volume = startingVolume;
            music.Play();
        }
        else if(gameManager.level >= 15){
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