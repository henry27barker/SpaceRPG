using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LockerMenu : MonoBehaviour
{
    private TMP_Text firstLetter;
    private TMP_Text secondLetter;
    private TMP_Text thirdLetter;
    private TMP_Text messageText;
    public string code;

    public GameObject lockerFirst;
    private GameObject messagePanel;
    public Locker locker;
    public PlayerMovement playerMovement;

    private int firstIndex = 0;
    private int secondIndex = 0;
    private int thirdIndex = 0;

    private string key;

    private float messageTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        firstLetter = gameObject.transform.Find("Background/Letter1").gameObject.GetComponent<TMP_Text>();
        secondLetter = gameObject.transform.Find("Background/Letter2").gameObject.GetComponent<TMP_Text>();
        thirdLetter = gameObject.transform.Find("Background/Letter3").gameObject.GetComponent<TMP_Text>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        messageText = gameObject.transform.Find("MessagePanel/MessageBackgroundPanel/MessageText").gameObject.GetComponent<TMP_Text>();
        messagePanel = gameObject.transform.Find("MessagePanel").gameObject;
        messagePanel.SetActive(false);

        key = locker.key;

        Debug.Log(key);
    }

    // Update is called once per frame
    void Update()
    {
        if (messageTimer > 0)
        {
            messageTimer -= Time.unscaledDeltaTime;
            messagePanel.SetActive(true);
        }
        else
        {
            messagePanel.SetActive(false);
        }
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.playerControls.SwitchCurrentActionMap("UI");
        EventSystem.current.SetSelectedGameObject(lockerFirst);
        playerMovement.codeUI = gameObject;
    }

    void OnDisable()
    {
        playerMovement.playerControls.SwitchCurrentActionMap("Player");
        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.codeUI = null;
    }

    public void FirstUp()
    {
        if(firstIndex == 5)
        {
            firstIndex = 0;
        }
        else
        {
            firstIndex++;
        }
        firstLetter.text = key[firstIndex].ToString();
    }
    public void SecondUp()
    {
        if (secondIndex == 5)
        {
            secondIndex = 0;
        }
        else
        {
            secondIndex++;
        }
        secondLetter.text = key[secondIndex].ToString();
    }
    public void ThirdUp()
    {
        if (thirdIndex == 5)
        {
            thirdIndex = 0;
        }
        else
        {
            thirdIndex++;
        }
        thirdLetter.text = key[thirdIndex].ToString();
    }

    public void FirstDown()
    {
        if (firstIndex == 0)
        {
            firstIndex = 5;
        }
        else
        {
            firstIndex--;
        }
        firstLetter.text = key[firstIndex].ToString();
    }
    public void SecondDown()
    {
        if (secondIndex == 0)
        {
            secondIndex = 5;
        }
        else
        {
            secondIndex--;
        }
        secondLetter.text = key[secondIndex].ToString();
    }
    public void ThirdDown()
    {
        if (thirdIndex == 0)
        {
            thirdIndex = 5;
        }
        else
        {
            thirdIndex--;
        }
        thirdLetter.text = key[thirdIndex].ToString();
    }

    public void Enter()
    {
        Debug.Log(firstLetter.text + secondLetter.text + thirdLetter.text);
        Debug.Log(code);
        if (firstLetter.text + secondLetter.text + thirdLetter.text == code)
        {
            locker.Open();
            gameObject.transform.parent.gameObject.SetActive(false);
            playerMovement.codeUI = null;
        }
        else
        {
            messageText.text = "Incorrect Code";
            messageTimer = 3f;
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

}