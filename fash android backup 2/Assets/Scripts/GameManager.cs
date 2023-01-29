using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int savedLevel;
    int showMenu;
    int firstTime;
    public GameObject tutorialText;
    public GameObject tutorialText2;
    public GameObject tutorialText3;
    public GameObject buttonCloseTutorial;
    public GameObject buttonCloseTutorial2;
    public GameObject buttonCloseTutorial3;
    public GameObject MainMenu;
    public GameObject GameUI;
    public GameObject pauseMenu;
    Animator resumeAnim;
    Animator menuAnim;
    public List<Animator> animators = new List<Animator>();
    int levelNumber;
    public TextMeshProUGUI levelText;
    string colorSide;
    float colorCurrentSize;
    float jumpCurrentSize;
    public GameObject lJump;
    public GameObject rJump;
    public GameObject lDive;
    public GameObject rDive;
    public GameObject lColot;
    public GameObject rColor;

    public float timer = 0;
    public float best = 0;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestText;
    public float levelTime;
    public CircleWipeController wipeController;

    private void Awake()
    {
        levelTime = 0;
        wipeController = Camera.main.GetComponent<CircleWipeController>();
        resumeAnim = pauseMenu.GetComponent<Animator>();
        if (PlayerPrefs.GetString("Mode") == "Normal")
        {
            PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);
        }
        
        colorSide = PlayerPrefs.GetString("ColorSide", "BOTH SIDES");
        colorCurrentSize = PlayerPrefs.GetFloat("ColorSize", 300);
        jumpCurrentSize = PlayerPrefs.GetFloat("JumpSize", 300);
        wipeController = Camera.main.GetComponent<CircleWipeController>();


        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorialText.SetActive(true);
            buttonCloseTutorial.SetActive(true);
            PlayerPrefs.SetInt("FirstLevelDone", 1);
        }

        levelNumber = 16 - SceneManager.GetActiveScene().buildIndex;
        if(levelNumber >= 1) 
        {
            levelText.text = "FLOOR " + levelNumber.ToString();
        }
        else
        {
            levelText.text = "";
        }

        if (colorSide == "BOTH SIDES")
        {
            lColot.SetActive(true);
            rColor.SetActive(true);
        }
        else if (colorSide == "RIGHT")
        {
            lColot.SetActive(false);
            rColor.SetActive(true);
        }
        else if (colorSide == "LEFT")
        {
            lColot.SetActive(true);
            rColor.SetActive(false);
        }

        if (PlayerPrefs.GetString("Mode") == "Speedrun")
        {
            timerText.enabled = true;
            bestText.enabled = true;
        }
        else
        {
            timerText.enabled = false;
            bestText.enabled = false;
        }

        lColot.GetComponent<RectTransform>().sizeDelta = new Vector2(colorCurrentSize, colorCurrentSize * 1.8f);
        rColor.GetComponent<RectTransform>().sizeDelta = new Vector2(colorCurrentSize, colorCurrentSize * 1.8f);
        lJump.GetComponent<RectTransform>().sizeDelta = new Vector2(jumpCurrentSize, jumpCurrentSize);
        rJump.GetComponent<RectTransform>().sizeDelta = new Vector2(jumpCurrentSize, jumpCurrentSize);
        lDive.GetComponent<RectTransform>().sizeDelta = new Vector2(jumpCurrentSize, jumpCurrentSize);
        rDive.GetComponent<RectTransform>().sizeDelta = new Vector2(jumpCurrentSize, jumpCurrentSize);
    }

    private void Start()
    {
        timer = PlayerPrefs.GetFloat("Timer", 0);       
        bestText.text = "BEST: " + PlayerPrefs.GetFloat("Best").ToString("0.00");

        if (PlayerPrefs.GetString("LastLevel") == "MainMenu") 
        {
            wipeController.enabled = false;

            foreach (Animator a in animators)
            {
                a.enabled = true;
            }

            MainMenu.SetActive(true);
            menuAnim = GameObject.Find("Main Menu").GetComponent<Animator>();           
            GameUI.SetActive(false);
            
            menuAnim.Play("MainMenuFadeOut");
        }
        else 
        {
            wipeController.enabled = true;
            GameUI.SetActive(true);
            MainMenu.SetActive(false);

            foreach(Animator a in animators)
            {
                a.enabled = true;
            }

            Invoke("UpdateLastScene", .1f);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "TIME: " + timer.ToString("0.00");
        levelTime += Time.deltaTime;
    }

    public void Play()
    {
        MainMenu.SetActive(false);
        GameUI.SetActive(true);
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
        pauseMenu.SetActive(true);
        GameUI.SetActive(false);
    }

    public void Resume()
    {
        resumeAnim.Play("PauseMenuFadeOut");
        Time.timeScale = 1;
        GameUI.SetActive(true);
    }

    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        Time.timeScale = 1;

        if (!wipeController.enabled)
        {
            wipeController.enabled = true;
        }
        PlayerPrefs.SetFloat("Timer", timer);
        wipeController.FadeOut();
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        StartCoroutine(Menu());
    }


    public IEnumerator Menu()
    {
        Time.timeScale = 1;

        if (!wipeController.enabled)
        {
            wipeController.enabled = true;
        }

        wipeController.FadeOut();

        yield return new WaitForSeconds(1);
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenu");
    }

    void WipeEnable()
    {
        wipeController.enabled = true;
    }

    void UpdateLastScene()
    {
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
    }
}
