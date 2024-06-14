using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public int ammoPrice;
    private TMP_Text ammoText;

    void Awake(){
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        ammoText = gameObject.transform.Find("Ammo/AmmoPanel/AmmoBackgroundPanel/AmmoTextNumber").gameObject.GetComponent<TMP_Text>();
        ammoText.text = "$" + ammoPrice.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateShop(){
        playerMovement.shopUI = gameObject;
    }
}
