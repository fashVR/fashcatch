using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip tutorialMusic;
    public AudioClip[] levelMusic = new AudioClip[0];
    public bool shouldLow;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = .8f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            audioSource.clip = tutorialMusic;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            if(SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3" || SceneManager.GetActiveScene().name == "Level4" || SceneManager.GetActiveScene().name == "Level5")
            {
                audioSource.clip = levelMusic[0];
            }
            else
            {
                audioSource.clip = levelMusic[1];
            }
        }
        else if(SceneManager.GetActiveScene().name == "SpeedrunEnding")
        {
            audioSource.clip = levelMusic[1];
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Options" || SceneManager.GetActiveScene().name == "ButtonLayout" || SceneManager.GetActiveScene().name == "LevelSelect")
        {
            audioSource.clip = menuMusic;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if(SceneManager.GetActiveScene().name != "VideoScene")
        {
            if (shouldLow)
            {
                audioSource.volume = 0;
            }
            else
            {
                audioSource.volume = .8f;
            }
        }
    }
}
