using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolumeInstance : MonoBehaviour
{
    public static GlobalVolumeInstance globalVolumeInstance;

    void Awake(){
        if (globalVolumeInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        globalVolumeInstance = this;
        DontDestroyOnLoad(gameObject);
    }
}
