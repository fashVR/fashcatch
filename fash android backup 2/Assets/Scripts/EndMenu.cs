using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    float timer = 0;
    float bestTime = 0;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestText;

    
    
    Animator anim;
    AudioSource audioSource;
    bool shouldLow;
    MenuCircleWipe circleWipe;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSource = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        circleWipe = Camera.main.GetComponent<MenuCircleWipe>();

        
        timer = PlayerPrefs.GetFloat("Timer", 0);

        if (PlayerPrefs.GetFloat("Best") > timer)
        {
            PlayerPrefs.SetFloat("Best", timer);
        }
        else
        {
            PlayerPrefs.SetFloat("Best", PlayerPrefs.GetFloat("Best"));
        }

        bestTime = PlayerPrefs.GetFloat("Best", timer);
        
        timeText.text = "TIME = " + timer.ToString("0.00");
        bestText.text = "BEST = " + bestTime.ToString("0.00");
        if (PlayerPrefs.GetFloat("Best") == 0)
        {
            PlayerPrefs.SetFloat("Best", timer);
        }
        else if (PlayerPrefs.GetFloat("Best") > timer)
        {
            PlayerPrefs.SetFloat("Best", timer);
        }
    }

    private IEnumerator Start()
    {

        yield return null;
    }

    private void Update()
    {
        if(shouldLow)
        {
            if(audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime;
            }
        }
    }

    public void BackToMenu()
    {
        circleWipe.enabled = false;
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        anim.Play("SpeedrunEndQuit");
        shouldLow = true;
        StartCoroutine(backToMenuWait());
    }

    IEnumerator backToMenuWait()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
