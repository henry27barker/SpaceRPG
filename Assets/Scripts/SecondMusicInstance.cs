using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMusicInstance : MonoBehaviour
{
    public static SecondMusicInstance secondMusicInstance;
    private Inventory gameManager;
    private AudioSource music;
    private int tempLevel;

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
    }

    void Update(){
        if((gameManager.level == 6 || gameManager.level == 11) && tempLevel != gameManager.level){
            music.Play();
        }
        else if(gameManager.level == 10 || gameManager.level >= 15){
            music.Stop();
        }
        tempLevel = gameManager.level;
        if(gameManager == null){
            Destroy(gameObject);
        }
    }
}
