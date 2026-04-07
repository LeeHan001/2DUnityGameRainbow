using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_Tone : MonoBehaviour
{
    static public Single_Tone instance;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
