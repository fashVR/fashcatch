using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Fash")
        {
            LoadNextToturial();
        }
    }

    void LoadNextToturial()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial2");
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial2")
        {
            SceneManager.LoadScene("Tutorial3");
        }
    }
}
