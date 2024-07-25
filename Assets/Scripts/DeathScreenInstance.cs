using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenInstance : MonoBehaviour
{
    public static DeathScreenInstance deathScreenInstance;

    void Awake(){
        if (deathScreenInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        deathScreenInstance = this;
        DontDestroyOnLoad(gameObject);
    }
}
