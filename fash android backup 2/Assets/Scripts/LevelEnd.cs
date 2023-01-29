using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public float loadTime = 1;
    GameManager gameManager;
    CircleWipeController circleWipe;

    private void Awake()
    {

        circleWipe = Camera.main.GetComponent<CircleWipeController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fashDetector")
        {

            string levelSaveString = "Level" + (SceneManager.GetActiveScene().buildIndex - 5) + "Played";
            PlayerPrefs.SetString(levelSaveString, "true");
            PlayerPrefs.SetFloat("Timer", gameManager.timer);
            circleWipe.enabled = true;
            circleWipe.FadeOut();
            Invoke("LoadNextLevel", loadTime);

            if(PlayerPrefs.GetString("Mode") != "FreePlay")
            {
                PlayerPrefs.SetString("SavedLevel", SceneManager.GetActiveScene().name);
            }
        }
        
    }

    void LoadNextLevel()
    {
        PlayerPrefs.SetFloat("Level" + (SceneManager.GetActiveScene().buildIndex - 4) + "Time", gameManager.levelTime);
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("FirstTime", 2);
        
        if (SceneManager.GetActiveScene().name == "Level10")
        {
            Debug.Log("BRRRRRRRR");
            PlayerPrefs.SetInt("UnlockSpeedrun", 1);
            Debug.Log(PlayerPrefs.GetInt("UnlockSpeedrun"));
            if (PlayerPrefs.GetString("Mode") == "FreePlay")
            {
                SceneManager.LoadScene("LevelSelect");
            }
            else if (PlayerPrefs.GetString("Mode") == "Speedrun")
            {
                SceneManager.LoadScene("SpeedrunEnding");
            }
            else
            {
                
                PlayerPrefs.SetString("Video", "Ending");
                SceneManager.LoadScene("VideoScene");
            }
        }
        else
        {
            if (PlayerPrefs.GetString("Mode") == "Normal")
            {
                SceneManager.LoadScene("MinutesToWedding");
            }
            else if (PlayerPrefs.GetString("Mode") == "FreePlay")
            {
                SceneManager.LoadScene("LevelSelect");
            }
            else if(PlayerPrefs.GetString("Mode") == "Speedrun")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
