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

    private int firstIndex = 0;
    private int secondIndex = 0;
    private int thirdIndex = 0;

    private string key = "ABDPEC";
    // Start is called before the first frame update
    void Start()
    {
        firstLetter = gameObject.transform.Find("Background/Letter1").gameObject.GetComponent<TMP_Text>();
        secondLetter = gameObject.transform.Find("Background/Letter2").gameObject.GetComponent<TMP_Text>();
        thirdLetter = gameObject.transform.Find("Background/Letter3").gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FirstUp()
    {
        if(firstIndex == 0)
        {
            firstIndex = 5;
        }
        else
        {
            firstIndex++;
        }
        firstLetter.text = key[firstIndex].ToString();
    }
    public void SecondUp()
    {
        if (secondIndex == 0)
        {
            secondIndex = 5;
        }
        else
        {
            secondIndex++;
        }
        secondLetter.text = key[secondIndex].ToString();
    }
    public void ThirdUp()
    {
        if (thirdIndex == 0)
        {
            thirdIndex = 5;
        }
        else
        {
            thirdIndex++;
        }
        thirdLetter.text = key[thirdIndex].ToString();
    }

    public void FirstDown()
    {
        if (firstIndex == 5)
        {
            firstIndex = 0;
        }
        else
        {
            firstIndex--;
        }
        firstLetter.text = key[firstIndex].ToString();
    }
    public void SecondDown()
    {
        if (secondIndex == 5)
        {
            secondIndex = 0;
        }
        else
        {
            secondIndex--;
        }
        secondLetter.text = key[secondIndex].ToString();
    }
    public void ThirdDown()
    {
        if (thirdIndex == 5)
        {
            thirdIndex = 0;
        }
        else
        {
            thirdIndex--;
        }
        thirdLetter.text = key[thirdIndex].ToString();
    }

}