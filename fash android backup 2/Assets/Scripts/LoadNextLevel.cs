using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadNextLevel : MonoBehaviour
{
    public TextMeshProUGUI minutesText;
    int minutesToWedding;
    string nextLevel;
    string lastLevel;
    AudioSource introMusic;
    DontDestroy dontDestroy;
    bool shouldLow;

    private void Awake()
    {
        introMusic = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        lastLevel = PlayerPrefs.GetString("LastLevel");
        dontDestroy = introMusic.GetComponent<DontDestroy>();

        if (lastLevel == "Tutorial")
        {
            nextLevel = "Tutorial2";
            LoadLevel();
            shouldLow = false;
        }
        else if (lastLevel == "Tutorial2")
        {
            nextLevel = "Tutorial3";
            LoadLevel();
            shouldLow = true;
        }
        else if(lastLevel == "Tutorial3")
        {
            nextLevel = "Level1";
            minutesToWedding = 55;
            shouldLow = true;
        }
        else if(lastLevel == "Level1")
        {
            nextLevel = "Level2";
            minutesToWedding = 50;
            shouldLow = false;
        }
        else if(lastLevel == "Level2")
        {
            nextLevel = "Level3";
            minutesToWedding = 45;
            shouldLow = false;
        }
        else if (lastLevel == "Level3")
        {
            nextLevel = "Level4";
            minutesToWedding = 40;
            shouldLow = false;
        }
        else if (lastLevel == "Level4")
        {
            nextLevel = "Level5";
            minutesToWedding = 35;
            shouldLow = false;
        }
        else if (lastLevel == "Level5")
        {
            nextLevel = "Level6";
            minutesToWedding = 30;
            shouldLow = true;
        }
        else if (lastLevel == "Level6")
        {
            nextLevel = "Level7";
            minutesToWedding = 25;
            shouldLow = false;
        }
        else if (lastLevel == "Level7")
        {
            nextLevel = "Level8";
            minutesToWedding = 20;
            shouldLow = false;
        }
        else if (lastLevel == "Level8")
        {
            nextLevel = "Level9";
            minutesToWedding = 15;
            shouldLow = false;
        }
        else if (lastLevel == "Level9")
        {
            nextLevel = "Level10";
            minutesToWedding = 10;
            shouldLow = false;
        }
        else if (lastLevel == "Level10")
        {
            nextLevel = "Ending";
        }

        if(minutesToWedding == 60)
        {
            minutesText.text = "1 HOUR TO WEDDING";
        }
        else
        {
            minutesText.text = minutesToWedding + " MINUTES TO WEDDING";
        }

        dontDestroy.shouldLow = shouldLow;
    }

    void LoadLevel()
    {
        Debug.Log(nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
}
