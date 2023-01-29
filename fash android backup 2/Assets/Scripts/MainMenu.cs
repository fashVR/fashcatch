using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Animator menuAnim;
    Animator modsPanelAnim;
    public List<Animator> animators = new List<Animator>();
    private bool allowSwitch;
    private bool shouldPlay;
    private bool shouldPlayScene;
    int savedLevel;
    string sceneToGo;

    bool isGameFinished()
    {
        if((PlayerPrefs.GetInt("GameFinished", 0)) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public GameObject modesPanel;
    public GameObject GameUI;
    public AudioMixer sfxMixer;
    public AudioMixer musicMixer;
    public AudioSource introMusicSFX;
    public GameObject speedRunButton;
    public GameObject speedRunLock;
    public GameObject videoButton;
    public GameObject removeAdsButton;
    public GameObject storyButton;
    public TMP_FontAsset greyFont;
    public TMP_FontAsset whiteFont;
    bool shouldLowMusic;

    void Awake()
    {
        Debug.Log("dfdf: " + PlayerPrefs.GetInt("UnlockSpeedrun", 0));
        //modesPanel.SetActive(false);
        introMusicSFX = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        introMusicSFX.volume = 1;
        menuAnim = GetComponent<Animator>();
        modsPanelAnim = modesPanel.GetComponent<Animator>();
        savedLevel = PlayerPrefs.GetInt("SavedLevel");
        PlayerPrefs.SetString("Video", "Intro");

        if(PlayerPrefs.GetInt("IsGameFinished") == 1)
        {
            storyButton.GetComponent<Image>().color = Color.green;
            storyButton.GetComponentInChildren<TextMeshProUGUI>().text = "STORY MODE\n(START OVER)";
            storyButton.GetComponentInChildren<TextMeshProUGUI>().font = whiteFont;
        }
        else
        {
            storyButton.GetComponent<Image>().color = Color.white;
            storyButton.GetComponentInChildren<TextMeshProUGUI>().text = "STORY MODE";
            storyButton.GetComponentInChildren<TextMeshProUGUI>().font = greyFont;
        }
    }

    private void Start()
    {
        sfxMixer.SetFloat("Volume", PlayerPrefs.GetFloat("SfxValue", 0));
        musicMixer.SetFloat("Volume", PlayerPrefs.GetFloat("MusicValue", 0));
        PlayerPrefs.SetString("Circle", "Black");
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("FirstLevelDone", 0) == 0)
        {
            videoButton.SetActive(false);
        }
        else
        {
            videoButton.SetActive(true);
        }

        if (PlayerPrefs.GetInt("UnlockSpeedrun", 0) == 1)
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
        
        if (shouldPlay == true)
        { 
            CheckSwitch();
            if (CheckSwitch())
            {
                if(PlayerPrefs.GetString("Mode") != "Speedrun")
                {
                    if (savedLevel == 0)
                    {
                        SceneManager.LoadScene("VideoScene");
                    }
                    else
                    {
                        SceneManager.LoadScene(savedLevel);
                        shouldPlay = false;
                    }
                }
                else
                {
                    SceneManager.LoadScene("Level1");
                    shouldPlay = false;
                }                
            }
            
        }

        if (shouldPlayScene == true)
        {
            CheckSwitch();
            if (CheckSwitch())
            {
                PlayerPrefs.SetInt("WatchFromMenu", 1);
                SceneManager.LoadScene("VideoScene");
                shouldPlay = false;
            }
        }

    }
    public void BeforeStartGame()
    {
        GameUI.SetActive(true);
    }
    public void StartGame()
    {
        gameObject.SetActive(false);
        GameUI.SetActive(true);
    }

    public void OpenModsPanel()
    {
        modesPanel.SetActive(true);
    }

    public void StoryMode()
    {
        PlayerPrefs.SetInt("FirstTime", 0);
        PlayerPrefs.SetString("LastLevel", "MainMenu");
        PlayerPrefs.SetInt("WatchFromMenu", 0);
        PlayerPrefs.SetString("Mode", "Normal");
        shouldPlay = true;
    }

    public void SpeedrunMode()
    {
        PlayerPrefs.SetInt("FirstTime", 0);
        PlayerPrefs.SetString("LastLevel", "MainMenu");
        PlayerPrefs.SetInt("WatchFromMenu", 0);
        PlayerPrefs.SetString("Mode", "Speedrun");
        PlayerPrefs.SetFloat("Timer", 0);
        
        sceneToGo = "Level1";
        menuAnim.SetBool("CloseMods", true);
        shouldPlay = true;
    }

    public void Freeplay()
    {
        menuAnim.SetBool("DoFadeOut", true);
        sceneToGo = "LevelSelect";
    }

    public void PlayScene()
    {
        PlayerPrefs.SetInt("FirstTime", 0);
        PlayerPrefs.SetString("LastLevel", "MainMenu");
        shouldPlayScene = true;
        shouldLowMusic = true;
        
    }
    private bool CheckSwitch()
    {
        if (allowSwitch == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AllowSwitch()
    {
        allowSwitch = true;
    }
    public void NotAllowSwitch()
    {
        allowSwitch = false;
    }

    public void Options()
    {
        PlayerPrefs.SetInt("FirstTime", 0);
        PlayerPrefs.SetString("LastLevel", "MainMenu");
        menuAnim.SetBool("DoFadeOut", true);
        sceneToGo = "Options";
    }

    public void HideModsPanel()
    {
        modsPanelAnim.SetBool("ModesClosing", true);
    }

    public void DisableModsAnim()
    {
        modsPanelAnim.enabled = false;
    }

    void GoToScene()
    {
        SceneManager.LoadScene(sceneToGo);
    }

    public void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        removeAdsButton.GetComponent<Animator>().enabled = true;
        Invoke("DisableAnim", 1);
    }

    void DisableAnim()
    {
        removeAdsButton.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
}
