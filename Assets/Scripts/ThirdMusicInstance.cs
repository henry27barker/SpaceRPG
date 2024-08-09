using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMusicInstance : MonoBehaviour
{
    public static ThirdMusicInstance thirdMusicInstance;
    private Inventory gameManager;
    private AudioSource music;
    private int tempLevel;
    private float startingVolume;
    public float musicFadeSpeed;

    void Awake(){
        if (thirdMusicInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        thirdMusicInstance = this;
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        music = gameObject.GetComponent<AudioSource>();
        tempLevel = gameManager.level;
        startingVolume = music.volume;
    }

    void Update(){
        if((gameManager.level == 16 || gameManager.level == 21) && tempLevel != gameManager.level){
            music.volume = startingVolume;
            music.Play();
        }
        else if(gameManager.level == 20 || gameManager.level >= 25){
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
