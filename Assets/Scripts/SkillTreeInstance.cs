using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeInstance : MonoBehaviour
{
    public static SkillTreeInstance skillTreeInstance;

    void Awake(){
        if (skillTreeInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        skillTreeInstance = this;
        DontDestroyOnLoad(gameObject);
    }
}
