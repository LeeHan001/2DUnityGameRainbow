using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;
    public float Bgm_sound;

    public AudioClip[] clips;

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    #region Singleton
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
    #endregion Singleton
    void Start()
    {
        source = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "Title" && PlayerPrefs.GetInt("End") != 1)
        {
            Play(1);
            FadeInMusic();
        }
        else if(SceneManager.GetActiveScene().name == "Title" && PlayerPrefs.GetInt("End") == 1)
        {
            Play(13);
            FadeInMusic();
        }
    }

    public void Play(int _playMusicTrack)
    {
        source.volume = Bgm_sound;
        source.clip = clips[_playMusicTrack];
        source.Play();
    }    

    public void SetVolume(float _volume)
    {
        source.volume = _volume;
    }

    public void Parse()
    {
        source.Pause();
    }
    public void UnParse()
    {
        source.UnPause();
    }
    public void Stop()
    {
        source.Stop();
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for(float i = Bgm_sound;  i >= 0f; i -= 0.001f )
        {
            source.volume = i;
            yield return waitTime;
        }
    }
    public void FadeInMusic()
    {
        StartCoroutine(FadeInMusicCoroutine());
    }

    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= Bgm_sound; i += 0.001f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
}
