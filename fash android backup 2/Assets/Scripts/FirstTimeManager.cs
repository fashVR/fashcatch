using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTimeManager : MonoBehaviour
{
    private void Awake()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (PlayerPrefs.GetInt("FirstTime", 1) == 1)
            {
            }
            else
            {
            }
        }
    }
    void Start()
    {
   
        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastLevel", "null");
        PlayerPrefs.SetInt("FirstTime", 1);
    }
}
