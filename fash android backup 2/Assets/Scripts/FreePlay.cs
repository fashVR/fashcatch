using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FreePlay : MonoBehaviour
{
    Animator animator;
    MenuCircleWipe circleWipe;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        circleWipe = Camera.main.GetComponent<MenuCircleWipe>();
    }



    public void Back()
    {
        animator.SetBool("DoFadeOut", true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator LoadNextLevel(string level)
    {
        PlayerPrefs.SetString("Mode", "FreePlay");
        PlayerPrefs.SetString("LastLevel", "LevelSelect");
        circleWipe.enabled = true;
        circleWipe.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
    }
}
