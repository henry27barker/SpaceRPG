using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;
    private GameObject player;
    public int rowIncrementer = 2;
    public int startingRowIncrementer = 5;
    public int columnIncrementer = 1;
    public int startingColumnIncrementer = 5;
    public int level = 0;
    public int bossLevel = 26;
    private Slider slider;
    private GameObject loadingScreen;

    void Awake(){
        if(instance != null){
            instance.startingRowIncrementer += rowIncrementer;
            instance.startingColumnIncrementer += columnIncrementer;
            Destroy(gameObject);
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player");
    }

    #endregion

    public int space = 10;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    void Start(){
        loadingScreen = gameObject.transform.Find("LoadingScreenCanvas").gameObject;
        slider = gameObject.transform.Find("LoadingScreenCanvas/BlackPanel/LoadSlider").gameObject.GetComponent<Slider>();
        loadingScreen.SetActive(false);
    }

    public bool Add(Item item){
        if(!item.isDefaultItem){
            if(items.Count >= space){
                Debug.Log("Not enough room.");
                return false;
            }
            Item newItem = Object.Instantiate(item);
            items.Add(newItem);
            if(item.name == "Ammo"){
                Ammo tempItem = (Ammo)newItem;
                tempItem.ammoAmount = player.GetComponent<SkillTree>().ammoCapacity;
            }

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item){
        items.Remove(item);

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void LoadSceneIndex(int index){
        StartCoroutine(LoadSceneAsynchronously(index));
        loadingScreen.SetActive(false);
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone){
            slider.value = operation.progress;
            yield return null;
        }
    }
}
