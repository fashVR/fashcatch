using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
      
    public GameObject MainMenu;
    Animator menuAnim;
    public List<Animator> animators = new List<Animator>();
    int watchFromMenu;
    public GameObject intro;
    public GameObject ending;
    AudioSource introMusic;
    bool comingOut = false;
    string videoToPlay;
    public GameObject modsPanel;
    public GameObject videoButton;
    public GameObject removeAdsButton;
    public GameObject lockSpeedrun;
    public GameObject speedRunButton;
    public GameObject speedRunLock;
    public GameObject[] menuStuff = new GameObject[0];
    DontDestroy dontDestroy;


    private void Awake()
    {
        PlayerPrefs.SetInt("FirstTime", 0);
        watchFromMenu = PlayerPrefs.GetInt("WatchFromMenu");

        introMusic = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        dontDestroy = introMusic.GetComponent<DontDestroy>();
        dontDestroy.shouldLow = true;

        videoToPlay = PlayerPrefs.GetString("Video", "Intro");
        if(videoToPlay == "Intro")
        {
            intro.SetActive(true);
            ending.SetActive(false);
        }
        else if((videoToPlay == "Ending"))
        {
            intro.SetActive(false);
            ending.SetActive(true);
            PlayerPrefs.SetInt("IsGameFinished", 1);
        }

        if (PlayerPrefs.GetInt("FirstLevelDone", 0) == 0)
        {
            videoButton.SetActive(false);
        }
        else
        {
            videoButton.SetActive(true);
        }

        if (PlayerPrefs.GetInt("UnlockSpeedrun") == 1)
        {
            speedRunButton.SetActive(true);
            speedRunLock.SetActive(false);
        }
        else
        {
            speedRunButton.SetActive(false);
            speedRunLock.SetActive(true);
        }

        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            removeAdsButton.SetActive(false);
        }
        else
        {
            removeAdsButton.SetActive(true);
        }

        if (watchFromMenu == 1)
        {
            modsPanel.SetActive(false);
        }
        else if (watchFromMenu == 0)
        {
            modsPanel.SetActive(true);
            Invoke("HideMods", .30f);
        }
    }

    private void Start()
    {
        
        if (PlayerPrefs.GetString("LastLevel") == "MainMenu")
        {
            foreach (Animator a in animators)
            {
                a.enabled = true;
            }

            
            MainMenu.SetActive(true);
            menuAnim = GameObject.Find("Main Menu").GetComponent<Animator>();
            menuAnim.Play("MainMenuFadeOut");
        }
        else
        {
            foreach(GameObject m in menuStuff)
            {
                m.SetActive(false);
            }

            foreach (Animator a in animators)
            {
                a.enabled = true;
            }
        }

        if(videoToPlay == "Intro")
        {
            Invoke("LoadNextLevel", 37);
        }
        else if(videoToPlay == "Ending")
        {
            Invoke("LoadNextLevel", 41f);
        }
    }

    private void Update()
    {
        Debug.Log("WatchFromMenu = " + watchFromMenu);

        if (introMusic.volume > 0)
        {
            introMusic.volume -= Time.deltaTime;
        }
    }

    void HideMods()
    {
        modsPanel.SetActive(false);
    }

    public void Play()
    {
        MainMenu.SetActive(false);
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetAll()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void AfterFirst()
    {
        Time.timeScale = 0;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetString("LastLevel", "VideoScene");

        dontDestroy.shouldLow = false;

        if (videoToPlay == "Intro")
        {
            if (watchFromMenu == 1)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (watchFromMenu == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            PlayerPrefs.SetInt("SvaedLevel", 0);
        }
    }
}
