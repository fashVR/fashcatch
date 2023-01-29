using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class VideoMenuScript : MonoBehaviour
{
    Animator menuAnim;
    public List<Animator> animators = new List<Animator>();
    
    private bool allowSwitch;
    private bool shouldPlay;
    private bool shouldPlayScene;
    int savedLevel;
    public Image blackScreen;
    public VideoManager videoManager;
    public GameObject storyButton;
    public TMP_FontAsset greyFont;
    public TMP_FontAsset whiteFont;

    void Awake()
    {
        menuAnim = GetComponent<Animator>();
        savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);

        if (PlayerPrefs.GetInt("IsGameFinished") == 1)
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

    private void Update()
    {
        if (shouldPlay == true)
        {
            CheckSwitch();
            if (CheckSwitch())
            {
                SceneManager.LoadScene(savedLevel);

                shouldPlay = false;
            }
        }

        if (shouldPlayScene == true)
        {
            CheckSwitch();
            if (CheckSwitch())
            {
                SceneManager.LoadScene("VideoScene");
                shouldPlay = false;
            }
        }

    }
    public void BeforeStartGame()
    {

    }
    public void StartGame()
    {
       

    }
    public void Play()
    {
        shouldPlay = true;
    }

    public void PlayScene()
    {
        shouldPlayScene = true;
    }

    public void SceneFadeOut()
    {
        PlayerPrefs.SetString("LastLevel", "MainMenu");
        blackScreen.enabled =  true;
        menuAnim.Play("SceneFadeOut");
    }

    public void ReturnToMenu()
    {
        videoManager.LoadNextLevel();
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

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetAll()
    {
        SceneManager.LoadScene(1);
    }
}
