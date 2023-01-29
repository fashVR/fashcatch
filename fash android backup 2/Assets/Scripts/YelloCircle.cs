using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YelloCircle : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetString("Circle", "Yello");
        PlayerPrefs.SetString("Mode", "Normal");
        SceneManager.LoadScene("MainMenu");
        Application.targetFrameRate = 60;
    }
}
