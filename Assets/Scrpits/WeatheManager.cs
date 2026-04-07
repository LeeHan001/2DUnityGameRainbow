using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatheManager : MonoBehaviour
{
    static public WeatheManager instance;
    public ParticleSystem rain;
    // Start is called before the first frame update

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
    //private AudioManager theAudio;
    public string rain_sound;

    void Start()
    {
        //theAudio = FindObjectOfType<AudioManager>();
    }

    public void Rain()
    {
        //theAudio.Play(rain_sound);
        rain.Play();
    }
    public void RainStop()
    {
        //theAudio.Stop(rain_sound);
        rain.Stop();
    }

    public void RainDrop()
    {
        rain.Emit(1);
    }

}
