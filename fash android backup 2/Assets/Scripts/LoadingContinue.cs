using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingContinue : MonoBehaviour
{
    int savedLevel;

    void Awake()
    {
        savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);

        PlayerPrefs.SetInt("ShowMenu", 1);
        PlayerPrefs.SetInt("YelloCircle", 1);

        SceneManager.LoadScene(savedLevel);
    }
}
